import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink, RouterLinkActive } from "@angular/router";
import { NzSpinComponent } from "ng-zorro-antd/spin";
import { NzI18nPipe } from "ng-zorro-antd/i18n";
import { DateFormatPipe } from './pipes/date-format.pipe';
import { InitialsPipe } from "./pipes/initials.pipe";
import { PluralPipe } from "./pipes/plural.pipe";
import { LoadingComponent } from "@modules/shared/components/loading/loading.component";
import { IconComponent } from "./components/icon/icon.component";
import { LogoComponent } from "./components/logo/logo.component";
import { SiteHeaderComponent } from "./components/site-header/site-header.component";
import { SiteFooterComponent } from "./components/site-footer/site-footer.component";
import { DocumentTileComponent } from "./components/document-tile/document-tile.component";
import { StatusTagComponent } from "./components/status-tag/status-tag.component";

const SHARED_DECLARATIONS = [
  LoadingComponent,
  DateFormatPipe,
  InitialsPipe,
  PluralPipe,
  IconComponent,
  LogoComponent,
  SiteHeaderComponent,
  SiteFooterComponent,
  DocumentTileComponent,
  StatusTagComponent,
];

@NgModule({
  declarations: SHARED_DECLARATIONS,
  exports: SHARED_DECLARATIONS,
  imports: [
    CommonModule,
    RouterLink,
    RouterLinkActive,
    NzSpinComponent,
    NzI18nPipe
  ]
})
export class SharedModule { }
