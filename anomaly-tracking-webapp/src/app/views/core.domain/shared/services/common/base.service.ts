import { HttpClient, HttpHeaders, HttpParams } from "@angular/common/http";
import { environment } from "src/environments/environment";
import { LvfOperationType } from "../../enums/lvf-operation-type";
import { LvfRequestSource } from "../../enums/lvf-request-src";
import { IEntityBaseInfo } from "../../models/common/entitybase";
import { DialogService } from "./dialog.service";


export interface IHttpOptions {
    headers?:
    | HttpHeaders
    | {
        [header: string]: string | string[];
    };
    observe: "events";
    params?:
    | HttpParams
    | {
        [param: string]: string | string[];
    };
    reportProgress?: boolean;
    responseType?: "json";
    withCredentials?: boolean;
}

export abstract class ServiceBase<TEntity extends IEntityBaseInfo> {

    /**
     * Creates a new instance of ServiceBase<T>.
     */
    constructor(
        protected http: HttpClient,
        protected dialogService: DialogService,
    ){
    }

    protected getHttpOptions(lvfOperationType: LvfOperationType, contentType: string = 'application/json') {
        return { headers: this.httpHeaders(lvfOperationType, contentType) };
    }

    private httpHeaders(lvfOperationType: LvfOperationType, contentType: string): HttpHeaders {
        let applicationUID = environment.appUID;

        return new HttpHeaders({
            "Content-Type": `${contentType}`,
            "X-APP-UID": `${applicationUID}`,
            "X-REQUEST-SRC": `${LvfRequestSource.WEB}`,
            "X-OPERATION-TYPE": `${lvfOperationType}`,
            "X-WEB-APP-URL": `${environment.webAppUrl}`,
        });
    }
}
