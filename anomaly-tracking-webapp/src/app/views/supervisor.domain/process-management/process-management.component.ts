import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { mainContentAnimation } from '../../core.domain/shared/animation';
import { SidebarService } from '../../core.domain/shared/services/sideBar.service';
import {
  Process,
  IProcessInfo,
} from 'src/app/views/anomalytracking.domain/_shared/models/process/process';
import {
  AnomalyType,
  IAnomalyTypeInfo,
} from 'src/app/views/anomalytracking.domain/_shared/models/anomalyType/anomalyType';
import { ProcessService } from 'src/app/views/anomalytracking.domain/_shared/services/process.service';
import { AnomalyTypeService } from 'src/app/views/anomalytracking.domain/_shared/services/anomalyType.service';
import { SearchFilterBase } from 'src/app/views/core.domain/shared/filters/search-filter-base';
import { IResponse } from 'src/app/views/core.domain/shared/models/common/iexception';
import { FilterService } from 'src/app/views/core.domain/shared/services/common/filter.service';
import {
  EnumHelper,
  IEnumEntry,
} from '../../core.domain/shared/_helpers/enum.helper';
import { NgbModal, NgbModalConfig } from '@ng-bootstrap/ng-bootstrap';
import { Router } from '@angular/router';
import { LocalService } from '../../anomalytracking.domain/_shared/services/local.service';

@Component({
  selector: 'app-process-management',
  templateUrl: './process-management.component.html',
  styleUrls: ['./process-management.component.scss'],
  animations: [mainContentAnimation()],
})
export class ProcessManagementComponent implements OnInit {
  @ViewChild('processDetailPopup', { static: true })
  processDetailPopup: TemplateRef<any>;
  @ViewChild('anomalyTypeDetailPopup', { static: true })
  anomalyTypeDetailPopup: TemplateRef<any>;
  selectedProcessId: number;
  sidebarState!: string;
  searchfilterProcess : string="";
  processs: Process[];
  process: Process;
  anomalyTypes: AnomalyType[];
  anomalyType: AnomalyType;
  filter: SearchFilterBase;
  indices: number[];
  role = false;
  constructor(
    private localStore : LocalService,
    private dialog: MatDialog,
    private filterService: FilterService,
    private processService: ProcessService,
    private anomalyTypeService: AnomalyTypeService,
    private sidebarService: SidebarService,
    private modalService: NgbModal,
    private router: Router,
    config: NgbModalConfig
  ) {}

  ngOnInit() {
    if(this.localStore.getData('CurrentUserName')){
      if(this.localStore.getData('CurrentUserRole') === '1'){
        this.filter = this.filterService.getFilterBase(true);
        this.getProcesss();
        this.sidebarService.sidebarStateObservable$.subscribe(
          (newState: string) => {
            this.sidebarState = newState;
          }
        );
      }else{
        this.router.navigateByUrl('/anomaly-declaration')

      }

    }else{
      this.router.navigateByUrl('/')
    }

  }
  showInput() {
    this.role = true;
  }
  hideInput() {
    this.role = false;
  }
  onSave() {
    debugger;
    if (this.process.Id) {
      this.update();
    } else {
      this.create();
    }
  }
  onSaveAnomalyType() {
    if (this.anomalyType.Id) {
      this.updateAnomalyType();
    } else {
      this.createAnomalyType();
    }
  }

  private update() {
    this.processService
      .update(this.process)
      .subscribe((response: IResponse<IProcessInfo>) => {
        if (response.IsSuccessful) {
          this.process = new Process(response.Data);
        }
      });
  }
  private updateAnomalyType() {
    this.anomalyTypeService
      .update(this.anomalyType)
      .subscribe((response: IResponse<IAnomalyTypeInfo>) => {
        if (response.IsSuccessful) {
          this.anomalyType = new AnomalyType(response.Data);
        }
      });
  }

  private create() {
    debugger;
    this.processService
      .create(this.process)
      .subscribe((response: IResponse<IProcessInfo>) => {
        if (response.IsSuccessful) {
          this.process = new Process(response.Data);
          this.getProcesss();
        }
        console.log(response);
      });
  }
  private createAnomalyType() {
    this.anomalyTypeService
      .create(this.anomalyType)
      .subscribe((response: IResponse<IAnomalyTypeInfo>) => {
        if (response.IsSuccessful) {
          this.anomalyType = new AnomalyType(response.Data);
          // this.getAnomalyTypes();
        }
      });
  }

  openModal() {
    this.process = Process.createInstance(Process);
    this.modalService.open(this.processDetailPopup, {
      centered: true,
      size: 'md',
    });
  }

  openModalType(processId: number) {
    this.anomalyTypes = this.processs.find(
      (c) => c.Id == processId
    ).AnomalyTypes;
    this.anomalyType = AnomalyType.createInstance(AnomalyType);
    this.modalService.open(this.anomalyTypeDetailPopup, {
      centered: true,
      size: 'lg',
    });
  }

  editProcess(processId: number) {
    this.process = this.processs.find((u) => u.Id == processId);
    this.modalService.open(this.processDetailPopup, {
      centered: true,
      size: 'lg',
    });
  }
  editAnomalyType(anomalyTypeId: number) {
    this.anomalyType = this.anomalyTypes.find((a) => a.Id == anomalyTypeId);
  }

  getProcesss(page: number = 1) {

    let filter = this.filterService.getFilterBase(false);
    filter.SearchInput = this.searchfilterProcess;
    this.filter.Page = page;

    if (this.filter.Page < 1) {
      this.filter.Page = 1;
    }

    this.processService
      .getAll(filter)
      .subscribe((response: IResponse<IProcessInfo[]>) => {
        if (response.IsSuccessful) {
          this.processs = response.Data
            ? response.Data.map((info) => new Process(info))
            : [];
          this.filter.TotalCount = response.TotalCount;
        }
      });
  }
  selectedProcess(event: any) {
    this.selectedProcessId = event.target.value;
    this.anomalyTypes = this.processs.find(
      (c) => c.Id == this.selectedProcessId
    ).AnomalyTypes;
  }
  onDeleteProcess(processId: number) {
    debugger;
    this.indices = [];
    this.indices.push(processId);
    return this.processService
      .deleteProcesss(this.indices)
      .subscribe((response: IResponse<number[]>) => {
        if (response.IsSuccessful) {
          if (this.processs.length - response.Data.length == 0) {
            this.filter.Page = this.filter.Page - 1;
          }
        }
        this.getProcesss();
      });
  }
  onDeleteAnomalyType(anomalyTypeId: number) {
    this.indices = [];
    this.indices.push(anomalyTypeId);
    return this.anomalyTypeService
      .deleteAnomalyTypes(this.indices)
      .subscribe((response: IResponse<number[]>) => {
        if (response.IsSuccessful) {
          if (this.anomalyTypes.length - response.Data.length == 0) {
            this.filter.Page = this.filter.Page - 1;
          }
        }
        // this.getAnomalyTypes();
      });
  }

  onLoadProcess(processId: number) {
    this.process = this.processs.find((c) => c.Id == processId);
    this.processService.process = this.process;
  }
}
