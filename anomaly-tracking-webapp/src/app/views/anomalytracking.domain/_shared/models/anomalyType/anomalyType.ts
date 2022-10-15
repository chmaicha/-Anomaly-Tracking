import { EntityBase, IEntityBaseInfo } from "src/app/views/core.domain/shared/models/common/entitybase";

/**
 * Interface that represents anomalytype's information object.
 */
export interface IAnomalyTypeInfo extends IEntityBaseInfo {

  Label: string;
  ProcessId? : number;
}

/**
 * @class AnomalyType
 * @classdesc Represents a anomalytype object.
 */
export class AnomalyType extends EntityBase {

  Label: string;
  ProcessId?: number

    /**
     * @constructor
     * Creates an instance of anomalytype.
     */
    constructor(info: IAnomalyTypeInfo) {
        super(info);

        this.Label = info.Label;
        this.ProcessId = info.ProcessId;
    }
}

