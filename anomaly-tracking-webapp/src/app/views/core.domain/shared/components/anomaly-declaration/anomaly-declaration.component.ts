import {
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
  TemplateRef,
  ViewChild,
} from '@angular/core';
import { MatChip, MatChipInputEvent } from '@angular/material/chips';
import { MatDialog } from '@angular/material/dialog';
import { FacePositionComponent } from '../face-position/face-position.component';
import { mainContentAnimation } from '../../animation';
import { SidebarService } from '../../services/sideBar.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { FacePosition1Component } from '../face-position1/face-position1.component';
import { FacePosition2Component } from '../face-position2/face-position2.component';
import { ActivatedRoute, Router } from '@angular/router';
import { IResponse } from 'src/app/views/core.domain/shared/models/common/iexception';
import {
  AnomalyDeclaration,
  IAnomalyDeclarationInfo,
} from 'src/app/views/anomalytracking.domain/_shared/models/anomalyDeclaration/anomalyDeclaration';
import { AnomalyDeclarationService } from 'src/app/views/anomalytracking.domain/_shared/services/anomalyDeclaration.service';
import { SearchFilterBase } from '../../filters/search-filter-base';
import { LvfUserRole } from '../../enums/lvf-user-role';
import { IEnumEntry } from '../../_helpers/enum.helper';
import { FilterService } from '../../services/common/filter.service';
import {
  Client,
  IClientInfo,
} from 'src/app/views/anomalytracking.domain/_shared/models/client/client';
import { ClientService } from 'src/app/views/anomalytracking.domain/_shared/services/client.service';
import {
  IMoldInfo,
  Mold,
} from 'src/app/views/anomalytracking.domain/_shared/models/mold/mold';
import {
  IProcessInfo,
  Process,
} from 'src/app/views/anomalytracking.domain/_shared/models/process/process';
import {
  AnomalyType,

} from 'src/app/views/anomalytracking.domain/_shared/models/anomalyType/anomalyType';
import { FaceService } from 'src/app/views/anomalytracking.domain/_shared/services/face.service';
import { Face, IFaceInfo } from 'src/app/views/anomalytracking.domain/_shared/models/face/face';
import { UserService } from 'src/app/views/anomalytracking.domain/_shared/services/user.service';
import { StatusEnum } from 'src/app/views/anomalytracking.domain/_shared/enums/StatusEnum';
import {
  Cavity,
} from 'src/app/views/anomalytracking.domain/_shared/models/cavity/cavity';
import { CavityService } from 'src/app/views/anomalytracking.domain/_shared/services/cavity.service';
import { MoldService } from 'src/app/views/anomalytracking.domain/_shared/services/mold.service';
import { ProcessService } from 'src/app/views/anomalytracking.domain/_shared/services/process.service';
import { AnomalyTypeService } from 'src/app/views/anomalytracking.domain/_shared/services/anomalyType.service';
import { Position } from 'ngx-perfect-scrollbar';
import { LocalService } from 'src/app/views/anomalytracking.domain/_shared/services/local.service';
import { Anomaly, IAnomalyInfo } from 'src/app/views/anomalytracking.domain/_shared/models/anomaly/anomaly';
import { AnomalyService } from 'src/app/views/anomalytracking.domain/_shared/services/anomaly.service';

@Component({
  selector: 'app-anomaly-declaration',
  templateUrl: './anomaly-declaration.component.html',
  styleUrls: ['./anomaly-declaration.component.scss'],
  animations: [mainContentAnimation()],
})
export class AnomalyDeclarationComponent implements OnInit {
  @ViewChild('facePositionPopup', { static: true })
  facePositionPopup: TemplateRef<any>;
  supervisor: string;
  public hideInput: boolean = false;
  public MOL: boolean = true;
  public myAngularxQrCode: string = null;
  sidebarState!: string;
  firstFormGroup: FormGroup;
  secondFormGroup: FormGroup;
  thirdFormGroup: FormGroup;
  notification: boolean = false;
  option1 = true;
  option2 = false;
  chipSelected = true;
  closeResult = '';
  isShown: string = '';
  anomalyDeclaration: AnomalyDeclaration;
  anomaly : Anomaly ;
  clients: Client[];
  client: Client;
  molds: Mold[];
  molds2: Mold[];
  moldsCavities: Mold[];
  cavities: Cavity[];
  anomalyTypes: AnomalyType[];
  processs: Process[];
  filter: SearchFilterBase;
  indices: number[];
  lvfUserRoles: IEnumEntry<LvfUserRole>[];
  selectedProjectId: number;
  selectedMoldId: number;
  selectedProcessId: number;
  selectedCavityId: number;
  selectedAnomalyTypeId : number ;
  AselectedPositionId : string[] ;
  BselectedPositionId : string[] ;
  selectedfaceId : number ;
  anomalyId : number ;
  faceA: Face; 
  faceB: Face; 
  positions1: string[] = ['1', '2', '3', '4', '5', '6', '7', '8', '9', '10', '11', '12'];
  positions2: string[] = ['1', '2', '3', '4', '5', '6', '7', '8', '9', '10', '11', '12'];
  disablemold = true;
  disablecavity = true;
  disableprocess = true;

  public onError(e): void {
    alert(e);
  }
  constructor(
    private filterService: FilterService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private modalService: NgbModal,
    private dialog: MatDialog,
    private sidebarService: SidebarService,
    private _formBuilder: FormBuilder,
    private clientService: ClientService,
    private processService: ProcessService,
    private moldService: MoldService ,
    private anomalyDeclarationService : AnomalyDeclarationService,
    private anomalyService : AnomalyService ,
    private faceService : FaceService ,
    private localStore : LocalService
  ) {
    this.myAngularxQrCode = 'tutsmake.com';
  }
  ngOnInit() {
    this.filter = this.filterService.getFilterBase(true);
    this.sidebarService.sidebarStateObservable$.subscribe(
      (newState: string) => {
        this.sidebarState = newState;
      }
    );
    this.getClients();
    this.getProcess();

    this.firstFormGroup = this._formBuilder.group({
      firstCtrl: ['', Validators.required],
    });
    this.secondFormGroup = this._formBuilder.group({
      secondCtrl: ['', Validators.required],
    });
    this.thirdFormGroup = this._formBuilder.group({
      thirdCtrl: ['', Validators.required],
    });
  }

  openModalFacePosition() {
    if (this.chipSelected == true) {
      this.modalService.open(this.facePositionPopup, {
        centered: true,
        size: 'lg',
      });
    }
    this.chipSelected = true;
  }
  onSave() {
    // if (this.anomalyDeclaration.Id) {
    //   this.update();
    // } else {
    //   this.createAnomalyDeclaration();
    // }

    this.createFaceA()
    //  this.createAnomaly()
    //  this.createAnomalyDeclaration();
  }
  private update() {
    this.anomalyDeclarationService
      .update(this.anomalyDeclaration)
      .subscribe((response: IResponse<IAnomalyDeclarationInfo>) => {
        if (response.IsSuccessful) {
          this.anomalyDeclaration = new AnomalyDeclaration(response.Data);
          this.router.navigateByUrl('/anomalyDeclaration/list');
        }
      });
  }
  getClients(page: number = 1) {
    this.filter.Page = page;
    if (this.filter.Page < 1) {
      this.filter.Page = 1;
    }
    this.clientService
      .getAll(this.filter)
      .subscribe((response: IResponse<IClientInfo[]>) => {
        if (response.IsSuccessful) {
          this.clients = response.Data
            ? response.Data.map((info) => new Client(info))
            : [];
          this.filter.TotalCount = response.TotalCount;
        }
      });
  }

  getMolds(page: number = 1) {
    this.filter.Page = page;
    if (this.filter.Page < 1) {
      this.filter.Page = 1;
    }
    this.moldService
      .getAll(this.filter)
      .subscribe((response: IResponse<IMoldInfo[]>) => {
        if (response.IsSuccessful) {
          this.molds = response.Data
            ? response.Data.map((info) => new Mold(info))
            : [];
          this.filter.TotalCount = response.TotalCount;
          this.moldsCavities = this.molds;
        }
      });
  }

  getProcess(page: number = 1) {
    this.filter.Page = page;
    if (this.filter.Page < 1) {
      this.filter.Page = 1;
    }
    this.processService
      .getAll(this.filter)
      .subscribe((response: IResponse<IProcessInfo[]>) => {
        if (response.IsSuccessful) {
          this.processs = response.Data
            ? response.Data.map((info) => new Process(info))
            : [];
          this.filter.TotalCount = response.TotalCount;
        }
      });
  }

  selectedClient(event: any) {
    this.selectedProjectId = event.target.value;
    this.molds2 = this.clients.find(
      (c) => c.Id == this.selectedProjectId
    ).Molds;
    this.disablemold = false;
    this.getMolds();
  }

  selectedMold(event: any) {
    this.selectedMoldId = event.target.value;
    this.cavities = this.moldsCavities.find(
      (c) => c.Id == this.selectedMoldId
    ).Cavities;
    this.disablecavity = false;
  }

  selectedCavity(event: any) {
    this.disableprocess = false;
    this.selectedCavityId = event;
  }


  selectedProcess(event: any) {
    this.selectedProcessId = event.target.value;
    this.anomalyTypes = this.processs.find(
      (c) => c.Id == this.selectedProcessId
    ).AnomalyTypes;
    if (event.target.value == StatusEnum.Status1) {
      this.isShown = StatusEnum.Status1;
    } else if (event.target.value == StatusEnum.Status2) {
      this.isShown = StatusEnum.Status2;
    } else {
      this.isShown = StatusEnum.Status3;
    }
  }
  public selectedAnomalyType(event: any) {
  this.selectedAnomalyTypeId = event.Id

  }

  public selectedposition1(event: any) {
    this.AselectedPositionId = event


    }

    public selectedposition2(event: any) {
      this.BselectedPositionId = event


      }
 
      private  createFaceA() {
        debugger
        this.faceA = Face.createInstance(Face);
        this.faceA.Positions = this.AselectedPositionId ;
        this.faceService
          .create(this.faceA)
          .subscribe((response: IResponse<IFaceInfo>) => {
            if (response.IsSuccessful) {
              debugger
              this.faceA = new Face(response.Data)
              console.log(response )
            }
          });
    
      }

      private  createFaceB() {
        debugger
        this.faceB = Face.createInstance(Face);
        this.faceB.Positions = this.BselectedPositionId ;
        this.faceService
          .create(this.faceB)
          .subscribe((response: IResponse<IFaceInfo>) => {
            if (response.IsSuccessful) {
              debugger
              this.faceB = new Face(response.Data)
              console.log(response )
            }
          });
    
      }



   private  createAnomaly() {
    debugger
    this.anomaly = Anomaly.createInstance(Anomaly);
    this.anomaly.AnomalyTypeId = this.selectedAnomalyTypeId ;
    this.anomaly.Faces[0] = this.faceA  ;
    this.anomaly.Faces[1] = this.faceB  ;
    this.anomalyService
      .create(this.anomaly)
      .subscribe((response: IResponse<IAnomalyInfo>) => {
        if (response.IsSuccessful) {
          debugger
          this.anomaly = new Anomaly(response.Data);
          this.anomalyId = this.anomaly.Id
          console.log(this.anomalyId )
        }
      });

  }

  public createAnomalyDeclaration() {
    debugger
    this.anomalyDeclaration = AnomalyDeclaration.createInstance(AnomalyDeclaration);
    this.anomalyDeclaration.UserId = Number(this.localStore.getData('CurrentUserId'));
    this. anomalyDeclaration.AnomalyId = this.anomalyId  ;
    this.anomalyDeclaration.CavityId =  this.selectedCavityId;
    this.anomalyDeclaration.ProcessId = this.selectedProcessId;
    this.anomalyDeclarationService
      .create(this.anomalyDeclaration)
      .subscribe((response: IResponse<IAnomalyDeclarationInfo>) => {
        if (response.IsSuccessful) {
          debugger
          this.anomalyDeclaration = new AnomalyDeclaration(response.Data);
        }
      });
  }
  @Output() featureSelected = new EventEmitter<string>();
  onSelect(feature: string) {
    this.featureSelected.emit(feature);
  }
  changediv(divid: string) {
    if (divid === 'yes') {
      this.option1 = true;
      this.option2 = false;
    } else if (divid === 'no') {
      this.option1 = false;
      this.option2 = true;
    }
  }
  changeoptions() {}
  toggleSelection(chip: MatChip) {
    if (chip.selected) {
      this.chipSelected = false;
    }
    chip.toggleSelected();
  }
  toggleSelection2(chip: MatChip) {
    chip.toggleSelected();
  }
  showNotification() {
    this.notification = true;
  }
  hidenotification() {
    this.notification = false;
  }
  open(content) {
    this.modalService
      .open(content, { ariaLabelledBy: 'modal-basic-title' })
      .result.then(
        (result) => {
          this.closeResult = `Closed with: ${result}`;
        },
        (reason) => {
          this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
        }
      );
  }
  getValue(val: string) {
    this.supervisor = val;
    if (this.supervisor === 'D34678926798') {
      this.hideInput = true;
      this.MOL = false;
    }
  }

  onStatusSelect(event: any) {
    if (event.target.value == StatusEnum.Status1) {
      this.isShown = StatusEnum.Status1;
    } else if (event.target.value == StatusEnum.Status2) {
      this.isShown = StatusEnum.Status2;
    } else {
      this.isShown = StatusEnum.Status3;
    }
  }
  
  private getDismissReason(reason: any): string {
    if (reason === ModalDismissReasons.ESC) {
      return 'by pressing ESC';
    } else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
      return 'by clicking on a backdrop';
    } else {
      return `with: ${reason}`;
    }
  }
}
