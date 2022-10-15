import { TranslateModule } from "@ngx-translate/core";
import { EmptyListComponent } from "./empty-list.component";
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';



@NgModule({
  exports: [EmptyListComponent, CommonModule],
  declarations: [EmptyListComponent],
  imports: [
    CommonModule,
    TranslateModule
  ]
})
export class EmptyListModule { }
