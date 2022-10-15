import { Injectable } from '@angular/core';
import { SearchFilterBase, ISearchFilterBaseInfo } from '../../filters/search-filter-base';

@Injectable({
  providedIn: 'root'
})
export class FilterService {


  getFilterBase(paginate: boolean = false): SearchFilterBase {
    return new SearchFilterBase(<ISearchFilterBaseInfo><unknown>{
        Paginate: paginate,
        EntityIds: [],
        Excepts: []
      });
  }
}
