import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { AdminNewsPageComponent } from "./pages/admin-news-page/admin-news-page.component";
import { AdminNewsCardComponent } from "./components/admin-news-card/admin-news-card.component";
import { SafeHtmlPipe } from "../../pipes/safe-html.pipe";
import SharedModule from "@modules/shared/shared.module";
import { FormsModule } from "@angular/forms";
import { AdminArticlesPageComponent } from './pages/admin-articles-page/admin-articles-page.component';

@NgModule({
  declarations: [AdminNewsPageComponent, AdminNewsCardComponent, AdminArticlesPageComponent],
  imports: [CommonModule, SafeHtmlPipe, SharedModule, SharedModule, FormsModule],
})
export class AdminModule {}
