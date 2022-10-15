import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs/internal/Observable";
import { IUserInfo, User } from "src/app/views/anomalytracking.domain/_shared/models/user/user";
import { LvfOperationType } from "src/app/views/core.domain/shared/enums/lvf-operation-type";
import { IResponse } from "src/app/views/core.domain/shared/models/common/iexception";
import { ServiceBase } from "src/app/views/core.domain/shared/services/common/base.service";
import { environment } from "src/environments/environment";
import { IAuthenticationData } from "../models/authentification-data";
import { tap } from 'rxjs/operators';
import { DialogService } from "src/app/views/core.domain/shared/services/common/dialog.service";


@Injectable({
    providedIn: 'root',
  })
  export class AuthentificationService extends ServiceBase<IUserInfo> {
    private url =
      'AnomalyTracking.WebServices/API/Authentifications/ServiceAuthentificationWeb.svc/api/users';
  
    constructor(
      http: HttpClient,
      dialogService: DialogService
      ) {
      super(http, dialogService);
    }

    login(user: User): Observable<IResponse<IAuthenticationData>> {
        const completeUrl = `${environment.apiUrl}/${this.url}`;
    
        return this.http.post<IResponse<IAuthenticationData>>(completeUrl, user, this.getHttpOptions(LvfOperationType.LOG_IN))
        .pipe(tap((response) => {this.dialogService.Show(response); }));
    }

}