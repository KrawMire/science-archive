import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { RouterLink } from "@angular/router";
import { NzI18nPipe } from "ng-zorro-antd/i18n";
import { SharedModule } from "@modules/shared/shared.module";
import { CategoriesPageComponent } from "./pages/categories-page/categories-page.component";
import { CategoryCardComponent } from "./components/category-card/category-card.component";
import { SubcategoryCardComponent } from "./components/subcategory-card/subcategory-card.component";
import { CategoryCardSkeletonComponent } from "./components/category-card-skeleton/category-card-skeleton.component";

@NgModule({
  declarations: [
    CategoriesPageComponent,
    CategoryCardComponent,
    SubcategoryCardComponent,
    CategoryCardSkeletonComponent,
  ],
  imports: [CommonModule, FormsModule, RouterLink, NzI18nPipe, SharedModule]
})
export class CategoriesModule {}
