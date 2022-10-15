import { Component, Input, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal, NgbModalConfig } from '@ng-bootstrap/ng-bootstrap';
import {
  Client,
  IClientInfo,
} from 'src/app/views/anomalytracking.domain/_shared/models/client/client';
import {
  IMoldInfo,
  Mold,
} from 'src/app/views/anomalytracking.domain/_shared/models/mold/mold';
import { ClientService } from 'src/app/views/anomalytracking.domain/_shared/services/client.service';
import { LocalService } from 'src/app/views/anomalytracking.domain/_shared/services/local.service';
import { MoldService } from 'src/app/views/anomalytracking.domain/_shared/services/mold.service';
import { mainContentAnimation } from 'src/app/views/core.domain/shared/animation';
import { SearchFilterBase } from 'src/app/views/core.domain/shared/filters/search-filter-base';
import { IResponse } from 'src/app/views/core.domain/shared/models/common/iexception';
import { FilterService } from 'src/app/views/core.domain/shared/services/common/filter.service';
import { SidebarService } from 'src/app/views/core.domain/shared/services/sideBar.service';

@Component({
  selector: 'app-project-management',
  templateUrl: './project-management.component.html',
  styleUrls: ['./project-management.component.scss'],
  animations: [mainContentAnimation()],
})
export class ProjectManagementComponent implements OnInit {
  @ViewChild('clientDetailPopup', { static: true })
  clientDetailPopup: TemplateRef<any>;
  @ViewChild('moldDetailPopup', { static: true })
  moldDetailPopup: TemplateRef<any>;
  searchfilterClient: string = '';
  sidebarState!: string;
  clients: Client[];
  client: Client;
  @Input() molds: Mold[];
  mold: Mold;
  filter: SearchFilterBase;
  indices: number[];
  role = false;
  constructor(
    private localStore : LocalService,
    private route : ActivatedRoute ,
    private filterService: FilterService,
    private clientService: ClientService,
    private moldService: MoldService,
    private sidebarService: SidebarService,
    private modalService: NgbModal,
    private router: Router,
    config: NgbModalConfig
  ) {}


  ngOnInit() {
    if(this.localStore.getData('CurrentUserName')){
      if(this.localStore.getData('CurrentUserRole') === '1'){
        this.filter = this.filterService.getFilterBase(true);
        this.getClients();
        this.getMolds();
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
  onLoadProject(projectId: number){
    this.client = this.clients.find((c) => c.Id == projectId);
    this.clientService.client= this.client;
   }
  showInput() {
    this.role = true;
  }
  hideInput() {
    this.role = false;
  }
  onSave() {
    if (this.client.Id) {
      this.update();
    } else {
      this.create();
    }
  }
  onSaveMold() {
    if (this.mold.Id) {
      this.updateMold();
    } else {
      this.createMold();
    }
  }

  private update() {
    this.clientService
      .update(this.client)
      .subscribe((response: IResponse<IClientInfo>) => {
        if (response.IsSuccessful) {
          this.client = new Client(response.Data);
        }
      });
  }
  private updateMold() {
    this.moldService
      .update(this.mold)
      .subscribe((response: IResponse<IMoldInfo>) => {
        if (response.IsSuccessful) {
          this.mold = new Mold(response.Data);
        }
      });
  }

  private create() {
    this.clientService.create(this.client)
      .subscribe((response: IResponse<IClientInfo>) => {
        if (response.IsSuccessful) {
          this.client = new Client(response.Data);
          this.getClients();
        }
        console.log(response);
      });
  }

  addForm = new FormGroup({
    Label: new FormControl(''),
    Email: new FormControl(''),
    PhoneNumber: new FormControl(''),
  });
  private createMold() {
    this.moldService
      .create(this.mold)
      .subscribe((response: IResponse<IMoldInfo>) => {
        if (response.IsSuccessful) {
          this.mold = new Mold(response.Data);
          this.getMolds();
        }
      });
  }

  openModalProject(){
    this.client = Client.createInstance(Client);
    this.modalService.open(this.clientDetailPopup, { centered: true, size: 'md' });
  }

  openModalMold() {
    this.mold = Mold.createInstance(Mold);
    this.modalService.open(this.moldDetailPopup, {
      centered: true,
      size: 'lg',
    });
  }

  editClient(clientId: number) {
    this.client = this.clients.find((u) => u.Id == clientId);
    this.modalService.open(this.clientDetailPopup, {
      centered: true,
      size: 'lg',
    });
  }
  editMold(moldId: number) {
    this.mold = this.molds.find((a) => a.Id == moldId);
  }

  getClients(page: number = 1) {
    let filter = this.filterService.getFilterBase(false);
    filter.SearchInput = this.searchfilterClient;
    this.filter.Page = page;

    if (this.filter.Page < 1) {
      this.filter.Page = 1;
    }
    this.clientService
      .getAll(filter)
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
        }
      });
  }
  onDeleteClient(clientId: number) {
    this.indices = [];
    this.indices.push(clientId);
    return this.clientService
      .deleteClients(this.indices)
      .subscribe((response: IResponse<number[]>) => {
        if (response.IsSuccessful) {
          if (this.clients.length - response.Data.length == 0) {
            this.filter.Page = this.filter.Page - 1;
          }
        }
        this.getClients();
      });
  }
  onDeleteMold(moldId: number) {
    this.indices = [];
    this.indices.push(moldId);
    return this.moldService
      .deleteMolds(this.indices)
      .subscribe((response: IResponse<number[]>) => {
        if (response.IsSuccessful) {
          if (this.molds.length - response.Data.length == 0) {
            this.filter.Page = this.filter.Page - 1;
          }
        }
        this.getMolds();
      });
  }


  }

