import { EntityBase, IEntityBaseInfo,} from 'src/app/views/core.domain/shared/models/common/entitybase';

/***
 * Represents the product object info.
 */
export interface IProductInfo extends IEntityBaseInfo {
  Ref: string;

}

/**
 * @Class product
 * @classdesc Represents a product object.
 */
export class Product extends EntityBase {
  Ref: string;
 


  /**
   * @constructor
   * Creates an instance of project.
   */
  constructor(info: IProductInfo) {
    super(info);

    this.Ref = info.Ref;
    
  }
}
