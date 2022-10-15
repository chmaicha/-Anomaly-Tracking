import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MediaDownloadComponent } from './media-download.component';
import { PerfectScrollbarModule } from 'ngx-perfect-scrollbar';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { NgxPaginationModule } from 'ngx-pagination';
import { ToolbarModule } from '../../toolbar/toolbar.module';
import { RouterModule } from '@angular/router';
import { TranslateModule } from '@ngx-translate/core';
import { NgbTooltipModule, NgbPopoverModule } from '@ng-bootstrap/ng-bootstrap';

@NgModule({
  imports: [
    CommonModule,
    PerfectScrollbarModule,
    ReactiveFormsModule,
    NgbPopoverModule,
    NgxPaginationModule,
    ToolbarModule,
    RouterModule,
    FormsModule,
    TranslateModule,
    NgbTooltipModule,
  ],
  declarations: [MediaDownloadComponent],
  exports: [MediaDownloadComponent]
})
export class FileDownloadModule { }
