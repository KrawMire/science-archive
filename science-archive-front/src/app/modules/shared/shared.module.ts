import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DateFormatPipe } from './pipes/date-format.pipe';



@NgModule({
  declarations: [
    DateFormatPipe
  ],
  exports: [
    DateFormatPipe
  ],
  imports: [
    CommonModule
  ]
})
export class SharedModule { }
