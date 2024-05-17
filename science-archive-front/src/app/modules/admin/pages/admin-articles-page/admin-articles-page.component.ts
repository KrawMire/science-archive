import { Component, OnInit } from "@angular/core";
import { NzCardComponent } from "ng-zorro-antd/card";
import { BehaviorSubject, map } from "rxjs";
import { Article } from "@models/article/article";
import { NzMessageService } from "ng-zorro-antd/message";
import { ArticleApiService } from "@services/article-api.service";
import { AdminArticleCardComponent } from "@modules/admin/components/admin-article-card/admin-article-card.component";
import { AsyncPipe, NgForOf, NgIf } from "@angular/common";
import { NzEmptyComponent } from "ng-zorro-antd/empty";
import { NzSpinComponent } from "ng-zorro-antd/spin";
import { NzI18nPipe } from "ng-zorro-antd/i18n";

@Component({
  selector: 'sar-admin-articles-page',
  standalone: true,
  imports: [
    NzCardComponent,
    AdminArticleCardComponent,
    NgIf,
    NgForOf,
    AsyncPipe,
    NzEmptyComponent,
    NzSpinComponent,
    NzI18nPipe
  ],
  templateUrl: './admin-articles-page.component.html',
  styleUrl: './admin-articles-page.component.scss'
})
export class AdminArticlesPageComponent implements OnInit {
  isLoadingArticleList$: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(true);
  articles$ = new BehaviorSubject<Article[]>([]);

  constructor(
    private readonly messageService: NzMessageService,
    private readonly articleService: ArticleApiService,
  ) {}

  ngOnInit(): void {
    this.getArticles();
  }

  getArticles() {
    this.articles$.next([]);
    this.isLoadingArticleList$.next(true);

    this.articleService
      .getAllArticles()
      .pipe(
        map((response) => {
          return response.articles.map((article) => {
            article.creationDate = article.creationDate ? new Date(article.creationDate.toString()) : new Date();
            return article;
          });
        }),
      )
      .subscribe({
        next: (articles) => {
          this.articles$.next(articles);
          this.isLoadingArticleList$.next(false);
        },
        error: () => {
          this.messageService.error("An error occurred");
          this.isLoadingArticleList$.next(false);
        }
      });
  }
}
