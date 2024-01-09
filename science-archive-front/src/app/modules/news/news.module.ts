import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { NewsPageComponent } from "./pages/news-page/news-page.component";
import { NewsCardComponent } from "./components/news-card/news-card.component";
import { NzCardModule } from "ng-zorro-antd/card";
import { NzButtonModule } from "ng-zorro-antd/button";

@NgModule({
  declarations: [NewsPageComponent, NewsCardComponent],
  imports: [CommonModule, NzCardModule, NzButtonModule],
})
export class NewsModule {}
