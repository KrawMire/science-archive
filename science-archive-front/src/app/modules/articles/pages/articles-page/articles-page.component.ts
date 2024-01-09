import { Component, OnInit } from "@angular/core";
import { BehaviorSubject } from "rxjs";
import { Article } from "@models/article/article";
import { ArticleService } from "@services/article.service";

@Component({
  selector: "sar-articles-page",
  templateUrl: "./articles-page.component.html",
  styleUrls: ["./articles-page.component.scss"],
})
export class ArticlesPageComponent implements OnInit {
  articles$ = new BehaviorSubject<Article[]>([]);
  isLoading = true;

  constructor(private readonly articleService: ArticleService) {}

  ngOnInit(): void {
    this.articleService.getAllVerifiedArticles().subscribe({
      complete: () => (this.isLoading = false),
      next: (response) => this.articles$.next(response.articles),
    });
  }
}
