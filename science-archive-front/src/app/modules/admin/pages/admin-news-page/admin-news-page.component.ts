import { Component } from '@angular/core';
import { NzCardComponent } from "ng-zorro-antd/card";
import { AdminArticleCardComponent } from "@modules/admin/components/admin-article-card/admin-article-card.component";
import { AsyncPipe, NgForOf, NgIf } from "@angular/common";
import { NzEmptyComponent } from "ng-zorro-antd/empty";
import { NzSpinComponent } from "ng-zorro-antd/spin";
import { AdminNewsCardComponent } from "@modules/admin/components/admin-news-card/admin-news-card.component";
import { BehaviorSubject, map } from "rxjs";
import { NzMessageService } from "ng-zorro-antd/message";
import { News } from "@models/news/news";
import { NewsApiService } from "@services/news-api.service";
import {
  FormControl,
  FormGroup,
  FormsModule,
  NonNullableFormBuilder,
  ReactiveFormsModule,
  Validators
} from "@angular/forms";
import { NzColDirective, NzRowDirective } from "ng-zorro-antd/grid";
import { NzFormControlComponent, NzFormDirective, NzFormItemComponent, NzFormLabelComponent } from "ng-zorro-antd/form";
import { NzIconDirective } from "ng-zorro-antd/icon";
import { NzInputDirective, NzInputGroupComponent, NzTextareaCountComponent } from "ng-zorro-antd/input";
import { NzModalComponent, NzModalContentDirective } from "ng-zorro-antd/modal";
import { NzOptionComponent, NzSelectComponent } from "ng-zorro-antd/select";
import { NzUploadComponent } from "ng-zorro-antd/upload";
import { NzButtonComponent } from "ng-zorro-antd/button";
import { NzWaveDirective } from "ng-zorro-antd/core/wave";
import { AuthService } from "@modules/auth/services/auth.service";
import { User } from "@models/user/user";
import { Router } from "@angular/router";
import CreateNewsRequest from "@models/news/requests/create-news.request";
import { NzI18nPipe } from "ng-zorro-antd/i18n";

@Component({
  selector: 'sar-admin-news-page',
  standalone: true,
  imports: [
    NzCardComponent,
    AdminArticleCardComponent,
    AsyncPipe,
    NgForOf,
    NgIf,
    NzEmptyComponent,
    NzSpinComponent,
    AdminNewsCardComponent,
    FormsModule,
    NzColDirective,
    NzFormControlComponent,
    NzFormDirective,
    NzFormItemComponent,
    NzFormLabelComponent,
    NzIconDirective,
    NzInputDirective,
    NzInputGroupComponent,
    NzModalComponent,
    NzOptionComponent,
    NzRowDirective,
    NzSelectComponent,
    NzTextareaCountComponent,
    NzUploadComponent,
    ReactiveFormsModule,
    NzButtonComponent,
    NzWaveDirective,
    NzModalContentDirective,
    NzI18nPipe
  ],
  templateUrl: './admin-news-page.component.html',
  styleUrl: './admin-news-page.component.scss'
})
export class AdminNewsPageComponent {
  showCreateNewModal = false;
  isLoadingNewsList$: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(true);
  user$ = new BehaviorSubject<User | null>(null);
  news$ = new BehaviorSubject<News[]>([]);
  isCreatingNews$ = new BehaviorSubject<boolean>(false);

  validateForm: FormGroup<{
    title: FormControl<string>,
    body: FormControl<string>,
  }> = this.fb.group({
    title: ['', Validators.required],
    body: ['', Validators.required],
  })

  constructor(
    authService: AuthService,
    private readonly router: Router,
    private readonly fb: NonNullableFormBuilder,
    private readonly messageService: NzMessageService,
    private readonly newsService: NewsApiService,
  ) {
    this.user$.next(authService.getCurrentUser());
  }

  ngOnInit(): void {
    this.getNews();
  }

  openCreateNewModal() {
    this.showCreateNewModal = true;
  }

  closeCreateNewModal() {
    this.showCreateNewModal = false;
  }

  async onCreateNews() {
    if (!this.validateForm.valid) {
      Object.values(this.validateForm.controls).forEach(control => {
        if (control.invalid) {
          control.markAsDirty();
          control.updateValueAndValidity({ onlySelf: true });
        }
      });
      return;
    }

    if (!this.user$.value?.id) {
      this.messageService.error("This action if only for authorized users!");
      await this.router.navigate(["/auth"]);
      return;
    }

    this.isCreatingNews$.next(true);

    const request: CreateNewsRequest = {
      news: {
        title: this.validateForm.controls.title.value,
        body: this.validateForm.controls.body.value,
        authorId: this.user$.value.id
      }
    };

    this.newsService
      .createNews(request)
      .subscribe({
        next: () => {
          this.isCreatingNews$.next(false);
          this.messageService.success("News were successfully created");
          this.getNews();
          this.showCreateNewModal = false;
          this.validateForm.reset();
        },
        error: (error) => {
          this.isCreatingNews$.next(false);
          this.messageService.error(error.message ?? error);
        }
      });
  }

  getNews() {
    this.news$.next([]);
    this.isLoadingNewsList$.next(true);

    this.newsService
      .getAllNews()
      .pipe(
        map((response) => {
          return response.news.map((singleNews) => {
            singleNews.creationDate = singleNews.creationDate ? new Date(singleNews.creationDate.toString()) : new Date();
            return singleNews;
          });
        }),
      )
      .subscribe({
        next: (singleNews) => {
          this.news$.next(singleNews);
          this.isLoadingNewsList$.next(false);
        },
        error: () => {
          this.messageService.error("An error occurred");
          this.isLoadingNewsList$.next(false);
        }
      });
  }
}
