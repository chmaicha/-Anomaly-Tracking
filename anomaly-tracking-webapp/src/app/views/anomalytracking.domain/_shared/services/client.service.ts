import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { LvfOperationType } from 'src/app/views/core.domain/shared/enums/lvf-operation-type';
import { SearchFilterBase } from 'src/app/views/core.domain/shared/filters/search-filter-base';
import { IResponse } from 'src/app/views/core.domain/shared/models/common/iexception';
import { ServiceBase } from 'src/app/views/core.domain/shared/services/common/base.service';
import { DialogService } from 'src/app/views/core.domain/shared/services/common/dialog.service';
import { environment } from 'src/environments/environment';
import { Client, IClientInfo } from '../models/client/client';

@Injectable({
  providedIn: 'root',
})
export class ClientService extends ServiceBase<IClientInfo> {

  client: Client;

  private url =
    'AnomalyTracking.WebServices/API/Clients/ServiceClientWeb.svc/api/clients';

    constructor(
      http: HttpClient,
      dialogService: DialogService) {
      super(http, dialogService);
    }

  create(client: Client): Observable<IResponse<IClientInfo>> {
    const completeUrl = `${environment.apiUrl}/${this.url}`;

    return this.http
      .post<IResponse<IClientInfo>>(
        completeUrl,
        client,
        this.getHttpOptions(LvfOperationType.CREATE)
      )
      .pipe(
        tap((response) => {
          response;
        })
      );
  }

  update(client: Client): Observable<IResponse<IClientInfo>> {
    const completeUrl = `${environment.apiUrl}/${this.url}/${client.Id}`;

    return this.http
      .put<IResponse<IClientInfo>>(
        completeUrl,
        client,
        this.getHttpOptions(LvfOperationType.UPDATE)
      )
      .pipe(
        tap((response) => {
          response;
        })
      );
  }

  deleteClients(indices: number[]) {
    const url = `${environment.apiUrl}/${this.url}/_delete`;

    return this.http
      .post<IResponse<number[]>>(
        url,
        indices,
        this.getHttpOptions(LvfOperationType.DELETE)
      )
      .pipe(tap((response) => response));
  }

  get(clientId: number): Observable<IResponse<IClientInfo>> {
    const completeUrl = `${environment.apiUrl}/${this.url}/${clientId}`;

    return this.http
      .get<IResponse<IClientInfo>>(
        completeUrl,
        this.getHttpOptions(LvfOperationType.READ)
      )
      .pipe(
        tap((response) => {
          response;
        })
      );
  }

  getAll(filter: SearchFilterBase): Observable<IResponse<IClientInfo[]>> {
    const completeUrl = `${environment.apiUrl}/${this.url}/_filter`;

    return this.http.post<IResponse<IClientInfo[]>>(
      completeUrl,
      filter,
      this.getHttpOptions(LvfOperationType.READ_ALL)
    );
  }
}
