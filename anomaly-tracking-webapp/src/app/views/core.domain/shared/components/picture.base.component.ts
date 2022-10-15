import { IMediaInfo, Media } from "../models/common/media";
import { Observable, of } from "rxjs";
import { EntityBase } from "../models/common/entitybase";
import { IBaseDetailComponentInfo, BaseDetailComponent } from "./base.detail.component";
import { environment } from "src/environments/environment";
import { MediaUploadService } from "./media/media-upload/media-upload.service";
import { EntityMediaBase } from "../models/common/entitymedia.base";
import { IResponse } from "../models/common/iexception";

export interface IPictureBaseComponentInfo extends IBaseDetailComponentInfo {
    mediaUploadService: MediaUploadService;
}

/**
 * @class Picture base component.
 * @classdesc Class that should be implemented by all the components that manages picture.
 */
export abstract class PictureBaseComponent<T extends EntityBase> extends BaseDetailComponent<T> {

    logo: string;
    avatar: string;
    formData: FormData;
    isUpdateProfile: boolean = false;
    fileData: File = null;
    previewFileLabel: string = null;

    private mediaUploadService: MediaUploadService;

    /**
     * @constructor
     * Creates a new instance of PictureBaseComponent.
     */
    constructor(info: IPictureBaseComponentInfo) {
        super(info);

        this.formData = null;
        this.mediaUploadService = info.mediaUploadService;
        this.logo = `${environment.staticFilesUrl}/avatar.png`;
        this.avatar = `${environment.staticFilesUrl}/useravatar.png`;
    }

    /**
     * Calls when selecting a picture on the current edit component.
     * @param formData : Selected picture
     */
    onPictureSelected(formData: FormData) {

        if (formData) {
            this.isUpdateProfile = true;
        }

        this.formData = formData;
    }

    onSavePicture(): Observable<IResponse<IMediaInfo>> {
        // Checks whether or not there are picture to upload
        if (!this.formData) {
            return of(<IResponse<IMediaInfo>>{
                Data: null,
                IsSuccessful: false,    // This allows to not update the entity's picture when there is no change to apply
            });
        }

        return this.mediaUploadService.uploadFile(this.formData);
    }

    onDeletePicture(entity: EntityMediaBase, media: Media) {
        entity.Media = media;
        entity.MediaId = media.Id;
    }

    public fileProgress(fileInput: any) {
        this.fileData = <File>fileInput.target.files[0];
        this.previewFileLabel = this.fileData.name;
        this.onUploadFile();
    }

    public onUploadFile() {

        if (this.fileData) {
            this.formData = new FormData();
            this.formData.append(this.fileData.type, this.fileData, this.fileData.name);
        }
    }
}