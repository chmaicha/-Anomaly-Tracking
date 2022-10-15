import { Component } from '@angular/core';
import { MatChip } from '@angular/material/chips';

@Component({
  selector: 'app-face-position2',
  templateUrl: './face-position2.component.html',
  styleUrls: ['./face-position2.component.scss']
})
export class FacePosition2Component  {

  constructor() { }

  
  toggleSelection(chip: MatChip) {
    chip.toggleSelected();
 }

}
