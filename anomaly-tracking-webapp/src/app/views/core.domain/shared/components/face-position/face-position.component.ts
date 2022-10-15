import { Component, OnInit } from '@angular/core';
import { MatChip } from '@angular/material/chips';

@Component({
  selector: 'app-face-position',
  templateUrl: './face-position.component.html',
  styleUrls: ['./face-position.component.scss']
})
export class FacePositionComponent implements OnInit {
  constructor() { }
  ngOnInit(): void {
    throw new Error('Method not implemented.');
  }
  toggleSelection(chip: MatChip) {
    chip.toggleSelected();
 }

}
