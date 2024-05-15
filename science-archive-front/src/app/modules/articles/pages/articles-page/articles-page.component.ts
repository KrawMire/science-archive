import { Component, OnInit } from "@angular/core";
import { BehaviorSubject, map } from "rxjs";
import { Article } from "@models/article/article";
import { ArticleApiService } from "@services/article-api.service";
import { Title } from "@angular/platform-browser";
import { NzMessageService } from "ng-zorro-antd/message";
import { ActivatedRoute } from "@angular/router";
import { Subcategory } from "@models/category/subcategory";

@Component({
  selector: "sar-articles-page",
  templateUrl: "./articles-page.component.html",
  styleUrls: ["./articles-page.component.scss"]
})
export class ArticlesPageComponent implements OnInit {
  category$ = new BehaviorSubject<Subcategory | null>(null);
  articles$ = new BehaviorSubject<Article[]>([]);
  isLoading$ = new BehaviorSubject<boolean>(true);

  constructor(
    private readonly articleService: ArticleApiService,
    private readonly message: NzMessageService,
    private readonly route: ActivatedRoute,
    private readonly titleService: Title) {

  }

  ngOnInit(): void {
    this.route.queryParams.subscribe((params) => {
      this.isLoading$.next(true);
      const categoryId = params["categoryId"] as string;

      if (!categoryId) {
        this.category$.next(null);
        this.getAllArticles();
      } else {
        this.getArticlesByCategoryId(categoryId);
      }
    });
  }

  private getAllArticles(): void {
    this.articleService
      .getAllVerifiedArticles()
      .pipe(
        map((response) => {
          return response.articles.map((article) => {
            article.creationDate = article.creationDate ? new Date(article.creationDate.toString()) : new Date();
            return article;
          });
        }),
      )
      .subscribe({
        complete: () => (this.isLoading$.next(false)),
        next: (articles) => {
          this.articles$.next(articles);
          this.titleService.setTitle("Science Archive - Articles");
        },
        error: (error) => {
          this.titleService.setTitle("Science Archive - Articles");
          this.isLoading$.next(false);
          this.articles$.next([]);
          console.log(error);
          this.message.error("Unhandled error occurred.");
        },
      });
  }

  private getArticlesByCategoryId(categoryId: string): void {
    this.articleService
      .getArticlesByCategoryId(categoryId)
      .pipe(
        map((response) => {
          const articles = response.articles.map((article) => {
            article.creationDate = article.creationDate ? new Date(article.creationDate.toString()) : new Date();
            return article;
          });

          return {
            category: response.category,
            articles: articles
          }
        }),
      )
      .subscribe({
        complete: () => (this.isLoading$.next(false)),
        next: (res) => {
          this.articles$.next(res.articles);
          this.category$.next(res.category);
          this.titleService.setTitle(`Science Archive - ${res.category.name} - Articles`);
        },
        error: (error) => {
          this.isLoading$.next(false);
          this.articles$.next([]);
          this.titleService.setTitle("Science Archive - Articles");
          console.log(error);
          this.message.error("Unhandled error occurred.");
        },
      });
  }
}
