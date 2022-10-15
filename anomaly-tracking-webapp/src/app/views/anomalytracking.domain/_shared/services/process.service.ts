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
import { Process, IProcessInfo } from '../models/process/process';

@Injectable({
  providedIn: 'root',
})
export class ProcessService extends ServiceBase<IProcessInfo> {
  process : Process ;
  private url =
    'AnomalyTracking.WebServices/API/Processs/ServiceProcessWeb.svc/api/processs';
  constructor(http: HttpClient, dialogService: DialogService) {
    super(http, dialogService);
  }

  create(process: Process): Observable<IResponse<IProcessInfo>> {
    const completeUrl = `${environment.apiUrl}/${this.url}`;

    return this.http
      .post<IResponse<IProcessInfo>>(
        completeUrl,
        process,
        this.getHttpOptions(LvfOperationType.CREATE)
      )
      .pipe(
        tap((response) => {
          response;
        })
      );
  }

  update(process: Process): Observable<IResponse<IProcessInfo>> {
    const completeUrl = `${environment.apiUrl}/${this.url}/${process.Id}`;

    return this.http
      .put<IResponse<IProcessInfo>>(
        completeUrl,
        process,
        this.getHttpOptions(LvfOperationType.UPDATE)
      )
      .pipe(
        tap((response) => {
          response;
        })
      );
  }

  deleteProcesss(indices: number[]) {
    const url = `${environment.apiUrl}/${this.url}/_delete`;

    return this.http
      .post<IResponse<number[]>>(
        url,
        indices,
        this.getHttpOptions(LvfOperationType.DELETE)
      )
      .pipe(tap((response) => response));
  }
  get(processId: number): Observable<IResponse<IProcessInfo>> {
    const completeUrl = `${environment.apiUrl}/${this.url}/${processId}`;

    return this.http
      .get<IResponse<IProcessInfo>>(
        completeUrl,
        this.getHttpOptions(LvfOperationType.READ)
      )
      .pipe(
        tap((response) => {
          response;
        })
      );
  }
  getAll(filter: SearchFilterBase): Observable<IResponse<IProcessInfo[]>> {
    const completeUrl = `${environment.apiUrl}/${this.url}/_filter`;
    return this.http.post<IResponse<IProcessInfo[]>>(
      completeUrl,
      filter,
      this.getHttpOptions(LvfOperationType.READ_ALL)
    );
  }
}
