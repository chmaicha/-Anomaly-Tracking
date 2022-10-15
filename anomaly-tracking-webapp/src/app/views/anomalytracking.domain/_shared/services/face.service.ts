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
import { Face, IFaceInfo } from '../models/face/face';


@Injectable({
  providedIn: 'root',
})
export class FaceService extends ServiceBase<IFaceInfo> {
  private url =
    'AnomalyTracking.WebServices/API/Faces/ServiceFaceWeb.svc/api/faces';

  constructor(http: HttpClient, dialogService: DialogService ) {
    super(http, dialogService);
  }

  create(face: Face): Observable<IResponse<IFaceInfo>> {
    const completeUrl = `${environment.apiUrl}/${this.url}`;

    return this.http
      .post<IResponse<IFaceInfo>>(
        completeUrl,
        face,
        this.getHttpOptions(LvfOperationType.CREATE)
      )
      .pipe(
        tap((response) => {
          response;
        })
      );
  }

  update(face: Face): Observable<IResponse<IFaceInfo>> {
    const completeUrl = `${environment.apiUrl}/${this.url}/${face.Id}`;

    return this.http
      .put<IResponse<IFaceInfo>>(
        completeUrl,
        face,
        this.getHttpOptions(LvfOperationType.UPDATE)
      )
      .pipe(
        tap((response) => {
          response;
        })
      );
  }

  deleteFaces(indices: number[]) {
    const url = `${environment.apiUrl}/${this.url}/_delete`;

    return this.http
      .post<IResponse<number[]>>(
        url,
        indices,
        this.getHttpOptions(LvfOperationType.DELETE)
      )
      .pipe(tap((response) => response));
  }

  get(faceId: number): Observable<IResponse<IFaceInfo>> {
    const completeUrl = `${environment.apiUrl}/${this.url}/${faceId}`;

    return this.http
      .get<IResponse<IFaceInfo>>(
        completeUrl,
        this.getHttpOptions(LvfOperationType.READ)
      )
      .pipe(
        tap((response) => {
          response;
        })
      );
  }

  getAll(filter: SearchFilterBase): Observable<IResponse<IFaceInfo[]>> {
    const completeUrl = `${environment.apiUrl}/${this.url}/_filter`;

    return this.http.post<IResponse<IFaceInfo[]>>(
      completeUrl,
      filter,
      this.getHttpOptions(LvfOperationType.READ_ALL)
    );
  }
}
