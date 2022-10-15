import { Component, OnInit } from '@angular/core';
import { SharedAnimations } from 'src/app/shared/animations/shared-animations';
import { SearchFilterBase } from '../../../filters/search-filter-base';
import { FilterService } from '../../../services/common/filter.service';
import { ToolbarService } from '../toolbar.service';

@Component({
  selector: 'app-toolbar-operations',
  templateUrl: './toolbar-operations.component.html',
  styleUrls: ['./toolbar-operations.component.scss'],
  animations: [SharedAnimations]
})
export class ToolbarOperationsComponent implements OnInit {

  filter: SearchFilterBase;

  constructor(public toolbarService: ToolbarService,
    private filterService: FilterService) { }

  ngOnInit() { 
    this.filter = this.filterService.getFilterBase(true, false);
  }

  onAllSelected() {
    this.toolbarService.selectAllHandler();
  }
}