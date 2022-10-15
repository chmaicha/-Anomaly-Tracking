import { Injectable } from '@angular/core';
import { AuthenticationData, IAuthenticationData } from 'src/app/views/authentification.domain/shared/models/authentification-data';

@Injectable({
  providedIn: 'root'
})
export class ContextService {

  public data: AuthenticationData;

  constructor(
  ) {
    this.data = new AuthenticationData(<IAuthenticationData>{});
  }

  setContextData(data: IAuthenticationData) {
    this.data = new AuthenticationData(data);
  }

}
