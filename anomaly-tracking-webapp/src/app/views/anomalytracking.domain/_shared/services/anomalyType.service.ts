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
import { AnomalyType, IAnomalyTypeInfo } from '../models/anomalyType/anomalyType';

@Injectable({
  providedIn: 'root',
})
export class AnomalyTypeService extends ServiceBase<IAnomalyTypeInfo> {
  private url =
    'AnomalyTracking.WebServices/API/AnomalyTypes/ServiceAnomalyTypeWeb.svc/api/anomalyTypes';

  constructor(http: HttpClient, dialogService: DialogService) {
    super(http, dialogService);
  }

  create(anomalyType: AnomalyType): Observable<IResponse<IAnomalyTypeInfo>> {
    const completeUrl = `${environment.apiUrl}/${this.url}`;

    return this.http
      .post<IResponse<IAnomalyTypeInfo>>(
        completeUrl,
        anomalyType,
        this.getHttpOptions(LvfOperationType.CREATE)
      )
      .pipe(
        tap((response) => {
          response;
        })
      );
  }

  update(anomalyType: AnomalyType): Observable<IResponse<IAnomalyTypeInfo>> {
    const completeUrl = `${environment.apiUrl}/${this.url}/${anomalyType.Id}`;

    return this.http
      .put<IResponse<IAnomalyTypeInfo>>(
        completeUrl,
        anomalyType,
        this.getHttpOptions(LvfOperationType.UPDATE)
      )
      .pipe(
        tap((response) => {
          response;
        })
      );
  }

  deleteAnomalyTypes(indices: number[]) {
    const url = `${environment.apiUrl}/${this.url}/_delete`;

    return this.http
      .post<IResponse<number[]>>(
        url,
        indices,
        this.getHttpOptions(LvfOperationType.DELETE)
      )
      .pipe(tap((response) => response));
  }

  get(anomalyTypeId: number): Observable<IResponse<IAnomalyTypeInfo>> {
    const completeUrl = `${environment.apiUrl}/${this.url}/${anomalyTypeId}`;

    return this.http
      .get<IResponse<IAnomalyTypeInfo>>(
        completeUrl,
        this.getHttpOptions(LvfOperationType.READ)
      )
      .pipe(
        tap((response) => {
          response;
        })
      );
  }

  getAll(filter: SearchFilterBase): Observable<IResponse<IAnomalyTypeInfo[]>> {
    const completeUrl = `${environment.apiUrl}/${this.url}/_filter`;

    return this.http.post<IResponse<IAnomalyTypeInfo[]>>(
      completeUrl,
      filter,
      this.getHttpOptions(LvfOperationType.READ_ALL)
    );
  }
}
