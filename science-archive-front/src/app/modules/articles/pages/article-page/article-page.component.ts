import { Component, OnInit } from "@angular/core";
import { Article } from "@models/article/article";
import { Title } from "@angular/platform-browser";
import { BehaviorSubject } from "rxjs";
import { ArticleService } from "@services/article.service";
import { ActivatedRoute } from "@angular/router";

@Component({
  selector: 'sar-article-page',
  templateUrl: './article-page.component.html',
  styleUrls: ['./article-page.component.scss']
})
export class ArticlePageComponent implements OnInit {
  article$ = new BehaviorSubject<Article | null>(null);
  isLoading$ = new BehaviorSubject<boolean>(true);
  error$ = new BehaviorSubject<number | null>(null);

  constructor(
    private readonly router: ActivatedRoute,
    private readonly articleService: ArticleService,
    private readonly titleService: Title) {}

  ngOnInit(): void {
      const articleId = this.router.snapshot.paramMap.get("id");

      if (!articleId) {
        this.error$.next(400);
        return;
      }

      this.articleService
        .getArticleById(articleId)
        .subscribe({
          complete: () => (this.isLoading$.next(true)),
          next: (response) => {
            this.article$.next(response.article)
            this.titleService.setTitle(`Science Archive - ${response.article.title}`);
          },
          error: (error) => {
            this.error$.next(error.status);
            this.isLoading$.next(false);
            this.titleService.setTitle("Unknown article");
          }
        });
  }
}
