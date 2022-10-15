import { Injectable } from "@angular/core";
import { ExportConfig } from "./export-config";
import { EntityBase } from "../../models/common/entitybase";
import { DataRow } from "./data-row";
import { DataCell } from "./data-cell";
import { TranslateService } from "@ngx-translate/core";

export interface IExportHandler {

    excelHandler: () => void;
    pdfHandler: () => void;
}

@Injectable({
    providedIn: "root"
})
export class ExportService {

    public exportConfig: ExportConfig;

    constructor(private translateService: TranslateService) { }

    configure(documentTitleKey: string, entityBase: EntityBase, handlers: IExportHandler) {

        // Instanciates object instance
        let fileName = this.translateService.instant(documentTitleKey);
        this.exportConfig = ExportConfig.createInstance(fileName, handlers);

        // Builds header cells
        let headerCells = [];
        entityBase.toCells().map(cell => {
            headerCells.push(DataCell.create(this.translateService.instant(cell.key), { fillColor: '#cecece', fontSize: cell.style.fontSize }))
        });
        this.exportConfig.dataTable.headers = DataRow.create(headerCells);

        // Set column widths
        this.exportConfig.dataTable.widths = [];
        entityBase.toCells().map(cell => {
            this.exportConfig.dataTable.widths.push(cell.style.width);
        });
    }

    loadPdfData(entities: EntityBase[]) {
        this.exportConfig.dataTable.rows = [];

        entities.filter(entity => entity.Selected).map(entity => {
            let cells: DataCell[] = [];
            
            entity.toCells().map(cell => {

                let cellValue = (!cell.value || cell.value.toString().trim() == '') ? '' : this.translateService.instant(cell.value);
                cells.push(DataCell.create(cellValue, cell.style));
            });
            
            this.exportConfig.dataTable.rows.push(DataRow.create(cells));
        });
    }

    loadXlsData(entities: EntityBase[]) {
        this.exportConfig.dataTable.rows = [];

        entities.filter(entity => entity.Selected).map(entity => {
            let row = {};
            entity.toCells().map(cell => {
                let cellValue = (!cell.value || cell.value.toString().trim() == '') ? '' : this.translateService.instant(cell.value);
                row[this.translateService.instant(cell.key)] = cellValue;
            });
            this.exportConfig.dataTable.rows.push(row);
        });
    }
}