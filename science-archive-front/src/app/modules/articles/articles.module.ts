import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { ArticlesPageComponent } from "./pages/articles-page/articles-page.component";
import { ArticleCardComponent } from "./components/article-card/article-card.component";
import { NzCardModule } from "ng-zorro-antd/card";
import { NzButtonModule } from "ng-zorro-antd/button";
import { ArticleCardSkeletonComponent } from "./components/article-card-skeleton/article-card-skeleton.component";
import { NzSkeletonModule } from "ng-zorro-antd/skeleton";
import { NzTagModule } from "ng-zorro-antd/tag";
import { RouterLink } from "@angular/router";
import { ArticlePageComponent } from './pages/article-page/article-page.component';
import { NzBreadCrumbModule } from "ng-zorro-antd/breadcrumb";
import { NzListModule } from "ng-zorro-antd/list";
import { NzTypographyModule } from "ng-zorro-antd/typography";
import { NzEmptyModule } from "ng-zorro-antd/empty";
import { BrowserModule } from "@angular/platform-browser";
import { NzResultModule } from "ng-zorro-antd/result";
import { SharedModule } from "@modules/shared/shared.module";
import { NzTooltipDirective } from "ng-zorro-antd/tooltip";
import { NzI18nPipe } from "ng-zorro-antd/i18n";

@NgModule({
  imports: [BrowserModule, CommonModule, NzCardModule, NzButtonModule, NzSkeletonModule, NzTagModule, RouterLink, NzBreadCrumbModule, NzListModule, NzTypographyModule, NzEmptyModule, NzResultModule, SharedModule, NzTooltipDirective, NzI18nPipe],
  declarations: [ArticlesPageComponent, ArticleCardComponent, ArticleCardSkeletonComponent, ArticlePageComponent],
  exports: [ArticleCardComponent, ArticlePageComponent],
})
export class ArticlesModule {}
