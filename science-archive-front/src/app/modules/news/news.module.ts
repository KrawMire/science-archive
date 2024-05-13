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
import { NewsDetailsPageComponent } from './pages/news-details-page/news-details-page.component';
import { RouterLink } from "@angular/router";
import { NzBreadCrumbModule } from "ng-zorro-antd/breadcrumb";
import { NzListModule } from "ng-zorro-antd/list";
import { NzResultModule } from "ng-zorro-antd/result";

@NgModule({
  declarations: [NewsPageComponent, NewsCardComponent, NewsCardSkeletonComponent, NewsDetailsPageComponent],
  imports: [CommonModule, NzCardModule, NzButtonModule, NzSkeletonModule, ArticlesModule, NzEmptyModule, RouterLink, NzBreadCrumbModule, NzListModule, NzResultModule]
})
export class NewsModule {}
