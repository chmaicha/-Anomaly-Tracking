import { Observable } from "rxjs";
import { IMediaInfo, Media } from "../../models/common/media";
import { MediaFilter } from "../../filters/media.filter";
import { IResponse } from "../../models/common/iexception";

/**
 * @interface
 * Interface that provides all the available operations accross the module list components.
 */
export interface IMediaBaseService {
   

    /**
     * Gets the document of a module.
     * @param filter The filter to apply to get the documents of the module
     */
    getDocuments(filter: MediaFilter): Observable<IResponse<IMediaInfo[]>>;

    /**
     * Adds documents to a give nmodule.
     * @param entityId The identifier of the given module
     * @param media The document to add
     */
    addDocument(entityId: number, media: Media): Observable<IResponse<IMediaInfo>>;

    /**
     * Deletes the file of the structure
     * @param entityMediaId The identifier of the given file to delete
     */
    deleteDocument(entityMediaId: number): Observable<IResponse<number>>;
/**
     * Deletes the all file of the structure
     * @param indices The identifier of the given file to delete
     */
    deleteDocuments(indices: number[]) : Observable<IResponse<number[]>>;
}