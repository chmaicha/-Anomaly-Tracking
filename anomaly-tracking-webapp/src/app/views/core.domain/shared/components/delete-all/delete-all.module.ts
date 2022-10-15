import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TranslateModule } from '@ngx-translate/core';
import { NgbPopoverModule, NgbTooltipModule } from '@ng-bootstrap/ng-bootstrap';
import { DeleteAllComponent } from './delete-all.component';

@NgModule({
  declarations: [DeleteAllComponent],
  exports: [DeleteAllComponent],
  imports: [
    CommonModule,
    TranslateModule,
    NgbTooltipModule,
  ]
})
export class DeleteAllModule { }
