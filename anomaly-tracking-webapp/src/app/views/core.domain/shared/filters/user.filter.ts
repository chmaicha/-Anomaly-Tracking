import { LvfUserJob } from "../enums/lvf-user-job";
import { SearchFilterBase } from "./search-filter-base";

/**
 * @class User filter.
 */
export class UserFilter extends SearchFilterBase {

    LvfUserJobs?: LvfUserJob[] = [];
}
