import { EntityBase } from "../models/common/entitybase";
import { ContextService } from "../services/common/context.service";

export interface IBaseComponentInfo {

  contextService: ContextService;
}

export abstract class BaseComponent<T extends EntityBase>  {

  contextService: ContextService;
  appModuleKey: string;
  btnLoad: boolean;

  /**
   * @constructor
   * @abstract
   * Default constructor of Base Component.
   */
  constructor(config: IBaseComponentInfo) {

    this.contextService = config.contextService;
    this.btnLoad = false;
  }

  checkIsAdmin(): boolean {
    return this.contextService.data.User.IsAdmin;
  }

  // compare(a: EntityBase, b: EntityBase, propertyName: string) {
  //   if (a[propertyName] < b[propertyName]) {
  //     return -1;
  //   }

  //   if (a[propertyName] > b[propertyName]) {
  //     return 1;
  //   }

  //   // a must be equal to b
  //   return 0;
  // }

}
