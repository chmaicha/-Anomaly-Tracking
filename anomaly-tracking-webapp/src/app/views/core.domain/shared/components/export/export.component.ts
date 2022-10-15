import { Component, OnInit } from '@angular/core';
import { IgxExcelExporterOptions, IgxExcelExporterService, IgxCsvExporterService, IgxCsvExporterOptions, CsvFileTypes } from 'igniteui-angular';
import { PdfmakeService } from './pdfmake.service';
import { ExportService } from './export.service';

@Component({
  selector: 'app-export',
  templateUrl: './export.component.html',
  styleUrls: ['./export.component.scss']
})
export class ExportComponent implements OnInit {

  constructor(
    public exportService: ExportService,
    private excelExportService: IgxExcelExporterService,
    private csvExportService: IgxCsvExporterService,
    private pdfmake: PdfmakeService,
  ) { }

  ngOnInit() { }

  public toExcel() {
    this.exportService.exportConfig.excelHandler();
    this.excelExportService.exportData(this.exportService.exportConfig.dataTable.rows, new IgxExcelExporterOptions(`${this.exportService.exportConfig.fileName}.xlsx`));
  }

  public toCSV() {
    this.exportService.exportConfig.excelHandler();
    let csvOptions = new IgxCsvExporterOptions(`${this.exportService.exportConfig.fileName}.csv`, CsvFileTypes.CSV);
    csvOptions.valueDelimiter = ';';
    this.csvExportService.exportData(this.exportService.exportConfig.dataTable.rows, csvOptions);
  }

  public toPdf() {
    this.exportService.exportConfig.pdfHandler();
    this.pdfmake.create();
    this.pdfmake.configureStyles({ header: { fontSize: 16, bold: false } });
    this.pdfmake.addText(this.exportService.exportConfig.dataTable.title, 'header');
    this.pdfmake.addText("\n");
    this.pdfmake.addImage('');
    this.pdfmake.addTable(this.exportService.exportConfig.dataTable);
    this.pdfmake.download(this.exportService.exportConfig.dataTable.title);
  }
}
