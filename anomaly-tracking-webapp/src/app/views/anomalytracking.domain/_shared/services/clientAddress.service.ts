import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { LvfOperationType } from 'src/app/views/core.domain/shared/enums/lvf-operation-type';
import { SearchFilterBase } from 'src/app/views/core.domain/shared/filters/search-filter-base';
import { IResponse } from 'src/app/views/core.domain/shared/models/common/iexception';
import { ServiceBase } from 'src/app/views/core.domain/shared/services/common/base.service';
import { environment } from 'src/environments/environment';
import { ClientAddress, IClientAddressInfo } from '../models/clientAddress/clientAddress';

@Injectable({
  providedIn: 'root',
})
export class ClientAddressService extends ServiceBase<IClientAddressInfo> {
  private url =
    'AnomalyTracking.WebServices/API/ClientAddresss/ServiceClientAddressWeb.svc/api/clientAddresss';

  constructor(http: HttpClient) {
    super(http);
  }

  create(clientAddress: ClientAddress): Observable<IResponse<IClientAddressInfo>> {
    const completeUrl = `${environment.apiUrl}/${this.url}`;

    return this.http
      .post<IResponse<IClientAddressInfo>>(
        completeUrl,
        clientAddress,
        this.getHttpOptions(LvfOperationType.CREATE)
      )
      .pipe(
        tap((response) => {
          response;
        })
      );
  }

  update(clientAddress: ClientAddress): Observable<IResponse<IClientAddressInfo>> {
    const completeUrl = `${environment.apiUrl}/${this.url}/${clientAddress.Id}`;

    return this.http
      .put<IResponse<IClientAddressInfo>>(
        completeUrl,
        clientAddress,
        this.getHttpOptions(LvfOperationType.UPDATE)
      )
      .pipe(
        tap((response) => {
          response;
        })
      );
  }

  deleteClientAddresss(indices: number[]) {
    const url = `${environment.apiUrl}/${this.url}/_delete`;

    return this.http
      .post<IResponse<number[]>>(
        url,
        indices,
        this.getHttpOptions(LvfOperationType.DELETE)
      )
      .pipe(tap((response) => response));
  }

  get(clientAddressId: number): Observable<IResponse<IClientAddressInfo>> {
    const completeUrl = `${environment.apiUrl}/${this.url}/${clientAddressId}`;

    return this.http
      .get<IResponse<IClientAddressInfo>>(
        completeUrl,
        this.getHttpOptions(LvfOperationType.READ)
      )
      .pipe(
        tap((response) => {
          response;
        })
      );
  }

  getAll(filter: SearchFilterBase): Observable<IResponse<IClientAddressInfo[]>> {
    const completeUrl = `${environment.apiUrl}/${this.url}/_filter`;

    return this.http.post<IResponse<IClientAddressInfo[]>>(
      completeUrl,
      filter,
      this.getHttpOptions(LvfOperationType.READ_ALL)
    );
  }
}
