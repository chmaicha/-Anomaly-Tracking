import { DataCell, IDataCellInfo } from "./data-cell";

export interface IDataRowInfo {

    index: number;
    cells: DataCell[];
}

export class DataRow {

    index: number;
    cells: DataCell[];

    /**
     * @constructor
     * Creates a new instance of DataRow.
     */
    constructor(info: IDataRowInfo) {
        this.index = info.index;
        this.cells = info.cells || [];
    }

    static create(cells: DataCell[]): DataRow {
        let row = new DataRow(<IDataRowInfo>{
            cells: cells
        });

        return row;
    }

    get(): any {
        const row = [];
        for (const cell of this.cells) {
            row.push(cell.get());
        }

        return row;
    }
}