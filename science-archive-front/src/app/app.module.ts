import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";

import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { NZ_I18N } from "ng-zorro-antd/i18n";
import { en_US } from "ng-zorro-antd/i18n";
import { NgOptimizedImage, registerLocaleData } from "@angular/common";
import en from "@angular/common/locales/en";
import { FormsModule } from "@angular/forms";
import { HttpClientModule } from "@angular/common/http";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { IconsProviderModule } from "./icons-provider.module";
import { NzLayoutModule } from "ng-zorro-antd/layout";
import { NzMenuModule } from "ng-zorro-antd/menu";
import { ContentPageComponent } from "./pages/content-page/content-page.component";
import { WelcomePageComponent } from "./pages/welcome-page/welcome-page.component";
import { ArticlesModule } from "./modules/articles/articles.module";
import { CategoriesModule } from "./modules/categories/categories.module";
import { NewsModule } from "./modules/news/news.module";
import { NzDrawerModule } from "ng-zorro-antd/drawer";
import { NzTypographyModule } from "ng-zorro-antd/typography";

registerLocaleData(en);

@NgModule({
  declarations: [AppComponent, ContentPageComponent, WelcomePageComponent],
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
    CategoriesModule,
    NewsModule,
    NzDrawerModule,
    NzTypographyModule,
  ],
  providers: [{ provide: NZ_I18N, useValue: en_US }],
  bootstrap: [AppComponent],
})
export class AppModule {}
