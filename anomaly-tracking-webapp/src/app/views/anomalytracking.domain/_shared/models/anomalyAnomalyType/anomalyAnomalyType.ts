import { EntityBase, IEntityBaseInfo,} from 'src/app/views/core.domain/shared/models/common/entitybase';

/***
 * Represents the anomalyAnomalyType object info.
 */
export interface IAnomalyAnomalyTypeInfo extends IEntityBaseInfo {
  AnomalyTypeId: number;
  AnomalyId: number;

}

/**
 * @Class anomalyAnomalyType
 * @classdesc Represents a anomalyAnomalyType object.
 */
export class AnomalyAnomalyType extends EntityBase {
  AnomalyTypeId: number;
  AnomalyId: number;


  /**
   * @constructor
   * Creates an instance of AnomalyAnomalyType.
   */
  constructor(info: IAnomalyAnomalyTypeInfo) {
    super(info);

    this.AnomalyId = info.AnomalyId;
    this.AnomalyTypeId = info.AnomalyTypeId;


  }
}
