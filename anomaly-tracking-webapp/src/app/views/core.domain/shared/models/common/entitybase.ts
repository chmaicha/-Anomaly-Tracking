export interface IEntityBaseInfo {

  Id?: number;
  LastModificationDateString?: string;
  LastModificationDateTimeString?: string
  Selected?: boolean;
  ApplicationId?: number;
  IsActive?: boolean;
  IsCollapsed?: boolean;
}

/**
 * @class Entitybase
 * @classdesc Represents the base class shared by all entities.
 */
export class EntityBase {

  Id?: number;
  LastModificationDateString?: string;
  LastModificationDateTimeString?: string;
  Selected?: boolean;
  ApplicationId?: number;
  IsActive?: boolean;
  IsCollapsed?: boolean;


  /**
   * @constructor
   * Creates a new instance of EntityBase.
   */
  constructor(info: IEntityBaseInfo) {
    this.Id = info.Id;
    this.ApplicationId = info.ApplicationId;
    this.LastModificationDateTimeString = info.LastModificationDateTimeString;
    this.Selected = info.Selected ?? false;
    this.IsCollapsed = info.IsCollapsed ?? false;
  }

  public static createInstance<T extends EntityBase>(c: { new(info: IEntityBaseInfo): T; }, info: IEntityBaseInfo = null): T {
    info = info ? info : <IEntityBaseInfo>{};

    return new c(info);
  }
}
