import { EntityBase } from "../models/common/entitybase";
import { SearchFilterBase } from "../filters/search-filter-base";
import { BaseComponent, IBaseComponentInfo } from "./base.component";
import { IEnumEntry } from "../_helpers/enum.helper";
import { TranslateService } from "@ngx-translate/core";
import { Router } from "@angular/router";

export interface IBaseListComponentConfig extends IBaseComponentInfo {

    translateService?: TranslateService;
}

export abstract class BaseListComponent<TEntity extends EntityBase, KFilter extends SearchFilterBase> extends BaseComponent<TEntity> {

    filter: KFilter;
    indices: number[] = [];
    translateService?: TranslateService;

    /**
     * @constructor
     * @abstract
     * Default constructor of BaseListComponent.
     */
    constructor(config: IBaseListComponentConfig) {
        super(config);

        this.translateService = config.translateService;
    }

    cleanEntitiesArray(entities: EntityBase[]) {
        this.indices.map(id => {
            let index = entities.findIndex(po => po.Id == id);
            entities.splice(index, 1);
        });
    }

    sortEntityBase(c1: EntityBase, c2: EntityBase, property: string): number {
        if (c1[property] > c2[property]) return 1;
        if (c1[property] < c2[property]) return -1;
        return 0;
    }

    applyFilters<T>(filter: string, statuses: number[], lvfToFilter: IEnumEntry<T>[]): [string, number[]] {
        statuses = [];

        let filterFirstState = filter;

        lvfToFilter.filter(lvtf => lvtf.selected).map((e, index) => {

            if (index == 0) {
                filter = this.translateService.instant(`${e.label}`);
            }

            statuses.push(+e.value);
        });

        if (filter != filterFirstState && statuses.length > 1) {
            filter += ` (+${statuses.length - 1})`;
        }
        return [filter, statuses];
    }

    reloadCurrentRoute(router : Router) {
      let currentUrl = router.url;
      router.navigateByUrl('/', {skipLocationChange: true}).then(() => {
          router.navigate([currentUrl]);
      });
    }
}
