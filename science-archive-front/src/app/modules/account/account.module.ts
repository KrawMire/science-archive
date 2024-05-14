import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProfilePageComponent } from './pages/profile-page/profile-page.component';
import { UserArticlesPageComponent } from './pages/user-articles-page/user-articles-page.component';



@NgModule({
  declarations: [
    ProfilePageComponent,
    UserArticlesPageComponent
  ],
  imports: [
    CommonModule
  ]
})
export class AccountModule { }
