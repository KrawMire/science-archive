import { Component, OnInit } from "@angular/core";
import { Article } from "@models/article/article";
import { LocalStorageService } from "@services/local-storage.service";
import { ArticleService } from "@services/article.service";
import { Router } from "@angular/router";
import { Category } from "@models/category/category";
import { CategoryService } from "@services/category.service";
import { IdentifiedUser } from "@models/user/identified-user";

@Component({
  selector: "app-my-articles-page",
  templateUrl: "./my-articles-page.component.html",
  styleUrls: ["./my-articles-page.component.scss"],
})
export class MyArticlesPageComponent implements OnInit {
  isLoading = true;
  showEditModal = false;
  articles: Article[] = [];
  categories: Category[] = [];
  createNew = false;

  currentArticle!: Article;
  selectedCategory: Category | null = null;

  currentUser: IdentifiedUser | null;

  constructor(
    private router: Router,
    private localStorageService: LocalStorageService,
    private articleService: ArticleService,
    private categoryService: CategoryService
  ) {
    this.currentUser = this.localStorageService.getCurrentUser();
  }

  ngOnInit(): void {
    if (!this.currentUser) {
      alert("This page is only for authorized users!");
      this.router.navigate(["main", "articles"]);
      return;
    }

    this.articleService.getMyArticles().subscribe({
      complete: () => (this.isLoading = false),
      next: (response) => (this.articles = response.articles),
      error: (err) => alert(err),
    });

    this.categoryService.getAllCategories().subscribe({
      next: (response) => (this.categories = response.categories),
      error: (err) => alert(err),
    });

    this.currentArticle = this.getEmptyArticle();
  }

  getEmptyArticle(): Article {
    return {
      title: "",
      description: "",
      authorsIds: [this.currentUser!.id],
      status: 0,
      categoryId: "",
      documentsPaths: [],
    };
  }

  onArticleEditSelect(article: Article) {
    this.currentArticle = article;
    this.createNew = false;
    this.showEditModal = true;
  }

  onCreateClick() {
    this.showEditModal = true;
    this.createNew = true;
    this.currentArticle = this.getEmptyArticle();
  }

  onEditModalClose() {
    this.showEditModal = false;
    this.createNew = false;
  }

  onSaveClick() {
    if (this.createNew) {
      this.articleService.createArticle(this.currentArticle).subscribe({
        next: () => (this.showEditModal = false),
        error: (err) => alert(err),
      });
    } else {
      if (!this.currentArticle.id) {
        alert("Current article to update does not have ID");
        return;
      }

      this.articleService.updateArticle(this.currentArticle.id, this.currentArticle).subscribe({
        next: () => (this.showEditModal = false),
        error: (err) => alert(err),
      });
    }
  }
}
