import {
  EntityBase,
  IEntityBaseInfo,
} from 'src/app/views/core.domain/shared/models/common/entitybase';
import { AnomalyType } from '../anomalyType/anomalyType';

/**
 * Interface that represents process's information object.
 */
export interface IProcessInfo extends IEntityBaseInfo {
  process : Process ;
  Label: string;
  AnomalyTypes?: AnomalyType[];
}

/**
 * @class Process
 * @classdesc Represents a process object.
 */
export class Process extends EntityBase {
  Label: string;
  AnomalyTypes?: AnomalyType[];

  /**
   * @constructor
   * Creates an instance of process.
   */
  constructor(info: IProcessInfo) {
    super(info);

    this.Label = info.Label;
    this.AnomalyTypes = info.AnomalyTypes;
  }
}
