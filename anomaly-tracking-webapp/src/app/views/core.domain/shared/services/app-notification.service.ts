import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IResponse } from '../models/common/iexception';
import { environment } from 'src/environments/environment';

import { Observable } from 'rxjs';
import { ServiceBase } from './common/base.service';
import { LvfAppModule } from '../enums/lvf-app-module';
import { LocalStoreService } from '../../../../shared/services/local-store.service';
import { DialogService } from './common/dialog.service';
import { tap } from 'rxjs/operators';
import { LvfOperationType } from '../enums/lvf-operation-type';
import { AppNotification, IAppNotificationInfo } from '../models/application/app-notification';

@Injectable({
    providedIn: 'root'
})
export class AppNotificationService extends ServiceBase<IAppNotificationInfo> {

    private url = 'CoreDomain.WebServices/API/Notifications/ServiceNotificationWeb.svc/api/notifications';

    constructor(http: HttpClient, localService: LocalStoreService, dialogService: DialogService) {
        super(LvfAppModule.APPLICATION_LOG, http, localService, dialogService, null);
    }

    update(appNotification: AppNotification): Observable<IResponse<IAppNotificationInfo>> {
        const completeUrl = `${environment.coreApiUrl}/${this.url}/${appNotification.Id}`;
        return this.http
            .put<IResponse<IAppNotificationInfo>>(completeUrl, appNotification, this.getHttpOptions(LvfOperationType.UPDATE))
            .pipe(tap(response => this.dialogService.Show(response)));
    }
}
