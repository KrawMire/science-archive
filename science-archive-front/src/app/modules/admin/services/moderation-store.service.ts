import { Injectable } from "@angular/core";
import { BehaviorSubject, map } from "rxjs";
import { Article } from "@models/article/article";
import { ArticleApiService } from "@services/article-api.service";
import { ArticleStatus, toDate } from "@modules/shared/utils/article-utils";

/**
 * Keeps the moderation list shared between the admin header (pending counter) and the queue page
 */
@Injectable({
  providedIn: "root"
})
export class ModerationStoreService {
  readonly articles$ = new BehaviorSubject<Article[]>([]);
  readonly isLoading$ = new BehaviorSubject<boolean>(false);
  readonly hasError$ = new BehaviorSubject<boolean>(false);
  readonly pendingCount$ = this.articles$.pipe(
    map(articles => articles.filter(article => article.status === ArticleStatus.ToVerify).length)
  );

  constructor(private readonly articleService: ArticleApiService) {}

  refresh(): void {
    this.isLoading$.next(true);
    this.hasError$.next(false);

    this.articleService
      .getAllArticles()
      .subscribe({
        next: (response) => {
          const articles = response.articles
            .map(article => ({ ...article, creationDate: toDate(article.creationDate) ?? undefined }))
            .sort((a, b) => (b.creationDate?.getTime() ?? 0) - (a.creationDate?.getTime() ?? 0));

          this.articles$.next(articles);
          this.isLoading$.next(false);
        },
        error: (error) => {
          console.log(error);
          this.hasError$.next(true);
          this.isLoading$.next(false);
        }
      });
  }
}
