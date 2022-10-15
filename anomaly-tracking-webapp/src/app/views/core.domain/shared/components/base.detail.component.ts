import { EntityBase } from "../models/common/entitybase";
import { RegexHelper } from "../_helpers/regex.helper";
import { DetailHelper } from "../_helpers/shared.detail.helper";
import { BaseComponent, IBaseComponentInfo } from "./base.component";

export interface IBaseDetailComponentInfo extends IBaseComponentInfo {

}

export abstract class BaseDetailComponent<T extends EntityBase> extends BaseComponent<T> {

    hasInvalidDependency: boolean;
    btnLoad: boolean;
    regexHelper = RegexHelper;
    detailHelper = DetailHelper;

    /**
     * @constructor
     * @abstract
     * Default constructor of BaseDetailComponent.
     */
    constructor(info: IBaseDetailComponentInfo) {
        super(info);

        this.hasInvalidDependency = false;
    }
}