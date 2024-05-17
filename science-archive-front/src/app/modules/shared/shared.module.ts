import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DateFormatPipe } from './pipes/date-format.pipe';
import { LoadingComponent } from "@modules/shared/components/loading/loading.component";
import { NzSpinComponent } from "ng-zorro-antd/spin";

@NgModule({
  declarations: [
    LoadingComponent,
    DateFormatPipe
  ],
  exports: [
    LoadingComponent,
    DateFormatPipe
  ],
  imports: [
    CommonModule,
    NzSpinComponent
  ]
})
export class SharedModule { }
