import { EntityBase, IEntityBaseInfo,} from 'src/app/views/core.domain/shared/models/common/entitybase';

/***
 * Represents the face object info.
 */
export interface IFaceInfo extends IEntityBaseInfo {
  Label: string;
  Positions : string[];
}

/**
 * @Class face
 * @classdesc Represents a face object.
 */
export class Face extends EntityBase {
  Label: string;
  Positions: string[] ;

  /**
   * @constructor
   * Creates an instance of project.
   */
  constructor(info: IFaceInfo) {
    super(info);
    this.Label = info.Label;
    this.Positions = info.Positions;
  }
}
