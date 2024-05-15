import { Component, OnInit } from "@angular/core";
import { ArticleApiService } from "@services/article-api.service";
import { BehaviorSubject, map } from "rxjs";
import { Article, ArticleDocument } from "@models/article/article";
import { NzMessageService } from "ng-zorro-antd/message";
import { FormControl, FormGroup, NonNullableFormBuilder, Validators } from "@angular/forms";
import { CategoryApiService } from "@services/category-api.service";
import { Category } from "@models/category/category";
import { Subcategory } from "@models/category/subcategory";
import { NzUploadFile } from "ng-zorro-antd/upload";
import { ContentStorageService } from "@services/content-storage-api.service";
import { User } from "@models/user/user";
import { AuthService } from "@modules/auth/services/auth.service";
import { Router } from "@angular/router";

@Component({
  selector: 'sar-user-articles-page',
  templateUrl: './user-articles-page.component.html',
  styleUrls: ['./user-articles-page.component.scss']
})
export class UserArticlesPageComponent implements OnInit {
  showCreateNewModal = false;
  fileList: NzUploadFile[] = [];
  isLoadingArticleList$: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(true);
  user$ = new BehaviorSubject<User | null>(null);
  documents$ = new BehaviorSubject<ArticleDocument[]>([]);
  articles$ = new BehaviorSubject<Article[]>([]);
  categories$ = new BehaviorSubject<Category[]>([]);
  subcategories$ = new BehaviorSubject<Subcategory[]>([]);
  isCreatingNewArticle$ = new BehaviorSubject<boolean>(false);

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
    authService: AuthService,
    private readonly router: Router,
    private readonly fb: NonNullableFormBuilder,
    private readonly messageService: NzMessageService,
    private readonly articleService: ArticleApiService,
    private readonly categoryService: CategoryApiService,
    private readonly contentStorageService: ContentStorageService,
  ) {
    this.user$.next(authService.getCurrentUser());
    this.validateForm.get('categoryId')?.valueChanges.subscribe(categoryId => {
      if (!categoryId) {
        return;
      }

      this.validateForm.controls.subcategoryId.reset();

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

  async onCreateNewArticle() {
    if (!this.validateForm.valid) {
      Object.values(this.validateForm.controls).forEach(control => {
        if (control.invalid) {
          control.markAsDirty();
          control.updateValueAndValidity({ onlySelf: true });
        }
      });
      return;
    }

    if (!this.user$.value) {
      this.messageService.error("This action if only for authorized users!");
      await this.router.navigate(["/auth"]);
    }

    this.isCreatingNewArticle$.next(true);

    const newArticle: Article = {
      categoryId: this.validateForm.controls.subcategoryId.value,
      categoryName: "",
      title: this.validateForm.controls.title.value,
      description: this.validateForm.controls.description.value,
      authors: [
        {
          userId: this.user$.value?.id as string,
          name: this.user$.value?.name as string,
          role: 0
        }
      ],
      documents: this.documents$
        .value
        .filter(doc => this.fileList.find(fl => fl.uid === doc.id))
        .map((doc) => ({
          id: undefined,
          name: doc.name,
          path: doc.path
        })),
      status: 0
    };

    this.articleService
      .createArticle(newArticle)
      .subscribe({
        next: () => {
          this.messageService.success("Article was successfully created");
          this.getArticles();
          this.showCreateNewModal = false;
          this.validateForm.reset();
        },
        error: (error) => {
          if (!error.status || error.status === 500) {
            this.messageService.error("Unhandled error occurred. Try again later");
            console.log(error);
          }

          if (error.status === 400) {
            this.messageService.error(error.message ?? error);
          }
        }
      });
  }

  beforeUpload(file: NzUploadFile): boolean {
    console.log(this.fileList);

    if (!file) {
      return false;
    }

    file.status = "uploading";
    this.fileList = this.fileList.concat(file);

    this.contentStorageService
      .uploadDocument(file)
      .subscribe({
        next: (response) => {
          this.messageService.success("Document was successfully uploaded");
          const documents = this.documents$.value;
          documents.push({
            id: file.uid,
            name: file.name,
            path: response.path,
          });
          this.documents$.next(documents);

          this.fileList = this.fileList.map((f) => {
            if (file.uid === f.uid) {
              f.status = "success";
            }

            return f;
          });
        },
        error: (error) => {
          console.log(error);
          this.messageService.error("Can not upload document");

          this.fileList = this.fileList.map((f) => {
            if (file.uid === f.uid) {
              f.status = "error";
            }

            return f;
          });
        }
      });

    return false;
  };

  ngOnInit(): void {
    this.getArticles();
    this.getCategories();
  }

  getArticles() {
    this.articleService
      .getProfileArticles()
      .pipe(
        map((response) => {
          return response.articles.map((article) => {
            article.creationDate = article.creationDate ? new Date(article.creationDate.toString()) : new Date();
            return article;
          });
        }),
      )
      .subscribe({
        next: (articles) => {
          this.articles$.next(articles);
          this.isLoadingArticleList$.next(false);
        },
        error: () => {
          this.messageService.error("An error occurred");
          this.isLoadingArticleList$.next(false);
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
