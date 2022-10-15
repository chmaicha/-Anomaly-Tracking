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
import { AnomalyDeclaration, IAnomalyDeclarationInfo } from '../models/anomalyDeclaration/anomalyDeclaration';

@Injectable({
  providedIn: 'root',
})
export class AnomalyDeclarationService extends ServiceBase<IAnomalyDeclarationInfo> {
  private url =
    'AnomalyTracking.WebServices/API/AnomalyDeclarations/ServiceAnomalyDeclarationWeb.svc/api/anomalyDeclarations';
  private completeUrl = `${environment.apiUrl}/${this.url}`;
  constructor(http: HttpClient,
    dialogService: DialogService) {
    super(http, dialogService);
  }

  create(anomalyDeclaration: AnomalyDeclaration): Observable<IResponse<IAnomalyDeclarationInfo>> {
    const completeUrl = `${environment.apiUrl}/${this.url}`;

    return this.http
      .post<IResponse<IAnomalyDeclarationInfo>>(
        completeUrl,
        anomalyDeclaration,
        this.getHttpOptions(LvfOperationType.CREATE)
      )
      .pipe(
        tap((response) => {
          response;
        })
      );
  }

  update(anomalyDeclaration: AnomalyDeclaration): Observable<IResponse<IAnomalyDeclarationInfo>> {
    const completeUrl = `${environment.apiUrl}/${this.url}/${anomalyDeclaration.Id}`;

    return this.http
      .put<IResponse<IAnomalyDeclarationInfo>>(
        completeUrl,
        anomalyDeclaration,
        this.getHttpOptions(LvfOperationType.UPDATE)
      )
      .pipe(
        tap((response) => {
          response;
        })
      );
  }

  deleteAnomalyDeclarations(indices: number[]) {
    const url = `${environment.apiUrl}/${this.url}/_delete`;

    return this.http
      .post<IResponse<number[]>>(
        url,
        indices,
        this.getHttpOptions(LvfOperationType.DELETE)
      )
      .pipe(tap((response) => response));
  }
 
  get(anomalyDeclarationId: number): Observable<IResponse<IAnomalyDeclarationInfo>> {
    const completeUrl = `${environment.apiUrl}/${this.url}/${anomalyDeclarationId}`;

    return this.http
      .get<IResponse<IAnomalyDeclarationInfo>>(
        completeUrl,
        this.getHttpOptions(LvfOperationType.READ)
      )
      .pipe(
        tap((response) => {
          response;
        })
      );
  }

  getAll(filter: SearchFilterBase): Observable<IResponse<IAnomalyDeclarationInfo[]>> {
    const completeUrl = `${environment.apiUrl}/${this.url}/_filter`;

    return this.http.post<IResponse<IAnomalyDeclarationInfo[]>>(
      completeUrl,
      filter,
      this.getHttpOptions(LvfOperationType.READ_ALL)
    );
  }

}
