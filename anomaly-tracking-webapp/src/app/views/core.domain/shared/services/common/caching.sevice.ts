import { Injectable } from "@angular/core";
import { LocalStoreService } from "src/app/shared/services/local-store.service";
import { environment } from "src/environments/environment";
import { SearchFilterBase } from "../../filters/search-filter-base";
import { IEntityBaseInfo } from "../../models/common/entitybase";
import { IResponse } from "../../models/common/iexception";

/**
 * Represents the cache entry for a given application's module.
 */
export interface ICacheEntry<T extends IEntityBaseInfo> {

  // Contains the last requested entities list into IResponse.
  response: IResponse<T[]>;

  // Contains the last retrieved page's number.
  filter: SearchFilterBase;
}

@Injectable({
  providedIn: 'root'
})
export class CachingService {

  private db: { [key: string]: ICacheEntry<any> } = {};

  /**
   * Creates a new instance of CachingService.
   */
  constructor(private localService: LocalStoreService) { }

  contains(key: string): boolean {

    if (environment.caching.inMemory) {
      return this.db[key] != null;
    }

    let entry = this.localService.getItem(key);
    return entry != null;
  }

  get<T extends IEntityBaseInfo>(key: string): ICacheEntry<T> {

    if (!key) {
      return null;
    }

    if (environment.caching.inMemory) {
      return this.db[key];
    }

    let entry = this.localService.getItem(key);
    return entry;
  }

  store<T extends IEntityBaseInfo>(key: string, filter: SearchFilterBase, response: IResponse<T[]>) {

    let entry = <ICacheEntry<T>>{ filter: filter ?? SearchFilterBase.CreateInstance(), response: response ?? { IsSuccessful: true, Data: [] } };

    if (environment.caching.inMemory) {
      this.db[key] = entry;
      return;
    }

    this.localService.setItem(key, entry);
  }

  append<T extends IEntityBaseInfo>(key: string, entity: T) {

    if (!this.contains(key)) {
      this.store<T>(key, null, null);
      // DO NOT STORE entity because it is not an array of data. This could be cause caching issues
      // return;
    }

    entity.Entire = true;

    let entry = this.get(key);
    let entityIndex = entry.response.Data.findIndex(e => e.Id == entity.Id);

    // Removes the old instance if exists
    if (entityIndex > -1) {
      entry.response.Data.splice(entityIndex, 1);
    } else {
      // Count the new added instance
      entry.filter.TotalCount += 1;
    }

    // Adds instance at the start of the array
    entry.response.Data.unshift(entity);

    // Stores the new array of entities
    this.store(key, entry.filter, entry.response);
  }

  remove(key: string, entityIds: number[]) {

    if (!entityIds || entityIds.length == 0) {
      return;
    }

    let entry = this.get(key);
    if(!entry){
      return;
    }

    // Removes all the deleting ids
    let data = [];
    entry.response.Data.map(entity => {
      if (!entityIds.includes(entity.Id)) {
        data.push(entity);
      }
    });

    entry.response.Data = data;

    // Forces reloading data to be sure to get instances from back end for the next request
    entry.filter.UseCache = false;

    // Stores the new array of entities
    this.store(key, entry.filter, entry.response);
  }

  forceReload<T extends IEntityBaseInfo>(key: string) {

    // Forces reloading data to be sure to get instances from the following page
    this.store<T>(key, null, null);
  }
}
