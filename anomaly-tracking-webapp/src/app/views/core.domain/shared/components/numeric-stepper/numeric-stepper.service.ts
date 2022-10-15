import { Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs';

export interface INumericStepperInfo {
  operation: 'UP' | 'DOWN';
  oldValue: number;
  newValue: number;
  parentId: number;
}

@Injectable({
  providedIn: 'root'
})
export class NumericStepperService {

  NumericStepperRoom: ReplaySubject<INumericStepperInfo>;

  constructor() {
    this.NumericStepperRoom = new ReplaySubject(1);
  }
}