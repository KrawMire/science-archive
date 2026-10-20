import { Component, OnInit } from "@angular/core";
import { BehaviorSubject } from "rxjs";
import { Category } from "@models/category/category";
import { CategoryApiService } from "@services/category-api.service";
import { Title } from "@angular/platform-browser";

@Component({
  selector: "sar-categories-page",
  templateUrl: "./categories-page.component.html",
  styleUrls: ["./categories-page.component.scss"],
})
export class CategoriesPageComponent implements OnInit {
  isLoading$ = new BehaviorSubject<boolean>(true);
  hasError = false;
  searchText = "";

  private categories: Category[] = [];
  visibleCategories: Category[] = [];

  constructor(
    private readonly categoryService: CategoryApiService,
    titleService: Title) {
    titleService.setTitle("Science Archive - Categories");
  }

  ngOnInit(): void {
    this.categoryService.getAllCategories().subscribe({
      next: (response) => {
        this.categories = response.categories;
        this.applySearch();
        this.isLoading$.next(false);
      },
      error: (error) => {
        console.log(error);
        this.hasError = true;
        this.isLoading$.next(false);
      }
    });
  }

  applySearch(): void {
    const query = this.searchText.trim().toLowerCase();

    if (!query) {
      this.visibleCategories = this.categories;
      return;
    }

    // A matching field keeps all of its subjects; otherwise only the matching subjects are shown
    this.visibleCategories = this.categories
      .map(category => category.name.toLowerCase().includes(query)
        ? category
        : { ...category, subcategories: category.subcategories.filter(sub => sub.name.toLowerCase().includes(query)) })
      .filter(category => category.subcategories.length > 0 || category.name.toLowerCase().includes(query));
  }
}
