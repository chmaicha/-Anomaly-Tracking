import { EntityBase, IEntityBaseInfo } from "src/app/views/core.domain/shared/models/common/entitybase";

/**
 * Interface that represents anomalytypeprocess's information object.
 */
export interface IAnomalyTypeProcessInfo extends IEntityBaseInfo {



AnomalyTypeId:number;
ProcessId:number;

}

/**
 * @class AnomalyTypeProcess
 * @classdesc Represents a anomalyTypeProcess object.
 */
export class AnomalyTypeProcess extends EntityBase {


  AnomalyTypeId:number;
  ProcessId:number;


    /**
     * @constructor
     * Creates an instance of anomalyTypeProcess.
     */
    constructor(info: IAnomalyTypeProcessInfo) {
        super(info);
        this.AnomalyTypeId = info.AnomalyTypeId;
        this.ProcessId = info.ProcessId;



    }
}

