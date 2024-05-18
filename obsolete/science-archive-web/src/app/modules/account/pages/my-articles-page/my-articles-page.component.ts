import { Component, OnInit } from "@angular/core";
import { Article } from "@models/article/article";
import { LocalStorageService } from "@services/local-storage.service";
import { ArticleService } from "@services/article.service";
import { Router } from "@angular/router";
import { Category } from "@models/category/category";
import { CategoryService } from "@services/category.service";
import { IdentifiedUser } from "@models/user/identified-user";
import { DocumentStorageService } from "@services/document-storage.service";

@Component({
  selector: "app-my-articles-page",
  templateUrl: "./my-articles-page.component.html",
  styleUrls: ["./my-articles-page.component.scss"],
})
export class MyArticlesPageComponent implements OnInit {
  isLoading = true;
  showEditModal = false;
  showDeleteModal = false;
  createNew = false;

  articles: Article[] = [];
  categories: Category[] = [];

  articleToDelete: Article | null = null;
  currentArticle!: Article;
  selectedCategory: Category | null = null;

  currentUser: IdentifiedUser | null;

  constructor(
    private router: Router,
    private localStorageService: LocalStorageService,
    private articleService: ArticleService,
    private categoryService: CategoryService,
    private documentStorageService: DocumentStorageService
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

    this.currentArticle = this.createEmptyArticle();
  }

  createEmptyArticle(): Article {
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

  onCreateNewClick() {
    this.showEditModal = true;
    this.createNew = true;
    this.currentArticle = this.createEmptyArticle();
  }

  onEditModalClose() {
    this.showEditModal = false;
    this.createNew = false;
  }

  onDeleteModalClose() {
    this.showDeleteModal = false;
    this.articleToDelete = null;
  }

  onDeleteArticleSelect(article: Article) {
    this.showDeleteModal = true;
    this.articleToDelete = article;
  }

  onDeleteArticle() {
    if (!this.articleToDelete) {
      alert("Article to delete is not presented");
      return;
    }

    this.articleService.deleteArticle(this.articleToDelete!.id!).subscribe({
      complete: () => this.ngOnInit(),
      next: () => (this.showDeleteModal = false),
      error: (err) => alert(err),
    });
  }

  onArticleDocumentUpload(event: any) {
    const files = event.target?.files;

    if (!files || files.length === 0) {
      alert("No file was presented");
      return;
    }

    const fileToUpload = files[0] as File;

    if (!fileToUpload) {
      alert("No file was presented");
      return;
    }

    this.documentStorageService.uploadDocument(fileToUpload).subscribe({
      next: (res) => this.currentArticle.documentsPaths.push(res.path),
      error: (err) => alert(err),
    });
  }

  onRemoveDocument(path: string) {
    this.currentArticle.documentsPaths = this.currentArticle.documentsPaths.filter((p) => p !== path);
  }

  onSaveClick() {
    if (this.createNew) {
      this.articleService.createArticle(this.currentArticle).subscribe({
        complete: () => this.ngOnInit(),
        next: () => (this.showEditModal = false),
        error: (err) => alert(err),
      });
    } else {
      if (!this.currentArticle.id) {
        alert("Current article to update does not have ID");
        return;
      }

      this.articleService.updateArticle(this.currentArticle.id, this.currentArticle).subscribe({
        complete: () => this.ngOnInit(),
        next: () => (this.showEditModal = false),
        error: (err) => alert(err),
      });
    }
  }
}
