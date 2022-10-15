import { DataRow } from "./data-row";

export interface IDataTableInfo<T> {

    index: number;
    title: string;
    headers: T;
    rows: T[];
    widths?: number[];
    heigth?: number;
    description?: string;
}

export class DataTable<T> {

    index: number;
    title: string;
    headers: T;
    rows: T[];
    widths?: number[];
    description: string;

    /**
     * @constructor
     * Creates a new instance of DataTable.
     */
    constructor(info: IDataTableInfo<T>) {
        this.index = info.index;
        this.title = info.title;
        this.headers = info.headers;
        this.rows = info.rows || [];
        this.widths = info.widths || [];
        this.description = info.description;
    }

    static createInstance<T>(title: string = ''): DataTable<T> {
        return new DataTable<T>(<IDataTableInfo<T>>{
            index: 0,
            description: '',
            title: title || '',
            rows: [],
            headers: null,
            widths: [],
        });
    }
}
