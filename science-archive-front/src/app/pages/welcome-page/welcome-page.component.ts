import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from "@angular/router";
import { Title } from "@angular/platform-browser";
import { Subscription } from "rxjs";
import { Article } from "@models/article/article";
import { Category } from "@models/category/category";
import { Subcategory } from "@models/category/subcategory";
import { News } from "@models/news/news";
import { ArticleApiService } from "@services/article-api.service";
import { CategoryApiService } from "@services/category-api.service";
import { NewsApiService } from "@services/news-api.service";
import { AuthService } from "@modules/auth/services/auth.service";
import { authorNames, findField, toDate } from "@modules/shared/utils/article-utils";

const LATEST_ARTICLES_COUNT = 6;
const LATEST_NEWS_COUNT = 2;
const BROWSE_CHIPS_COUNT = 4;

@Component({
  selector: 'sar-welcome-page',
  templateUrl: './welcome-page.component.html',
  styleUrls: ['./welcome-page.component.scss']
})
export class WelcomePageComponent implements OnInit, OnDestroy {
  searchText = "";
  isLoadingArticles = true;

  featured?: Article;
  latestArticles: Article[] = [];
  categories: Category[] = [];
  browseChips: Subcategory[] = [];
  latestNews: News[] = [];

  readonly isSignedIn: boolean;

  private readonly subscriptions = new Subscription();

  constructor(
    private readonly router: Router,
    private readonly articleService: ArticleApiService,
    private readonly categoryService: CategoryApiService,
    private readonly newsService: NewsApiService,
    authService: AuthService,
    titleService: Title,
  ) {
    this.isSignedIn = !!authService.getCurrentUser();
    titleService.setTitle("Science Archive");
  }

  ngOnInit(): void {
    this.subscriptions.add(
      this.articleService.getAllVerifiedArticles().subscribe({
        next: (response) => {
          const articles = [...response.articles].sort(
            (a, b) => (toDate(b.creationDate)?.getTime() ?? 0) - (toDate(a.creationDate)?.getTime() ?? 0)
          );

          this.featured = articles[0];
          this.latestArticles = articles.slice(0, LATEST_ARTICLES_COUNT);
          this.isLoadingArticles = false;
        },
        error: (error) => {
          console.log(error);
          this.isLoadingArticles = false;
        },
      })
    );

    this.subscriptions.add(
      this.categoryService.getAllCategories().subscribe({
        next: (response) => {
          this.categories = response.categories;
          this.browseChips = response.categories
            .flatMap(category => category.subcategories)
            .slice(0, BROWSE_CHIPS_COUNT);
        },
        error: (error) => console.log(error),
      })
    );

    this.subscriptions.add(
      this.newsService.getAllNews().subscribe({
        next: (response) => {
          this.latestNews = [...response.news]
            .sort((a, b) => (toDate(b.creationDate)?.getTime() ?? 0) - (toDate(a.creationDate)?.getTime() ?? 0))
            .slice(0, LATEST_NEWS_COUNT);
        },
        error: (error) => console.log(error),
      })
    );
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }

  search(): void {
    const query = this.searchText.trim();
    this.router.navigate(["/content", "articles"], { queryParams: query ? { q: query } : {} });
  }

  fieldNameOf(article: Article): string | undefined {
    return findField(this.categories, article.categoryId)?.name;
  }

  authorsOf(article: Article): string {
    return authorNames(article);
  }

  subcategoryNames(category: Category): string {
    return category.subcategories.map(sub => sub.name).join(" · ");
  }
}
