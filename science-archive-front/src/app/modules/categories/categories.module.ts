import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { CategoriesPageComponent } from "./pages/categories-page/categories-page.component";
import { CategoryCardComponent } from "./components/category-card/category-card.component";
import { SubcategoryCardComponent } from "./components/subcategory-card/subcategory-card.component";
import { NzCardModule } from "ng-zorro-antd/card";
import { NzButtonModule } from "ng-zorro-antd/button";

@NgModule({
  declarations: [CategoriesPageComponent, CategoryCardComponent, SubcategoryCardComponent],
  imports: [CommonModule, NzCardModule, NzButtonModule],
})
export class CategoriesModule {}
