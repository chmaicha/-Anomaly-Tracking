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
import { Cavity, ICavityInfo } from '../models/cavity/cavity';

@Injectable({
  providedIn: 'root',
})
export class CavityService extends ServiceBase<ICavityInfo> {
  private url =
    'AnomalyTracking.WebServices/API/Cavities/ServiceCavityWeb.svc/api/cavities';
  private completeUrl = `${environment.apiUrl}/${this.url}`;
  constructor(http: HttpClient,
    dialogService: DialogService) {
    super(http, dialogService);
  }

  create(cavity: Cavity): Observable<IResponse<ICavityInfo>> {
    const completeUrl = `${environment.apiUrl}/${this.url}`;

    return this.http
      .post<IResponse<ICavityInfo>>(
        completeUrl,
        cavity,
        this.getHttpOptions(LvfOperationType.CREATE)
      )
      .pipe(
        tap((response) => {
          response;
        })
      );
  }

  update(cavity: Cavity): Observable<IResponse<ICavityInfo>> {
    const completeUrl = `${environment.apiUrl}/${this.url}/${cavity.Id}`;

    return this.http
      .put<IResponse<ICavityInfo>>(
        completeUrl,
        cavity,
        this.getHttpOptions(LvfOperationType.UPDATE)
      )
      .pipe(
        tap((response) => {
          response;
        })
      );
  }

  deleteCavities(indices: number[]) {
    const url = `${environment.apiUrl}/${this.url}/_delete`;

    return this.http
      .post<IResponse<number[]>>(
        url,
        indices,
        this.getHttpOptions(LvfOperationType.DELETE)
      )
      .pipe(tap((response) => response));
  }
 
  get(cavityId: number): Observable<IResponse<ICavityInfo>> {
    const completeUrl = `${environment.apiUrl}/${this.url}/${cavityId}`;

    return this.http
      .get<IResponse<ICavityInfo>>(
        completeUrl,
        this.getHttpOptions(LvfOperationType.READ)
      )
      .pipe(
        tap((response) => {
          response;
        })
      );
  }

  getAll(filter: SearchFilterBase): Observable<IResponse<ICavityInfo[]>> {
    const completeUrl = `${environment.apiUrl}/${this.url}/_filter`;

    return this.http.post<IResponse<ICavityInfo[]>>(
      completeUrl,
      filter,
      this.getHttpOptions(LvfOperationType.READ_ALL)
    );
  }

}
