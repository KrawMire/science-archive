import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProfilePageComponent } from './pages/profile-page/profile-page.component';
import { UserArticlesPageComponent } from './pages/user-articles-page/user-articles-page.component';
import { NzCardComponent } from "ng-zorro-antd/card";
import { NzDescriptionsComponent, NzDescriptionsItemComponent } from "ng-zorro-antd/descriptions";
import { NzSpinComponent } from "ng-zorro-antd/spin";
import { NzTagComponent } from "ng-zorro-antd/tag";
import { NzIconDirective } from "ng-zorro-antd/icon";
import { NzEmptyComponent } from "ng-zorro-antd/empty";
import { NzButtonComponent } from "ng-zorro-antd/button";
import { NzWaveDirective } from "ng-zorro-antd/core/wave";
import { NzModalModule } from "ng-zorro-antd/modal";
import { ArticlesModule } from "@modules/articles/articles.module";
import { NzColDirective, NzRowDirective } from "ng-zorro-antd/grid";
import { NzFormControlComponent, NzFormDirective, NzFormItemComponent, NzFormLabelComponent } from "ng-zorro-antd/form";
import { NzInputDirective, NzInputGroupComponent, NzTextareaCountComponent } from "ng-zorro-antd/input";
import { ReactiveFormsModule } from "@angular/forms";
import { NzOptionComponent, NzSelectComponent } from "ng-zorro-antd/select";
import { NzUploadComponent } from "ng-zorro-antd/upload";
import { UserArticleCardComponent } from "@modules/account/components/user-article-card/user-article-card.component";



@NgModule({
  declarations: [
    ProfilePageComponent,
    UserArticlesPageComponent
  ],
  imports: [
    CommonModule,
    NzCardComponent,
    NzDescriptionsComponent,
    NzDescriptionsItemComponent,
    NzSpinComponent,
    NzTagComponent,
    NzIconDirective,
    NzEmptyComponent,
    NzButtonComponent,
    NzWaveDirective,
    NzModalModule,
    ArticlesModule,
    NzColDirective,
    NzFormControlComponent,
    NzFormDirective,
    NzFormItemComponent,
    NzInputDirective,
    NzInputGroupComponent,
    NzRowDirective,
    ReactiveFormsModule,
    NzTextareaCountComponent,
    NzFormLabelComponent,
    NzSelectComponent,
    NzOptionComponent,
    NzUploadComponent,
    UserArticleCardComponent
  ]
})
export class AccountModule { }
