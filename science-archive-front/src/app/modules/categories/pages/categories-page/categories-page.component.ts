import { Component, OnInit } from "@angular/core";
import { BehaviorSubject } from "rxjs";
import { Category } from "@models/category/category";
import { CategoryApiService } from "@services/category-api.service";
import { NzMessageService } from "ng-zorro-antd/message";
import { Title } from "@angular/platform-browser";
import { NzI18nService } from "ng-zorro-antd/i18n";

@Component({
  selector: "sar-categories-page",
  templateUrl: "./categories-page.component.html",
  styleUrls: ["./categories-page.component.scss"],
})
export class CategoriesPageComponent implements OnInit {
  isLoading$ = new BehaviorSubject<boolean>(true);
  categories$ = new BehaviorSubject<Category[]>([]);

  constructor(
    private readonly i18nService: NzI18nService,
    private readonly categoryService: CategoryApiService,
    private readonly message: NzMessageService,
    titleService: Title) {
    titleService.setTitle("Science Archive - Categories");
  }

  ngOnInit(): void {
    this.categoryService.getAllCategories().subscribe({
      complete: () => (this.isLoading$.next(false)),
      next: (response) => this.categories$.next(response.categories),
      error: (error) => {
        this.isLoading$.next(false);
        this.categories$.next([]);
        console.log(error);
        this.message.error(this.i18nService.translate("commonErrors.unhandledError"));
      }
    });
  }
}
