import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { ArticlesPageComponent } from "./pages/articles-page/articles-page.component";
import { AuthorsPageComponent } from "./pages/authors-page/authors-page.component";
import { CategoriesPageComponent } from "./pages/categories-page/categories-page.component";
import { NewsPageComponent } from "./pages/news-page/news-page.component";
import { ArticleCardComponent } from "./components/articles/article-card/article-card.component";
import { AuthorCardComponent } from "./components/authors/author-card/author-card.component";
import { CategoryCardComponent } from "./components/categories/category-card/category-card.component";
import { SubcategoryCardComponent } from "./components/categories/subcategory-card/subcategory-card.component";
import { AuthorArticleCardComponent } from "./components/authors/author-article-card/author-article-card.component";
import { NewsCardComponent } from "./components/news/news-card/news-card.component";
import SharedModule from "@modules/shared/shared.module";

@NgModule({
  imports: [CommonModule, SharedModule, SharedModule, SharedModule],
  declarations: [
    ArticlesPageComponent,
    AuthorsPageComponent,
    CategoriesPageComponent,
    NewsPageComponent,
    ArticleCardComponent,
    AuthorCardComponent,
    CategoryCardComponent,
    SubcategoryCardComponent,
    AuthorArticleCardComponent,
    NewsCardComponent,
  ],
  exports: [CommonModule],
})
export default class MainModule {}
