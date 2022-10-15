import { LvfMonthOfYear } from "../enums/lvf-month";
import { NGBDatePicker } from "../models/common/ngbdatapicker";

export interface ISearchFilterBaseInfo {

    ApplicationId: number;
    IsAdmin?: boolean;
    ConnectedUserId?: number;
    Page?: number;
    PageSize?: number;
    Paginate?: boolean;
    TotalCount?: number;
    SearchInput?: string;
    StartDate?: NGBDatePicker;
    StartDateString?: string;
    EndDate?: NGBDatePicker;
    EndDateString?: string;
    Years?: number[];
    Months?: LvfMonthOfYear[];
    Statuses?: number[];
    EntityIds?: number[];
    UserIds?: number[];
}

/**
 * @class SearchFilterBase.
 * Represents the base filter class of all the filters.
 */
export class SearchFilterBase {

    ApplicationId: number;
    IsAdmin: boolean;
    ConnectedUserId?: number;
    Page: number;
    PageSize: number;
    TotalCount?: number;
    Paginate?: boolean;
    SearchInput?: string;
    StartDate?: NGBDatePicker;
    StartDateString?: string;
    EndDate?: NGBDatePicker;
    EndDateString?: string;
    Years?: number[];
    Months?: LvfMonthOfYear[];
    Statuses?: number[];
    EntityIds?: number[];
    UserIds?: number[];

    /** Ensures to exclude theses entities into the response. It contains a list of identifiers. */
    Excepts?: number[];

    /** Ensures to exclude theses entities into the response. It contains a list of identifiers. */
    Excludes?: number[];

    /** Indicates whether or not it should use the cache for data retrieval. */
    UseCache?: boolean;

    /**
     * Client side properties : these properties should not be added to server side SearchFilterBase DTO.
     * There are only used for client side purpose.
     */
    btnLoad?: boolean = false;

    /**
     * @constructor
     * Creates a new instance of SearchFilterBase.
     */
    constructor(info: ISearchFilterBaseInfo) {

        this.ApplicationId = info.ApplicationId;
        this.IsAdmin = info.IsAdmin ?? false;
        this.ConnectedUserId = info.ConnectedUserId;
        this.Page = info.Page ?? 1;
        this.PageSize = info.PageSize ?? 12;
        this.Paginate = info.Paginate;
        this.TotalCount = info.TotalCount ?? 0;
        this.UserIds = info.UserIds ?? [];
        this.SearchInput = info.SearchInput ?? '';
        this.StartDateString = info.StartDateString ?? "";
        this.EndDateString = info.EndDateString ?? "";
        this.Years = info.Years ?? [];
        this.Months = info.Months ?? [];
        this.Statuses = info.Statuses ?? [];
        this.EntityIds = info.EntityIds ?? [];
    }

    static CreateInstance(): SearchFilterBase {
        return new SearchFilterBase(<ISearchFilterBaseInfo>{
            TotalCount: 0,
        });
    }

    formatDate() {
        if (this.StartDate) {
            this.StartDateString = `${this.StartDate.day}/${this.StartDate.month}/${this.StartDate.year}`
        }

        if (this.EndDate) {
            this.EndDateString = `${this.EndDate.day}/${this.EndDate.month}/${this.EndDate.year}`
        }
    }
}

export interface IYearInfo {
    value: number;
    selected: boolean;
}
