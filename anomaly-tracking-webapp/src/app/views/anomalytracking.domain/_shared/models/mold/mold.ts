import {
  EntityBase,
  IEntityBaseInfo,
} from 'src/app/views/core.domain/shared/models/common/entitybase';
import { Cavity } from '../cavity/cavity';

/***
 * Represents the mold object info.
 */
export interface IMoldInfo extends IEntityBaseInfo {
  Label: string;
  ClientId: number;
  Cavities: Cavity[];
}

/**
 * @Class mold
 * @classdesc Represents a mold object.
 */
export class Mold extends EntityBase {
  Label: string;
  ClientId: number;
  Cavities: Cavity[];

  /**
   * @constructor
   * Creates an instance of project.
   */
  constructor(info: IMoldInfo) {
    super(info);
    this.Label = info.Label;
    this.ClientId = info.ClientId;
    this.Cavities = info.Cavities;
  }
}
