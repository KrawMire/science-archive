import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";

import { AuthPageComponent } from "@pages/auth-page/auth-page.component";
import { MainPageComponent } from "@pages/main-page/main-page.component";
import { ArticlesPageComponent } from "@modules/main/pages/articles-page/articles-page.component";
import { AuthorsPageComponent } from "@modules/main/pages/authors-page/authors-page.component";
import { CategoriesPageComponent } from "@modules/main/pages/categories-page/categories-page.component";
import { NewsPageComponent } from "@modules/main/pages/news-page/news-page.component";
import { NewsDetailsPageComponent } from "@modules/main/pages/news-details-page/news-details-page.component";

const routes: Routes = [
  { path: "", pathMatch: "full", redirectTo: "main" },
  { path: "auth", component: AuthPageComponent },
  {
    path: "main",
    component: MainPageComponent,
    children: [
      { path: "", pathMatch: "full", redirectTo: "articles" },
      {
        path: "articles",
        component: ArticlesPageComponent,
      },
      {
        path: "authors",
        component: AuthorsPageComponent,
      },
      {
        path: "categories",
        component: CategoriesPageComponent,
      },
      {
        path: "news",
        component: NewsPageComponent,
      },
      {
        path: "news/:id",
        component: NewsDetailsPageComponent,
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
