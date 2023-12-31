import { Component, OnInit } from "@angular/core";
import { ArticleService } from "@services/article.service";
import { Article } from "@models/article/article";

@Component({
  selector: "app-admin-articles-page",
  templateUrl: "./admin-articles-page.component.html",
  styleUrls: ["./admin-articles-page.component.scss"],
})
export class AdminArticlesPageComponent implements OnInit {
  constructor(private articleService: ArticleService) {}

  showArticleModal = false;
  isLoading = true;
  isLoadingCurrentArticle = true;
  articles: Article[] = [];
  currentArticle: Article | null = null;

  ngOnInit(): void {
    this.articleService.getAllArticles().subscribe({
      complete: () => (this.isLoading = false),
      next: (response) => (this.articles = response.articles),
      error: (err) => alert(err),
    });
  }

  onArticleClick(id: string) {
    this.showArticleModal = true;

    this.articleService.getArticleById(id).subscribe({
      complete: () => (this.isLoadingCurrentArticle = false),
      next: (response) => (this.currentArticle = response.article),
      error: (err) => alert(err),
    });
  }

  onArticleModalClose() {
    this.showArticleModal = false;
    this.isLoadingCurrentArticle = true;
    this.currentArticle = null;
  }

  openAttachedFile(filename: string) {
    window.open("/api/documents/" + filename);
  }

  onApproveClick() {
    if (!this.currentArticle?.id) {
      alert("Cannot perform this action with not loaded article");
      return;
    }

    this.articleService.approveArticle(this.currentArticle.id).subscribe({
      next: () => {
        this.onArticleModalClose();
        this.ngOnInit();
      },
      error: (err) => alert(err),
    });
  }

  onDeclineClick() {
    if (!this.currentArticle?.id) {
      alert("Cannot perform this action with not loaded article");
      return;
    }

    this.articleService.declineArticle(this.currentArticle.id).subscribe({
      next: () => {
        this.onArticleModalClose();
        this.ngOnInit();
      },
      error: (err) => alert(err),
    });
  }
}
