import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { ProfilePageComponent } from './pages/profile-page/profile-page.component';
import { MyArticlesPageComponent } from './pages/my-articles-page/my-articles-page.component';

@NgModule({
  declarations: [
    ProfilePageComponent,
    MyArticlesPageComponent
  ],
  imports: [CommonModule],
})
export class AccountModule {}
