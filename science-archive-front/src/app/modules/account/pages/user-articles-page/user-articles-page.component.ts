import { Component, OnInit } from "@angular/core";
import { ArticleApiService } from "@services/article-api.service";
import { BehaviorSubject } from "rxjs";
import { Article } from "@models/article/article";
import { NzMessageService } from "ng-zorro-antd/message";
import { FormControl, FormGroup, NonNullableFormBuilder, Validators } from "@angular/forms";
import { CategoryApiService } from "@services/category-api.service";
import { Category } from "@models/category/category";
import { Subcategory } from "@models/category/subcategory";

@Component({
  selector: 'sar-user-articles-page',
  templateUrl: './user-articles-page.component.html',
  styleUrls: ['./user-articles-page.component.scss']
})
export class UserArticlesPageComponent implements OnInit {
  showCreateNewModal = false;
  articles$ = new BehaviorSubject<Article[]>([]);
  categories$ = new BehaviorSubject<Category[]>([]);
  subcategories$ = new BehaviorSubject<Subcategory[]>([]);

  validateForm: FormGroup<{
    title: FormControl<string>,
    categoryId: FormControl<string>,
    subcategoryId: FormControl<string>
    description: FormControl<string | null>,
  }> = this.fb.group({
    title: ['', Validators.required],
    categoryId: ['', Validators.required],
    subcategoryId: ['', Validators.required],
    description: ['', null],
  })

  constructor(
    private readonly fb: NonNullableFormBuilder,
    private readonly messageService: NzMessageService,
    private readonly articleService: ArticleApiService,
    private readonly categoryService: CategoryApiService
  ) {
    this.validateForm.get('categoryId')?.valueChanges.subscribe(categoryId => {
      if (!categoryId) {
        return;
      }
      let selectedCategory = this.categories$.value.find(category => category.id === categoryId);
      if (selectedCategory) {
        this.subcategories$.next(selectedCategory.subcategories);
      }
    });
  }

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
