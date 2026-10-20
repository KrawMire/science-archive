import { Component, OnDestroy, OnInit } from "@angular/core";
import { Title } from "@angular/platform-browser";
import { ActivatedRoute } from "@angular/router";
import { BehaviorSubject, Subscription } from "rxjs";
import { NzMessageService } from "ng-zorro-antd/message";
import { NzI18nService } from "ng-zorro-antd/i18n";
import { Article, ArticleAuthor, ArticleDocument } from "@models/article/article";
import { Category } from "@models/category/category";
import { ArticleApiService } from "@services/article-api.service";
import { CategoryApiService } from "@services/category-api.service";
import {
  ArticleAuthorRole,
  buildCitation,
  documentKind,
  documentUrl,
  findField,
  isNewArticle,
  sortedAuthors
} from "@modules/shared/utils/article-utils";

const MORE_ARTICLES_COUNT = 3;

@Component({
  selector: 'sar-article-page',
  templateUrl: './article-page.component.html',
  styleUrls: ['./article-page.component.scss']
})
export class ArticlePageComponent implements OnInit, OnDestroy {
  article$ = new BehaviorSubject<Article | null>(null);
  isLoading$ = new BehaviorSubject<boolean>(true);
  error$ = new BehaviorSubject<number | null>(null);

  field?: Category;
  authors: ArticleAuthor[] = [];
  paragraphs: string[] = [];
  moreArticles: Article[] = [];
  isNew = false;

  readonly ArticleAuthorRole = ArticleAuthorRole;
  readonly telegramUrl = "https://t.me/science_archive_project";

  private categories: Category[] = [];
  private readonly subscriptions = new Subscription();

  constructor(
    private readonly route: ActivatedRoute,
    private readonly articleService: ArticleApiService,
    private readonly categoryService: CategoryApiService,
    private readonly i18nService: NzI18nService,
    private readonly messageService: NzMessageService,
    private readonly titleService: Title) {}

  ngOnInit(): void {
    this.subscriptions.add(
      this.categoryService.getAllCategories().subscribe({
        next: (response) => {
          this.categories = response.categories;
          this.field = findField(this.categories, this.article$.value?.categoryId);
        },
        error: (error) => console.log(error),
      })
    );

    // The component is reused when navigating between articles, e.g. from "More in ..."
    this.subscriptions.add(
      this.route.paramMap.subscribe(params => this.loadArticle(params.get("id")))
    );
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }

  get primaryDocument(): ArticleDocument | undefined {
    const documents = this.article$.value?.documents ?? [];
    return documents.find(document => documentKind(document) === "pdf") ?? documents[0];
  }

  documentUrl(document: ArticleDocument): string {
    return documentUrl(document);
  }

  async copyCitation(): Promise<void> {
    const article = this.article$.value;

    if (!article) {
      return;
    }

    await this.copy(buildCitation(article, window.location.href), "article.citationCopied");
  }

  async copyLink(): Promise<void> {
    await this.copy(window.location.href, "article.linkCopied");
  }

  async share(): Promise<void> {
    const article = this.article$.value;

    if (article && navigator.share) {
      try {
        await navigator.share({ title: article.title, url: window.location.href });
      } catch {
        // The user closed the share sheet
      }
      return;
    }

    await this.copyLink();
  }

  downloadAll(): void {
    for (const document of this.article$.value?.documents ?? []) {
      const link = window.document.createElement("a");
      link.href = documentUrl(document);
      link.download = document.name;
      link.rel = "noopener";
      link.click();
    }
  }

  private loadArticle(articleId: string | null): void {
    this.article$.next(null);
    this.error$.next(null);
    this.moreArticles = [];

    if (!articleId) {
      this.error$.next(400);
      this.isLoading$.next(false);
      return;
    }

    this.isLoading$.next(true);

    this.articleService
      .getArticleById(articleId)
      .subscribe({
        next: (response) => {
          const article = response.article;

          this.authors = sortedAuthors(article);
          this.paragraphs = (article.description ?? "")
            .split(/\n\s*\n/)
            .map(paragraph => paragraph.trim())
            .filter(paragraph => paragraph.length > 0);
          this.isNew = isNewArticle(article);
          this.field = findField(this.categories, article.categoryId);
          this.article$.next(article);
          this.isLoading$.next(false);
          this.titleService.setTitle(`Science Archive - ${article.title}`);
          this.loadMoreArticles(article);
        },
        error: (error) => {
          this.error$.next(error.status ?? 0);
          this.isLoading$.next(false);
          this.titleService.setTitle("Unknown article");
        }
      });
  }

  private loadMoreArticles(article: Article): void {
    this.articleService
      .getArticlesByCategoryId(article.categoryId)
      .subscribe({
        next: (response) => {
          this.moreArticles = response.articles
            .filter(other => other.id !== article.id)
            .sort((a, b) => new Date(b.creationDate ?? 0).getTime() - new Date(a.creationDate ?? 0).getTime())
            .slice(0, MORE_ARTICLES_COUNT);
        },
        error: (error) => console.log(error),
      });
  }

  private async copy(text: string, successKey: string): Promise<void> {
    try {
      await navigator.clipboard.writeText(text);
      this.messageService.success(this.i18nService.translate(successKey));
    } catch {
      this.messageService.error(this.i18nService.translate("article.copyFailed"));
    }
  }
}
