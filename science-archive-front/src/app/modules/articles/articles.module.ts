import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { ArticlesPageComponent } from "./pages/articles-page/articles-page.component";
import { ArticleCardComponent } from "./components/article-card/article-card.component";
import { NzCardModule } from "ng-zorro-antd/card";
import { NzButtonModule } from "ng-zorro-antd/button";

@NgModule({
  declarations: [ArticlesPageComponent, ArticleCardComponent],
  imports: [CommonModule, NzCardModule, NzButtonModule],
})
export class ArticlesModule {}
