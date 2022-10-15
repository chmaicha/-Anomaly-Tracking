import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { mainContentAnimation } from '../../core.domain/shared/animation';
import { SidebarService } from '../../core.domain/shared/services/sideBar.service';
import { FacePositionComponent } from '../../core.domain/shared/components/face-position/face-position.component';
import { AnomalyDeclaration, IAnomalyDeclarationInfo } from 'src/app/views/anomalytracking.domain/_shared/models/anomalyDeclaration/anomalyDeclaration';
import { AnomalyDeclarationService } from 'src/app/views/anomalytracking.domain/_shared/services/anomalyDeclaration.service';
import { SearchFilterBase } from 'src/app/views/core.domain/shared/filters/search-filter-base';
import { IResponse } from 'src/app/views/core.domain/shared/models/common/iexception';
import { FilterService } from 'src/app/views/core.domain/shared/services/common/filter.service';
import { EnumHelper, IEnumEntry } from '../../core.domain/shared/_helpers/enum.helper';
import { NgbModal, NgbModalConfig } from '@ng-bootstrap/ng-bootstrap';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { LocalService } from '../../anomalytracking.domain/_shared/services/local.service';

/**
 * @title Basic use of `<table mat-table>`
 */

@Component({
  selector: 'app-anomaly-history',
  templateUrl: './anomaly-history.component.html',
  styleUrls: ['./anomaly-history.component.scss'],
  animations: [mainContentAnimation()],
})
export class AnomalyHistoryComponent {
  anomalyDeclarations: AnomalyDeclaration[];
  anomalyDeclaration: AnomalyDeclaration;
  filter: SearchFilterBase;
  indices: number[];
  sidebarState!: string;
  constructor(
    private localStore : LocalService,

    private dialog: MatDialog,
    private filterService: FilterService,
    private anomalyDeclarationService: AnomalyDeclarationService,
    private sidebarService: SidebarService,
    private modalService: NgbModal,
    private router: Router,
    config: NgbModalConfig
  ) {}



  ngOnInit() {
    if(this.localStore.getData('CurrentUserName')){
      this.sidebarService.sidebarStateObservable$.subscribe(
        (newState: string) => {
          this.sidebarState = newState;
        }
      );
      this.filter = this.filterService.getFilterBase(true);
      this.getAnomalyDeclarations();

    }else {
      this.router.navigateByUrl('/')
    }

  }


  getAnomalyDeclarations(page: number = 1) {

    this.filter.Page = page;

    if (this.filter.Page < 1) {
      this.filter.Page = 1;
    }

    this.anomalyDeclarationService.getAll(this.filter)
      .subscribe((response: IResponse<IAnomalyDeclarationInfo[]>) => {
        if (response.IsSuccessful) {
          this.anomalyDeclarations = response.Data ? response.Data.map(info => new AnomalyDeclaration(info)) : [];
          this.filter.TotalCount = response.TotalCount;
        }
      });
  }


}
