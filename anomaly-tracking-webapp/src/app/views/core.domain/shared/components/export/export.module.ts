import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ExportComponent } from './export.component';
import {TranslateModule } from '@ngx-translate/core';
import { IgxExcelExporterService, IgxCsvExporterService } from 'igniteui-angular';
import { PdfmakeService } from './pdfmake.service';
import { NgbPopoverModule, NgbDropdownModule, NgbTooltipModule } from '@ng-bootstrap/ng-bootstrap';

@NgModule({
    exports: [
        ExportComponent,
        FormsModule,
        TranslateModule,
    ],
    declarations: [ExportComponent],
    imports: [
        CommonModule,
        FormsModule,
        TranslateModule,
        NgbPopoverModule,
        NgbTooltipModule
    ],
    providers: [IgxExcelExporterService, IgxCsvExporterService, PdfmakeService]
})
export class ExportModule { }
