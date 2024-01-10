import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { CategoriesPageComponent } from "./pages/categories-page/categories-page.component";
import { CategoryCardComponent } from "./components/category-card/category-card.component";
import { SubcategoryCardComponent } from "./components/subcategory-card/subcategory-card.component";
import { NzCardModule } from "ng-zorro-antd/card";
import { NzButtonModule } from "ng-zorro-antd/button";
import { CategoryCardSkeletonComponent } from "./components/category-card-skeleton/category-card-skeleton.component";
import { NzSkeletonModule } from "ng-zorro-antd/skeleton";

@NgModule({
  declarations: [
    CategoriesPageComponent,
    CategoryCardComponent,
    SubcategoryCardComponent,
    CategoryCardSkeletonComponent,
  ],
  imports: [CommonModule, NzCardModule, NzButtonModule, NzSkeletonModule],
})
export class CategoriesModule {}
