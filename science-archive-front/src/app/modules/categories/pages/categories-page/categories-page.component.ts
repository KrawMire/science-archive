import { Component, OnInit } from "@angular/core";
import { BehaviorSubject } from "rxjs";
import { Category } from "@models/category/category";
import { CategoryService } from "@services/category.service";
import { NzMessageService } from "ng-zorro-antd/message";
import { Title } from "@angular/platform-browser";

@Component({
  selector: "sar-categories-page",
  templateUrl: "./categories-page.component.html",
  styleUrls: ["./categories-page.component.scss"],
})
export class CategoriesPageComponent implements OnInit {
  isLoading$ = new BehaviorSubject<boolean>(true);
  categories$ = new BehaviorSubject<Category[]>([]);

  constructor(
    private readonly categoryService: CategoryService,
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
        this.message.error(error);
      }
    });
  }
}
