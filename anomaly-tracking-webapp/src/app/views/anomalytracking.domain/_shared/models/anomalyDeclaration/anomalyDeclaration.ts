import {
  EntityBase,
  IEntityBaseInfo,
} from 'src/app/views/core.domain/shared/models/common/entitybase';
import { Cavity } from '../cavity/cavity';

/***
 * Represents the anomalyDeclaration object info.
 */
export interface IAnomalyDeclarationInfo extends IEntityBaseInfo {

  UserId: number;
  CavityId: number;
  ProcessId: number;
  AnomalyId?: number;
}

/**
 * @Class anomalyDeclaration
 * @classdesc Represents a anomalyDeclaration object.
 */
export class AnomalyDeclaration extends EntityBase {

  UserId: number;
  CavityId: number;
  ProcessId: number;
  AnomalyId?: number;

  /**
   * @constructor
   * Creates an instance of project.
   */
  constructor(info: IAnomalyDeclarationInfo) {
    super(info);
    this.UserId = info.UserId;
    this.CavityId = info.CavityId;
    this.ProcessId = info.ProcessId;
    this.AnomalyId = info.AnomalyId;
  }
}
