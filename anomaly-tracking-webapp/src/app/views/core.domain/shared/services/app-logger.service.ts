import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IResponse } from '../models/common/iexception';
import { environment } from 'src/environments/environment';
import { IAppLogInfo as IAppLoggerInfo } from '../models/application/app-log';
import { Observable } from 'rxjs';
import { ServiceBase } from './common/base.service';
import { LvfAppModule } from '../enums/lvf-app-module';
import { LocalStoreService } from '../../../../shared/services/local-store.service';
import { DialogService } from './common/dialog.service';
import { SearchFilterBase } from 'src/app/views/core.domain/_shared/filters/search-filter-base';
import { tap } from 'rxjs/operators';
import { LvfOperationType } from '../enums/lvf-operation-type';

@Injectable({
  providedIn: 'root'
})
export class AppLoggerService extends ServiceBase<IAppLoggerInfo> {

  private url = 'CoreDomain.WebServices/API/Applications/Logs/ServiceAppLogWeb.svc/api/applogs';

  constructor(http: HttpClient, localService: LocalStoreService, dialogService: DialogService) {
    super(LvfAppModule.APPLICATION_LOG, http, localService, dialogService, null);
  }

  getLogs(filter: SearchFilterBase = null): Observable<IResponse<IAppLoggerInfo[]>> {
    const completeUrl = `${environment.coreApiUrl}/${this.url}/_filter`;

    return this.http
      .post<IResponse<IAppLoggerInfo[]>>(completeUrl, filter, this.getHttpOptions(LvfOperationType.READ_ALL))
      .pipe(tap((response) => { this.dialogService.Show(response, true); }));
  }
}
