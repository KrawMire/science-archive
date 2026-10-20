import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { RouterLink } from "@angular/router";
import { NzI18nPipe } from "ng-zorro-antd/i18n";
import { SharedModule } from "@modules/shared/shared.module";
import { NewsPageComponent } from "./pages/news-page/news-page.component";
import { NewsCardComponent } from "./components/news-card/news-card.component";
import { NewsCardSkeletonComponent } from "./components/news-card-skeleton/news-card-skeleton.component";
import { NewsDetailsPageComponent } from './pages/news-details-page/news-details-page.component';

@NgModule({
  declarations: [NewsPageComponent, NewsCardComponent, NewsCardSkeletonComponent, NewsDetailsPageComponent],
  imports: [CommonModule, RouterLink, SharedModule, NzI18nPipe]
})
export class NewsModule {}
