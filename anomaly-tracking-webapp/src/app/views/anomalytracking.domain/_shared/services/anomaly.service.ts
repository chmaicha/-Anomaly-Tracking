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
import { Anomaly, IAnomalyInfo } from '../models/anomaly/anomaly';

@Injectable({
  providedIn: 'root',
})
export class AnomalyService extends ServiceBase<IAnomalyInfo> {
  private url =
    'AnomalyTracking.WebServices/API/Anomalies/ServiceAnomalyWeb.svc/api/anomalies';
  private completeUrl = `${environment.apiUrl}/${this.url}`;
  constructor(http: HttpClient,
    dialogService: DialogService) {
    super(http, dialogService);
  }

  create(anomaly: Anomaly): Observable<IResponse<IAnomalyInfo>> {
    const completeUrl = `${environment.apiUrl}/${this.url}`;

    return this.http
      .post<IResponse<IAnomalyInfo>>(
        completeUrl,
        anomaly,
        this.getHttpOptions(LvfOperationType.CREATE)
      )
      .pipe(
        tap((response) => {
          response;
        })
      );
  }

  update(anomaly: Anomaly): Observable<IResponse<IAnomalyInfo>> {
    const completeUrl = `${environment.apiUrl}/${this.url}/${anomaly.Id}`;

    return this.http
      .put<IResponse<IAnomalyInfo>>(
        completeUrl,
        anomaly,
        this.getHttpOptions(LvfOperationType.UPDATE)
      )
      .pipe(
        tap((response) => {
          response;
        })
      );
  }

  deleteAnomalies(indices: number[]) {
    const url = `${environment.apiUrl}/${this.url}/_delete`;

    return this.http
      .post<IResponse<number[]>>(
        url,
        indices,
        this.getHttpOptions(LvfOperationType.DELETE)
      )
      .pipe(tap((response) => response));
  }
 
  get(anomalyId: number): Observable<IResponse<IAnomalyInfo>> {
    const completeUrl = `${environment.apiUrl}/${this.url}/${anomalyId}`;

    return this.http
      .get<IResponse<IAnomalyInfo>>(
        completeUrl,
        this.getHttpOptions(LvfOperationType.READ)
      )
      .pipe(
        tap((response) => {
          response;
        })
      );
  }

  getAll(filter: SearchFilterBase): Observable<IResponse<IAnomalyInfo[]>> {
    const completeUrl = `${environment.apiUrl}/${this.url}/_filter`;

    return this.http.post<IResponse<IAnomalyInfo[]>>(
      completeUrl,
      filter,
      this.getHttpOptions(LvfOperationType.READ_ALL)
    );
  }

}
