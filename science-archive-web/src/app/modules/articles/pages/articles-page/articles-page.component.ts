import { Component, OnInit } from "@angular/core";
import { Article } from "@models/article/article";
import { ArticleService } from "@services/article.service";

@Component({
  selector: "app-articles-page",
  templateUrl: "./articles-page.component.html",
  styleUrls: ["./articles-page.component.scss"],
})
export class ArticlesPageComponent implements OnInit {
  articles: Article[] = [];

  constructor(private articleService: ArticleService) {}

  ngOnInit() {
    this.articleService.getAllArticles().subscribe({
      next: (response) => {
        if (!response.success) {
          alert(response.error);
          return;
        }

        if (!response.data) {
          alert("Cannot get any data!");
          return;
        }

        this.articles = response.data!.articles;
      },
    });
  }
}
