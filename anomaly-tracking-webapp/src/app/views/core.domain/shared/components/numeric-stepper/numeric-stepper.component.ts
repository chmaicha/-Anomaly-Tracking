import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { NumericStepperService } from './numeric-stepper.service';

@Component({
  selector: 'numeric-stepper',
  templateUrl: './numeric-stepper.component.html',
  styleUrls: ['./numeric-stepper.component.scss']
})
export class NumericStepperComponent implements OnInit {

  @Input() min: number;
  @Input() max: number;
  @Input() value: number;
  @Input() parentId: number;
  @Input() disabled: boolean;


  @Output() updatedValue = new EventEmitter<number>();

  constructor(private numericStepperService: NumericStepperService) { }

  ngOnInit() {
    this.value = this.value || 0;
  }

  public up() {
    if (this.max < (this.value + 1)) {
      console.log('Invalid max value');
      return;
    }

    // Trigger UP event
    this.numericStepperService.NumericStepperRoom.next({
      parentId: this.parentId,
      operation: 'UP',
      oldValue: this.value,
      newValue: this.value + 1
    });

    this.value++;
    this.onUpdateValue();

  }

  public down() {
    if (this.min > (this.value - 1)) {
      console.log('Invalid min value');
      return;
    }

    // Trigger UP event
    this.numericStepperService.NumericStepperRoom.next({
      parentId: this.parentId,
      operation: 'DOWN',
      oldValue: this.value,
      newValue: this.value - 1
    });

    this.value--;
    this.onUpdateValue();

  }

  private onUpdateValue() {
    this.updatedValue.emit(this.value);
  }

  public disableStepper() {
    if (this.disabled) {
      this.value = 0;
      this.onUpdateValue();
    }

    return this.disabled;
  }
}
