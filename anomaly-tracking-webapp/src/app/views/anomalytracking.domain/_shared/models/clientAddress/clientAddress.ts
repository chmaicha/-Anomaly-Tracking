import { Address } from 'src/app/views/core.domain/shared/models/common/address';
import {
  EntityBase,
  IEntityBaseInfo,
} from 'src/app/views/core.domain/shared/models/common/entitybase';

/***
 * Represents the clientaddressaddress object info.
 */
export interface IClientAddressInfo extends IEntityBaseInfo {
  StreetNumber: string;
  StreetName: string;
  ZipCode: string;
  City: string;
  CountryCode:string;
  FurtherInformation:string;
}

/**
 * @Class clientaddress
 * @classdesc Represents a clientaddress object.
 */
export class ClientAddress extends EntityBase {
  StreetNumber: string;
  StreetName: string;
  ZipCode: string;
  City: string;
  CountryCode:string;
  FurtherInformation:string;
  /**
   * @constructor
   * Creates an instance of project.
   */
  constructor(info: IClientAddressInfo) {
    super(info);

    this.StreetNumber = info.StreetNumber;
    this.StreetName = info.StreetName;
    this.ZipCode = info.ZipCode;
    this.City = info.City;
    this.CountryCode = info.CountryCode;
    this.FurtherInformation = info.FurtherInformation;
  }
}
