import { Component, OnInit } from "@angular/core";
import { Article } from "@models/article/article";
import { ArticleService } from "@services/article.service";
import { ActivatedRoute } from "@angular/router";

@Component({
  selector: "app-article-details-page",
  templateUrl: "./article-details-page.component.html",
  styleUrls: ["./article-details-page.component.scss"],
})
export class ArticleDetailsPageComponent implements OnInit {
  article?: Article;

  constructor(private readonly articleService: ArticleService, private readonly route: ActivatedRoute) {}

  ngOnInit() {
    const articleId = this.route.snapshot.paramMap.get("id");

    if (!articleId) {
      alert("Invalid article ID!");
      return;
    }

    this.articleService.getArticleById(articleId).subscribe({
      next: (response) => (this.article = response.article),
      error: (err) => alert(err),
    });
  }

  openArticleDocument(path: string) {
    window.open("/api/documents/" + path);
  }
}
