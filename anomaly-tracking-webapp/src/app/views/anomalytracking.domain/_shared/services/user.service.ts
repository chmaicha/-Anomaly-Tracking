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
import { User, IUserInfo } from '../models/user/user';

@Injectable({
  providedIn: 'root',
})
export class UserService extends ServiceBase<IUserInfo> {
  private url =
    'AnomalyTracking.WebServices/API/Users/ServiceUserWeb.svc/api/users';

  constructor(
    http: HttpClient,
    dialogService: DialogService
    ) {
    super(http, dialogService);
  }

  create(user: User): Observable<IResponse<IUserInfo>> {
    const completeUrl = `${environment.apiUrl}/${this.url}`;

    return this.http
      .post<IResponse<IUserInfo>>(
        completeUrl,
        user,
        this.getHttpOptions(LvfOperationType.CREATE)
      )
      .pipe(
        tap((response) => {
          response;
        })
      );
  }

  update(user: User): Observable<IResponse<IUserInfo>> {
    const completeUrl = `${environment.apiUrl}/${this.url}/${user.Id}`;

    return this.http
      .put<IResponse<IUserInfo>>(
        completeUrl,
        user,
        this.getHttpOptions(LvfOperationType.UPDATE)
      )
      .pipe(
        tap((response) => {
          response;
        })
      );
  }

  deleteUsers(indices: number[]) {
    const url = `${environment.apiUrl}/${this.url}/_delete`;

    return this.http
      .post<IResponse<number[]>>(
        url,
        indices,
        this.getHttpOptions(LvfOperationType.DELETE)
      )
      .pipe(tap((response) => response));
  }

  get(userId: number): Observable<IResponse<IUserInfo>> {
    const completeUrl = `${environment.apiUrl}/${this.url}/${userId}`;

    return this.http
      .get<IResponse<IUserInfo>>(
        completeUrl,
        this.getHttpOptions(LvfOperationType.READ)
      )
      .pipe(
        tap((response) => {
          response;
        })
      );
  }

  getAll(filter: SearchFilterBase): Observable<IResponse<IUserInfo[]>> {
    const completeUrl = `${environment.apiUrl}/${this.url}/_filter`;

    return this.http.post<IResponse<IUserInfo[]>>(
      completeUrl,
      filter,
      this.getHttpOptions(LvfOperationType.READ_ALL)
    );
  }
}
