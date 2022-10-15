import { Component, OnInit } from '@angular/core';
import { ToolbarService } from '../toolbar/toolbar.service';

@Component({
  selector: 'search-bar',
  templateUrl: './search-bar.component.html',
  styleUrls: ['./search-bar.component.scss']
})
export class SearchBarComponent implements OnInit {

  searchInput: string;
  constructor(public toolbarService: ToolbarService) { }

  ngOnInit() { }

  public onClick(): void {
    this.toolbarService.searchInput = this.searchInput;
    this.toolbarService.onBtnSearchClick();
  }
}
