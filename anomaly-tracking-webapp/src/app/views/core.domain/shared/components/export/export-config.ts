import { DataTable } from "./data-table";
import { DataRow } from "./data-row";
import { IExportHandler } from "./export.service";

/**
 * Ensures that components have to implement some methods to be able to export data.
 */
export interface IExportConfigInfo {
    csv: boolean;
    pdf: boolean;
    xlsx: boolean;
    dataTable: DataTable<DataRow | any>;
    fileName: string;
}

export class ExportConfig {

    public csv: boolean;
    public pdf: boolean;
    public xlsx: boolean;
    public fileName?: string;

    public dataTable: DataTable<DataRow | any>;

    public pdfHandler: () => void;
    public excelHandler: () => void;

    constructor(info: IExportConfigInfo) {
        this.csv = info.csv || true;
        this.pdf = info.pdf || true;
        this.xlsx = info.xlsx || true;
        this.fileName = info.fileName || "exportedfile";

        this.dataTable = DataTable.createInstance();
        this.dataTable.title = this.fileName;
    }

    static createInstance<T>(fileName: string = "", handlers: IExportHandler): ExportConfig {
        let config = new ExportConfig(<IExportConfigInfo>{
            fileName: fileName,
        });

        config.excelHandler = () => { handlers.excelHandler(); };
        config.pdfHandler = () => { handlers.pdfHandler(); };

        return config;
    }
}