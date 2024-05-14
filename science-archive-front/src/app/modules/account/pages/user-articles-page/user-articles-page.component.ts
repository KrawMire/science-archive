import { Component, OnInit } from "@angular/core";
import { ArticleApiService } from "@services/article-api.service";
import { BehaviorSubject } from "rxjs";
import { Article } from "@models/article/article";
import { NzMessageService } from "ng-zorro-antd/message";
import { NonNullableFormBuilder, Validators } from "@angular/forms";
import { CategoryApiService } from "@services/category-api.service";
import { Category } from "@models/category/category";

@Component({
  selector: 'sar-user-articles-page',
  templateUrl: './user-articles-page.component.html',
  styleUrls: ['./user-articles-page.component.scss']
})
export class UserArticlesPageComponent implements OnInit {
  showCreateNewModal = false;
  articles$ = new BehaviorSubject<Article[]>([]);
  categories$ = new BehaviorSubject<Category[]>([]);

  validateForm = this.fb.group({
    title: ['', Validators.required],
    description: [null],
    category: [null]
  })

  constructor(
    private readonly fb: NonNullableFormBuilder,
    private readonly messageService: NzMessageService,
    private readonly articleService: ArticleApiService,
    private readonly categoryService: CategoryApiService
  ) {}

  openCreateNewModal() {
    this.showCreateNewModal = true;
  }

  onCancelCreateNewArticle() {
    this.showCreateNewModal = false;
  }

  onCreateNewArticle() {
    this.showCreateNewModal = false;
    this.validateForm.reset();
  }

  ngOnInit(): void {
    this.getArticles();
    this.getCategories();
  }

  getArticles() {
    this.articleService
      .getProfileArticles()
      .subscribe({
        next: (response) => {
          this.articles$.next(response.articles);
        },
        error: () => {
          this.messageService.error("An error occurred");
        }
      });
  }

  getCategories() {
    this.categoryService
      .getAllCategories()
      .subscribe({
        next: (response) => {
          this.categories$.next(response.categories);
        },
        error: () => {
          this.messageService.error("An error occurred");
        }
      });
  }
}
