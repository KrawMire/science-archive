import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { RouterLink } from "@angular/router";
import { NzI18nPipe } from "ng-zorro-antd/i18n";
import { SharedModule } from "@modules/shared/shared.module";
import { ArticlesPageComponent } from "./pages/articles-page/articles-page.component";
import { ArticleCardComponent } from "./components/article-card/article-card.component";
import { ArticleCardSkeletonComponent } from "./components/article-card-skeleton/article-card-skeleton.component";
import { ArticlePageComponent } from './pages/article-page/article-page.component';

@NgModule({
  imports: [CommonModule, FormsModule, RouterLink, SharedModule, NzI18nPipe],
  declarations: [ArticlesPageComponent, ArticleCardComponent, ArticleCardSkeletonComponent, ArticlePageComponent],
  exports: [ArticleCardComponent, ArticleCardSkeletonComponent, ArticlePageComponent],
})
export class ArticlesModule {}
