import {
  EntityBase,
  IEntityBaseInfo,
} from 'src/app/views/core.domain/shared/models/common/entitybase';

/***
 * Represents the cavity object info.
 */
export interface ICavityInfo extends IEntityBaseInfo {
  Label: string;
  MoldId: string;
}

/**
 * @Class cavity
 * @classdesc Represents a cavity object.
 */
export class Cavity extends EntityBase {
  Label: string;
  MoldId: string;

  /**
   * @constructor
   * Creates an instance of project.
   */
  constructor(info: ICavityInfo) {
    super(info);

    this.Label = info.Label;
    this.MoldId = info.MoldId;
  }
}
