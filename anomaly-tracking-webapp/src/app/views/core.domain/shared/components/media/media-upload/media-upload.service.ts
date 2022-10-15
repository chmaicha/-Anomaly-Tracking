import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from 'src/environments/environment';
import { IMediaInfo } from '../../../models/common/media';
import { IResponse } from '../../../models/common/iexception';
import { LvfOperationType } from '../../../enums/lvf-operation-type';
import { ServiceBase } from '../../../services/common/base.service';
import { DialogService } from '../../../services/common/dialog.service';

@Injectable({
  providedIn: 'root'
})
export class MediaUploadService extends ServiceBase<IMediaInfo>{
  private fileurl = 'CoreDomain.WebServices/API/MediaManager/ServiceMediaManagerWeb.svc/api/mediamanager';

  constructor(http: HttpClient, dialogService: DialogService) {
    super(http, dialogService);
  }

  public uploadFile(formdata: FormData): Observable<IResponse<IMediaInfo>> {

    const completeUrl = `${environment.coreApiUrl}/${this.fileurl}`;

    return this.http
      .post<IResponse<IMediaInfo>>(completeUrl, formdata, this.getHttpOptions(LvfOperationType.CREATE, ''));
  }
}
