import { NgbTooltipModule } from "@ng-bootstrap/ng-bootstrap";
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TranslateModule } from '@ngx-translate/core';
import { RouterModule } from '@angular/router';
import { SearchBarComponent } from "./search-bar.component";
import { FormsModule } from "@angular/forms";


@NgModule({
  exports: [
    SearchBarComponent,
    TranslateModule,
    RouterModule,
  ],
  declarations: [SearchBarComponent],
  imports: [
    CommonModule,
    TranslateModule, 
    NgbTooltipModule,
    RouterModule,
    FormsModule
  ]
})
export class SearchBarModule { }
