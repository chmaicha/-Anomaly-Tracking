import {
  EntityBase,
  IEntityBaseInfo,
} from 'src/app/views/core.domain/shared/models/common/entitybase';
import { Face } from '../face/face';

/***
 * Represents the anomaly object info.
 */
export interface IAnomalyInfo extends IEntityBaseInfo {
  Faces: Face[];
  AnomalyTypeId: number;

}

/**
 * @Class anomaly
 * @classdesc Represents a anomaly object.
 */
export class Anomaly extends EntityBase {
    Faces: Face[];
    AnomalyTypeId: number;


  /**
   * @constructor
   * Creates an instance of project.
   */
  constructor(info: IAnomalyInfo) {
    super(info);

    this.Faces = info.Faces;
    this.AnomalyTypeId = info.AnomalyTypeId;

  }
}
