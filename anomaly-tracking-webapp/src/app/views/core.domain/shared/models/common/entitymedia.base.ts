import { Media, IMediaInfo } from "./media";
import { IEntityBaseInfo, EntityBase } from "./entitybase";

export interface IEntityMediaBaseInfo extends IEntityBaseInfo {

    MediaId?: number;
    Media?: IMediaInfo;
    Documents?: IMediaInfo[];
    MediaCount?: number;
}

/**
 * @class Entity media base.
 * @classdesc Represents the base class shared by all entities with media.
 */
export class EntityMediaBase extends EntityBase {

    MediaId?: number;
    Media?: IMediaInfo;
    Documents?: Media[];
    MediaCount?: number;

    /**
     * @constructor
     * Creates an instance of entity media base.
     */
    constructor(info: IEntityMediaBaseInfo) {
        super(info);

        this.MediaId = info.MediaId;
        this.MediaCount = info.MediaCount || 0;
        this.Media = Media.createMediaInstance(info.Media);
        this.Documents = info.Documents ? info.Documents.map(d => new Media(d)) : [];
    }

    public static createInstance<T extends EntityMediaBase>(c: { new(info: IEntityMediaBaseInfo): T; }, info: IEntityMediaBaseInfo = null): T {
        info = info ? info : <IEntityMediaBaseInfo>{};

        return new c(info);
    }
}
