import { Address } from 'src/app/views/core.domain/shared/models/common/address';
import {
  EntityBase,
  IEntityBaseInfo,
} from 'src/app/views/core.domain/shared/models/common/entitybase';
import { Mold } from '../mold/mold';

/***
 * Represents the client object info.
 */
export interface IClientInfo extends IEntityBaseInfo {
  Label: string;
  Email: string;
  PhoneNumber: string;
  Address?: Address;
  AddressId?:number;
  Molds?:Mold[];
}

/**
 * @Class client
 * @classdesc Represents a client object.
 */
export class Client extends EntityBase {
  Label: string;
  Email: string;
  PhoneNumber: string;
  Address?: Address;
  AddressId?:number;
  Molds?:Mold[];
  
  /**
   * @constructor
   * Creates an instance of project.
   */
  constructor(info: IClientInfo) {
    super(info);

    this.Label = info.Label;
    this.Email = info.Email;
    this.PhoneNumber = info.PhoneNumber;
    this.Address = Address.createInstance(info.Address);
    this.AddressId = info.AddressId;
    this.Molds=info.Molds;
  }
}
