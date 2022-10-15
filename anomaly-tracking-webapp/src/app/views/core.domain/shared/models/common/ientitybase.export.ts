export interface IExportColumnStyle {

    fontSize : number;
    width : number;
   }

export interface IExportColumnData {

    widths : number[];
    titleKeys : string[];
    styles : IExportColumnStyle[];
}

export interface IExportCell {
    key : string;
    value : string;
    style : IExportColumnStyle;
}

export interface IEntityBaseExport {

    toCells(): IExportCell[] | null;
}