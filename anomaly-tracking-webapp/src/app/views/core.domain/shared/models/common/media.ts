import { LvfMediaType } from "../../enums/lvf-media-type";
import { IEntityBaseInfo, EntityBase } from "./entitybase";
import { environment } from "src/environments/environment";

/***
 * Represents the media object info.
 */
export interface IMediaInfo extends IEntityBaseInfo {

    EntityId: number;
    FileOriginalName: string;
    UID: string;
    LvfMediaType: LvfMediaType;
}

/**
 * @Class Media
 * @classdesc Represents a media object.
 */
export class Media extends EntityBase {

    EntityId: number;
    FileOriginalName: string;
    UID: string;
    LvfMediaType: LvfMediaType;

    /**
     * @constructor
     * Creates an instance of media.
     */
    constructor(info: IMediaInfo) {
        super(info);

        this.UID = info.UID;
        this.LvfMediaType = info.LvfMediaType || LvfMediaType.DOCUMENT;
        this.EntityId = info.EntityId
        this.FileOriginalName = info.FileOriginalName || `${environment.staticFilesUrl}/avatar.png`;
    }

    static createMediaInstance(info: IMediaInfo = null): Media {
  
        if (!info) {
            return new Media(<IMediaInfo>{
                LvfMediaType: LvfMediaType.DOCUMENT
            });
        }

        let media = new Media(<IMediaInfo>{
            Id: info.Id,
            UID: info.UID,
            LvfMediaType: info.LvfMediaType || LvfMediaType.DOCUMENT,
            EntityId: info.EntityId
        });

        if (info && info.FileOriginalName && info.FileOriginalName.indexOf('http') < 0) {
            media.FileOriginalName = `${environment.staticFilesUrl}/${info.FileOriginalName}`;
        } else {
            media.FileOriginalName = info.FileOriginalName;
        }

        return media;
    }
}
