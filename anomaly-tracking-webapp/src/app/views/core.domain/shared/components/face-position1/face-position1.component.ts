import { Component, OnInit } from '@angular/core';
import { MatChip } from '@angular/material/chips';

@Component({
  selector: 'app-face-position1',
  templateUrl: './face-position1.component.html',
  styleUrls: ['./face-position1.component.scss']
})
export class FacePosition1Component implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }
  toggleSelection(chip: MatChip) {
    chip.toggleSelected();
 }


}
