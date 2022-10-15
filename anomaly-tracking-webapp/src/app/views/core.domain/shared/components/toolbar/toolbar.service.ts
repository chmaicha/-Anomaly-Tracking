import { Injectable } from "@angular/core";

export interface IToolbarOperationHandler {
    onItemSelectedHandler: () => void;
    selectAllHandler: () => void;
    deleteAllHandler: () => void;
}

export interface IToolbarBtnConfig {
    style: string;
    buttonLabel: string;
    onButtonClick: () => void;
}

@Injectable({
    providedIn: "root"
})
export class ToolbarService {

    btnaddpath: string;
    btnAddLabel: string;
    btnAddType: 'add' | 'config' = 'add';

    custumButtons: IToolbarBtnConfig[];

    searchInput: string;
    searchInputPlaceHolder: string;

    allSelected: boolean;
    isSelected: boolean;
    isListEmpty: boolean;
    unavailableDelete: boolean;
    unavailableExport: boolean;
    unavailableAdd: boolean;
    unavailableSelection: boolean;

    onBtnAddClick: () => void;
    onItemSelectedHandler: (isSelected: boolean) => void;
    onUpdateToolbarConfig: (isListEmpty: boolean) => void;
    selectAllHandler: () => void;
    deleteAllHandler: () => void;
    onBtnSearchClick: () => void;

    private configuration: IToolbarOperationHandler;
    private allSelectedInit: boolean;
    private isSelectedInit: boolean;
    deletionQuestion: string;

    constructor() { }

    configure(btnaddpath: string, configuration: IToolbarOperationHandler, isPopup: boolean = false, btnAddLabel: string = 'Nouvel item', globalSearchInputPlaceHolder: string = 'app.shared.searchbyfield') {
        this.btnAddType = 'add';
        this.btnAddLabel = btnAddLabel ? btnAddLabel : this.btnAddLabel;
        this.btnaddpath = btnaddpath ? btnaddpath : this.btnaddpath;
        this.searchInputPlaceHolder = globalSearchInputPlaceHolder ? globalSearchInputPlaceHolder : this.searchInputPlaceHolder;

        // Stores toolbar configuration if not popup in order to re-apply it after closing the modal popup (media updload modal for example)
        if (!isPopup) this.configuration = Object.assign({}, configuration);

        this.onItemSelectedHandler = () => { configuration.onItemSelectedHandler(); };
        this.selectAllHandler = () => { configuration.selectAllHandler(); };
        this.deleteAllHandler = () => { configuration.deleteAllHandler(); };
    }

    /**
     * Stores the buttons to add to the toolbar.
     * @param buttonConfig 
     */
    addButton(buttonConfig: IToolbarBtnConfig) {
        this.custumButtons = this.custumButtons ?? [];

        // Check whether the array contains elements or not and store the first element if the array is empty.
        if (!this.custumButtons?.length) {
            this.custumButtons.push(buttonConfig);
        }

        // Check if there's already an element in the array with the same style as the buttonConfig to add and reject it if it is true.
        this.custumButtons?.map(cb => {
            if (cb.style == buttonConfig.style) {
                return;
            } else {
                this.custumButtons.push(buttonConfig);
            }
        });
    }

    /**
     * Stores the values ​​of the check box of the toolbar in order to reapply it after having closed the modal popup (media updload modal for example).
     * @param allSelected 
     * @param isSelected 
     */
    selectedInit(allSelected: boolean, isSelected: boolean) {

        this.allSelectedInit = allSelected;
        this.isSelectedInit = isSelected;
        this.unavailableExport = false;
        this.unavailableDelete = false;
    }

    restoreConfig(): void {

        this.onItemSelectedHandler = () => { this.configuration.onItemSelectedHandler(); };
        this.selectAllHandler = () => { this.configuration.selectAllHandler(); };
        this.deleteAllHandler = () => { this.configuration.deleteAllHandler(); };
        this.allSelected = this.allSelectedInit;
        this.isSelected = this.isSelectedInit;
        this.unavailableExport = false;

    }

    /**
     * Unchecks the boxes of the previous component.
     */
    unchecked() {

        this.isSelected = false;
        this.allSelected = false;
        this.allSelectedInit = false;
        this.isSelectedInit = false;
        this.unavailableDelete = false;
        this.unavailableExport = false;
        this.unavailableAdd = false;
    }
}