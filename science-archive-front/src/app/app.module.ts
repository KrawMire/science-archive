import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";

import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { NZ_I18N, NzI18nPipe } from "ng-zorro-antd/i18n";
import { NgOptimizedImage } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { HttpClientModule } from "@angular/common/http";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { IconsProviderModule } from "./icons-provider.module";
import { NzLayoutModule } from "ng-zorro-antd/layout";
import { NzMenuModule } from "ng-zorro-antd/menu";
import { ContentPageComponent } from "@pages/content-page/content-page.component";
import { WelcomePageComponent } from "@pages/welcome-page/welcome-page.component";
import { ArticlesModule } from "@modules/articles/articles.module";
import { CategoriesModule } from "@modules/categories/categories.module";
import { NewsModule } from "@modules/news/news.module";
import { NzDrawerModule } from "ng-zorro-antd/drawer";
import { NzTypographyModule } from "ng-zorro-antd/typography";
import { NzAffixModule } from "ng-zorro-antd/affix";
import { AuthPageComponent } from "@pages/auth-page/auth-page.component";
import { AccountPageComponent } from "@pages/account-page/account-page.component";
import { AuthModule } from "@modules/auth/auth.module";
import { AccountModule } from "@modules/account/account.module";
import { NzButtonModule } from "ng-zorro-antd/button";
import { NzToolTipModule } from "ng-zorro-antd/tooltip";
import { NzBreadCrumbModule } from "ng-zorro-antd/breadcrumb";
import { CookieService } from "ngx-cookie-service";
import { AdminModule } from "@modules/admin/admin.module";
import { SharedModule } from "@modules/shared/shared.module";
import { ruLocale } from "./locales/ru-locale";
import { enLocale } from "./locales/en-locale";

@NgModule({
  declarations: [AppComponent, AuthPageComponent, AccountPageComponent, ContentPageComponent, WelcomePageComponent],
  imports: [
    // Build-in Modules
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    IconsProviderModule,

    // Component library modules
    NzLayoutModule,
    NzMenuModule,
    NgOptimizedImage,

    // App modules
    ArticlesModule,
    AccountModule,
    AdminModule,
    AuthModule,
    CategoriesModule,
    NewsModule,
    NzDrawerModule,
    NzTypographyModule,
    NzAffixModule,
    NzButtonModule,
    NzToolTipModule,
    NzBreadCrumbModule,
    SharedModule,
    NzI18nPipe
  ],
  providers: [CookieService, {
    provide: NZ_I18N,
    useFactory: () => {
      console.log(navigator.language);
      switch (navigator.language) {
        case 'en':
          return enLocale;
        case 'ru':
          return ruLocale;
        default:
          return enLocale;
      }
    }
  }],
  bootstrap: [AppComponent],
})
export class AppModule {}
