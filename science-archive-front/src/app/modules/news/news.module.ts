import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { NewsPageComponent } from "./pages/news-page/news-page.component";
import { NewsCardComponent } from "./components/news-card/news-card.component";
import { NzCardModule } from "ng-zorro-antd/card";
import { NzButtonModule } from "ng-zorro-antd/button";
import { NewsCardSkeletonComponent } from "./components/news-card-skeleton/news-card-skeleton.component";
import { NzSkeletonModule } from "ng-zorro-antd/skeleton";
import { ArticlesModule } from "@modules/articles/articles.module";
import { NzEmptyModule } from "ng-zorro-antd/empty";

@NgModule({
  declarations: [NewsPageComponent, NewsCardComponent, NewsCardSkeletonComponent],
  imports: [CommonModule, NzCardModule, NzButtonModule, NzSkeletonModule, ArticlesModule, NzEmptyModule]
})
export class NewsModule {}
