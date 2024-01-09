import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { ArticlesPageComponent } from "./pages/articles-page/articles-page.component";
import { ArticleCardComponent } from "./components/article-card/article-card.component";
import { NzCardModule } from "ng-zorro-antd/card";
import { NzButtonModule } from "ng-zorro-antd/button";
import { ArticleCardSkeletonComponent } from "./components/article-card-skeleton/article-card-skeleton.component";
import { NzSkeletonModule } from "ng-zorro-antd/skeleton";

@NgModule({
  declarations: [ArticlesPageComponent, ArticleCardComponent, ArticleCardSkeletonComponent],
  imports: [CommonModule, NzCardModule, NzButtonModule, NzSkeletonModule],
})
export class ArticlesModule {}
