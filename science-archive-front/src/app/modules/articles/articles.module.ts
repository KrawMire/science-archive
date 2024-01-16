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

@NgModule({
  declarations: [ArticlesPageComponent, ArticleCardComponent, ArticleCardSkeletonComponent],
  imports: [CommonModule, NzCardModule, NzButtonModule, NzSkeletonModule, NzTagModule, RouterLink],
  exports: [ArticleCardComponent],
})
export class ArticlesModule {}
