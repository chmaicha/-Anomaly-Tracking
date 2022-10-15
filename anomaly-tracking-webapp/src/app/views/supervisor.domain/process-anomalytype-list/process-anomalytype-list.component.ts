import {
  Component,
  Input,
  OnInit,
  TemplateRef,
  ViewChild,
} from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import {
  Process,
  IProcessInfo,
} from '../../anomalytracking.domain/_shared/models/process/process';
import {
  AnomalyType,
  IAnomalyTypeInfo,

} from '../../anomalytracking.domain/_shared/models/anomalyType/anomalyType';
import { ProcessService } from '../../anomalytracking.domain/_shared/services/process.service';
import { AnomalyTypeService } from '../../anomalytracking.domain/_shared/services/anomalyType.service';
import { mainContentAnimation } from '../../core.domain/shared/animation';
import { SearchFilterBase } from '../../core.domain/shared/filters/search-filter-base';
import { IResponse } from '../../core.domain/shared/models/common/iexception';
import { FilterService } from '../../core.domain/shared/services/common/filter.service';
import { SidebarService } from '../../core.domain/shared/services/sideBar.service';

@Component({
  selector: 'app-process-anomalytype-list',
  templateUrl: './process-anomalytype-list.component.html',
  styleUrls: ['./process-anomalytype-list.component.scss'],
  animations: [mainContentAnimation()],
})
export class ProcessAnomalytypeListComponent implements OnInit {
  @ViewChild('anomalytypeDetailPopup', { static: true })
  anomalytypeDetailPopup: TemplateRef<any>;
  sidebarState!: string;
  process: Process;
  anomalyTypes: AnomalyType[] = [];
  anomalyType: AnomalyType;
  searchfilterAnomalyType: string = '';
  filter: SearchFilterBase;
  indices: number[];
  processId: number;
  label: string;
  constructor(
    private filterService: FilterService,
    private sidebarService: SidebarService,
    private route: ActivatedRoute, // this will give us access to the label of the user passed in the project
    private processService: ProcessService,
    private anomalyTypeService: AnomalyTypeService,
    private modalService: NgbModal
  ) {}

  ngOnInit(): void {
    this.processId = +this.route.snapshot.paramMap.get('id');
    // this.process = Process.createInstance(Process)
    this.get();
    this.sidebarService.sidebarStateObservable$.subscribe(
      (newState: string) => {
        this.sidebarState = newState;
      }
    );
  }

  openModalAnomalyType() {
    this.anomalyType = AnomalyType.createInstance(AnomalyType);
    this.modalService.open(this.anomalytypeDetailPopup, {
      centered: true,
      size: 'md',
    });
  }
  onSave() {
    if (this.anomalyType.Id) {
      this.update();
    } else {
      this.create();
    }
  }
  private update() {
    this.anomalyTypeService
      .update(this.anomalyType)
      .subscribe((response: IResponse<IAnomalyTypeInfo>) => {
        if (response.IsSuccessful) {
          this.anomalyType = new AnomalyType(response.Data);
        }
      });
  }

  private create() {
    this.anomalyType.ProcessId = this.processId ;
    this.anomalyTypeService
      .create(this.anomalyType)
      .subscribe((response: IResponse<IAnomalyTypeInfo>) => {
        if (response.IsSuccessful) {
          this.anomalyType = new AnomalyType(response.Data);
        }
        this.get();
      });
  }

  get() {
    this.processService
    .get(this.processId)
      .subscribe((response: IResponse<IProcessInfo>) => {
        if (response.IsSuccessful) {
          this.process = new Process(response.Data);
          this.anomalyTypes = this.process.AnomalyTypes;
          this.label = this.process.Label;
        }
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
        this.get();
      });
      
  }
  editAnomalyType(anomalyTypeId: number) {
    this.anomalyType = this.anomalyTypes.find((u) => u.Id == anomalyTypeId);
    this.modalService.open(this.anomalytypeDetailPopup, {
      centered: true,
      size: 'md',
    });
  }


}
