export interface IDataCellInfo {

    content: string;
    style: any | string;
    htmlContent?: string;
    color?: string;
    index?: number;
}

export class DataCell {

    content: string;
    style: any | string;
    htmlContent?: string;
    color?: string;
    index?: number;

    constructor(info: IDataCellInfo) {
        this.index = info.index;
        this.style = info.style;
        this.content = info.content;
        this.htmlContent = info.htmlContent;
        this.color = info.color;
    }

    get(): any {
        return { text: this.content, style: this.style };
    }

    static create(content: string, style: any | string): DataCell {
        let cell = new DataCell(<IDataCellInfo>{
            content: content,
            style: style
        });

        return cell;
    }
}
