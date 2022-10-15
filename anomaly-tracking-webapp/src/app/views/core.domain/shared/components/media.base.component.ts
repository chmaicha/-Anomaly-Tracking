import { environment } from "src/environments/environment";
import { IMediaFilterInfo, MediaFilter } from "../filters/media.filter";
import { SearchFilterBase } from "../filters/search-filter-base";
import { EntityMediaBase } from "../models/common/entitymedia.base";
import { IResponse } from "../models/common/iexception";
import { IMediaInfo, Media } from "../models/common/media";
import { IMediaBaseService } from "../services/common/media-base.service";
import { BaseListComponent, IBaseListComponentConfig } from "./base.list.component";
import { IMediaDownloaderHandler, MediaDownloadService } from "./media/media-download/media-download.service";

export interface IMediaBaseComponentConfig extends IBaseListComponentConfig {
    /**
     * This represents the actual module service. 
     * For example if the StructureListComponent is loaded, 
     * this contains StructureService with access only to the structure's media handling operations.
     */
    mediaService: IMediaBaseService;
    fileDownloadService: MediaDownloadService;
    isMainEntity?: boolean;
}

export abstract class MediaBaseComponent<TEntityMedia extends EntityMediaBase, KFilter extends SearchFilterBase> extends BaseListComponent<TEntityMedia, KFilter>{

    public mediaFilter: MediaFilter;
    public documents: Media[];
    public mediaService: IMediaBaseService;
    public mediaDownloadService: MediaDownloadService;
    public isMainEntity: boolean;
    public defaultAvatar = `${environment.staticFilesUrl}/avatar.png`;

    /**
     * Creates a new instance of MediaBaseComponent.
     * @param contextService : Application context.
     * @param lvfAppModule : Actual module.
     */
    constructor(config: IMediaBaseComponentConfig) {
        super(config);

        this.mediaFilter = new MediaFilter(<IMediaFilterInfo>{});

        this.mediaService = config.mediaService;
        this.isMainEntity = config.isMainEntity || true;
        this.mediaDownloadService = config.fileDownloadService;

        this.mediaDownloadService.registerHandler(<IMediaDownloaderHandler>{
            addDocument: (media: Media) => { this.addDocument(media); },
            deleteDocument: (mediaId: number) => { this.deleteDocument(mediaId); },
            getDocuments: (page: number) => { this.getDocuments(page); },
            deleteDocuments: () => { this.deleteDocuments() }

        });
    }

    public loadDocuments(entityId: number) {

        this.mediaDownloadService.folderOpen = true;
        this.mediaDownloadService.entityId = entityId;

        this.mediaFilter.EntityId = entityId;
        this.mediaFilter.Page = this.mediaDownloadService.page;
        this.mediaFilter.PageSize = this.mediaDownloadService.pageSize;

        this.mediaService.getDocuments(this.mediaFilter)
            .subscribe((response: IResponse<IMediaInfo[]>) => {
                if (response.IsSuccessful) {

                    this.mediaDownloadService.documents = response.Data ? response.Data.map(mi => new Media(mi)) : [];
                    this.mediaDownloadService.totalCount = response.TotalCount;
                }
            });
    }

    private addDocument(media: Media) {

        this.mediaService.addDocument(this.mediaDownloadService.entityId, media)
            .subscribe((response: IResponse<IMediaInfo>) => {

                if (response.IsSuccessful) {
                    this.mediaFilter.Page = 1;
                    this.mediaDownloadService.page = 1;
                    this.mediaService.getDocuments(this.mediaFilter)
                        .subscribe((response: IResponse<IMediaInfo[]>) => {
                            if (response.IsSuccessful) {

                                this.mediaDownloadService.documents = response.Data ? response.Data.map(mi => new Media(mi)) : [];
                                this.mediaDownloadService.totalCount = response.TotalCount;
                            }
                        });

                }
            });
    }

    private deleteDocument(mediaId: number) {

        this.mediaService.deleteDocument(mediaId)
            .subscribe((response: IResponse<number>) => {

                if (response.IsSuccessful) {
                    let index = this.mediaDownloadService.documents.findIndex(d => d.Id == mediaId);
                    this.mediaDownloadService.documents.splice(index, 1);
                }
            });
    }

    private deleteDocuments() {
        
        this.indices = [];
        this.mediaDownloadService.documents.filter(d => d.Selected).map(d => { this.indices.push(d.Id) });

        this.mediaService.deleteDocuments(this.indices)
            .subscribe((response: IResponse<number[]>) => {

                if (response.IsSuccessful) {

                    if (this.mediaDownloadService.documents.length - response.Data.length == 0) {
                        this.filter.Page--;
                    }
                    this.cleanEntitiesArray(this.mediaDownloadService.documents);
                }
            });
    }
    private getDocuments(page: number = 1) {

        this.mediaFilter.Page = page;
        this.mediaDownloadService.page = page;

        this.mediaService.getDocuments(this.mediaFilter)
            .subscribe((response: IResponse<IMediaInfo[]>) => {

                if (response.IsSuccessful) {
                    this.mediaDownloadService.documents = response.Data ? response.Data.map(mi => new Media(mi)) : [];
                    this.mediaDownloadService.totalCount = response.TotalCount;
                    this.mediaFilter.TotalCount = response.TotalCount;
                }
            });
    }
}
