import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { ContentPageComponent } from "@pages/content-page/content-page.component";
import { WelcomePageComponent } from "@pages/welcome-page/welcome-page.component";
import { ArticlesPageComponent } from "@modules/articles/pages/articles-page/articles-page.component";
import { NewsPageComponent } from "@modules/news/pages/news-page/news-page.component";
import { CategoriesPageComponent } from "@modules/categories/pages/categories-page/categories-page.component";
import { AccountPageComponent } from "@pages/account-page/account-page.component";
import { AuthPageComponent } from "@pages/auth-page/auth-page.component";
import { ArticlePageComponent } from "@modules/articles/pages/article-page/article-page.component";
import { NewsDetailsPageComponent } from "@modules/news/pages/news-details-page/news-details-page.component";
import { SignInPageComponent } from "@modules/auth/pages/sign-in-page/sign-in-page.component";
import { SignUpPageComponent } from "@modules/auth/pages/sign-up-page/sign-up-page.component";
import { ProfilePageComponent } from "@modules/account/pages/profile-page/profile-page.component";
import { UserArticlesPageComponent } from "@modules/account/pages/user-articles-page/user-articles-page.component";

const routes: Routes = [
  { path: "", pathMatch: "full", redirectTo: "welcome" },
  { path: "welcome", pathMatch: "full", component: WelcomePageComponent },
  { path: "account", pathMatch: "full", component: AccountPageComponent },
  {
    path: "auth",
    component: AuthPageComponent,
    children: [
      {
        path: "",
        pathMatch: "full",
        redirectTo: "sign-in"
      },
      {
        path: "sign-in",
        component: SignInPageComponent,
      },
      {
        path: "sign-up",
        component: SignUpPageComponent,
      }
    ]
  },
  {
    path: "account",
    component: AccountPageComponent,
    children: [
      {
        path: "",
        pathMatch: "full",
        redirectTo: "profile",
      },
      {
        path: "profile",
        component: ProfilePageComponent,
      },
      {
        path: "user-articles",
        component: UserArticlesPageComponent
      }
    ]
  },
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
        path: "articles/:id",
        component: ArticlePageComponent
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
        component: NewsDetailsPageComponent
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
