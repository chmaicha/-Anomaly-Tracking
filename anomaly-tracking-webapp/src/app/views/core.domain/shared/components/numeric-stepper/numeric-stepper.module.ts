import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NumericStepperComponent } from './numeric-stepper.component';
import { FormsModule } from '@angular/forms';

@NgModule({
  imports: [CommonModule, FormsModule],
  declarations: [NumericStepperComponent],
  
  exports: [NumericStepperComponent, CommonModule, FormsModule]
})
export class NumericStepperModule { }
