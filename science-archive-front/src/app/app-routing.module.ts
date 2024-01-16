import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { ContentPageComponent } from "@pages/content-page/content-page.component";
import { WelcomePageComponent } from "@pages/welcome-page/welcome-page.component";
import { ArticlesPageComponent } from "@modules/articles/pages/articles-page/articles-page.component";
import { NewsPageComponent } from "@modules/news/pages/news-page/news-page.component";
import { CategoriesPageComponent } from "@modules/categories/pages/categories-page/categories-page.component";
import { AccountPageComponent } from "@pages/account-page/account-page.component";
import { AuthPageComponent } from "@pages/auth-page/auth-page.component";

const routes: Routes = [
  { path: "", pathMatch: "full", redirectTo: "welcome" },
  { path: "welcome", pathMatch: "full", component: WelcomePageComponent },
  { path: "account", pathMatch: "full", component: AccountPageComponent },
  { path: "auth", pathMatch: "full", component: AuthPageComponent },
  {
    path: "content",
    component: ContentPageComponent,
    children: [
      {
        path: "",
        pathMatch: "full",
        redirectTo: "articles",
      },
      {
        path: "articles",
        component: ArticlesPageComponent,
      },
      {
        path: "categories",
        component: CategoriesPageComponent,
      },
      {
        path: "news",
        component: NewsPageComponent,
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
