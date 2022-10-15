export interface IButtonLoadingInfo {

    Text: string;
    Loading: boolean;
}

/**
 * Button loading helper.
 */
export class ButtonLoading {

    Text: string;
    Loading: boolean;

    /**
     * @constructor
     * Creates a new instance of ButtonLoading.
     */
    constructor(info: IButtonLoadingInfo) {
        this.Text = info.Text;
        this.Loading = info.Loading || false;
    }
}