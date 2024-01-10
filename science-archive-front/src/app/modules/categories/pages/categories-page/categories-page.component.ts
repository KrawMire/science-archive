import { Component, OnInit } from "@angular/core";
import { BehaviorSubject } from "rxjs";
import { Category } from "@models/category/category";
import { CategoryService } from "@services/category.service";

@Component({
  selector: "sar-categories-page",
  templateUrl: "./categories-page.component.html",
  styleUrls: ["./categories-page.component.scss"],
})
export class CategoriesPageComponent implements OnInit {
  isLoading = true;
  categories$ = new BehaviorSubject<Category[]>([]);

  constructor(private readonly categoryService: CategoryService) {}

  ngOnInit(): void {
    this.categoryService.getAllCategories().subscribe({
      complete: () => (this.isLoading = false),
      next: (response) => this.categories$.next(response.categories),
    });
  }
}
