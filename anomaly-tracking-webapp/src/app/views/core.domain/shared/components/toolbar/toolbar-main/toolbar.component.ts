import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { ToolbarService } from '../toolbar.service';

@Component({
  selector: 'toolbar',
  templateUrl: './toolbar.component.html',
  styleUrls: ['./toolbar.component.scss']
})
export class ToolbarComponent implements OnInit {

  @Output() action = new EventEmitter();

  constructor(public toolbarService: ToolbarService) { }

  ngOnInit() {
    // Checks whether or not a manual action has been defined
    if (this.action.observers.length > 0) {
      // this.toolbarService.btnaddpath = '';
      this.toolbarService.onBtnSearchClick = () => { this.action.emit(); };
    }
  }
}
