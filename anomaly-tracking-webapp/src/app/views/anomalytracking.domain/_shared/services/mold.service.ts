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
import { Mold, IMoldInfo } from '../models/mold/mold';

@Injectable({
  providedIn: 'root',
})
export class MoldService extends ServiceBase<IMoldInfo> {
  private url =
    'AnomalyTracking.WebServices/API/Molds/ServiceMoldWeb.svc/api/molds';
  private completeUrl = `${environment.apiUrl}/${this.url}`;
  constructor(http: HttpClient,
    dialogService: DialogService) {
    super(http, dialogService);
  }

  create(mold: Mold): Observable<IResponse<IMoldInfo>> {
    const completeUrl = `${environment.apiUrl}/${this.url}`;

    return this.http
      .post<IResponse<IMoldInfo>>(
        completeUrl,
        mold,
        this.getHttpOptions(LvfOperationType.CREATE)
      )
      .pipe(
        tap((response) => {
          response;
        })
      );
  }

  update(mold: Mold): Observable<IResponse<IMoldInfo>> {
    const completeUrl = `${environment.apiUrl}/${this.url}/${mold.Id}`;

    return this.http
      .put<IResponse<IMoldInfo>>(
        completeUrl,
        mold,
        this.getHttpOptions(LvfOperationType.UPDATE)
      )
      .pipe(
        tap((response) => {
          response;
        })
      );
  }

  deleteMolds(indices: number[]) {
    const url = `${environment.apiUrl}/${this.url}/_delete`;

    return this.http
      .post<IResponse<number[]>>(
        url,
        indices,
        this.getHttpOptions(LvfOperationType.DELETE)
      )
      .pipe(tap((response) => response));
  }
 
  get(moldId: number): Observable<IResponse<IMoldInfo>> {
    const completeUrl = `${environment.apiUrl}/${this.url}/${moldId}`;

    return this.http
      .get<IResponse<IMoldInfo>>(
        completeUrl,
        this.getHttpOptions(LvfOperationType.READ)
      )
      .pipe(
        tap((response) => {
          response;
        })
      );
  }

  getAll(filter: SearchFilterBase): Observable<IResponse<IMoldInfo[]>> {
    const completeUrl = `${environment.apiUrl}/${this.url}/_filter`;

    return this.http.post<IResponse<IMoldInfo[]>>(
      completeUrl,
      filter,
      this.getHttpOptions(LvfOperationType.READ_ALL)
    );
  }

}
