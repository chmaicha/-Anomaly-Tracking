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
  Client,
  IClientInfo,
} from '../../anomalytracking.domain/_shared/models/client/client';
import {
  IMoldInfo,
  Mold,
} from '../../anomalytracking.domain/_shared/models/mold/mold';
import { Process } from '../../anomalytracking.domain/_shared/models/process/process';
import { ClientService } from '../../anomalytracking.domain/_shared/services/client.service';
import { MoldService } from '../../anomalytracking.domain/_shared/services/mold.service';
import { mainContentAnimation } from '../../core.domain/shared/animation';
import { SearchFilterBase } from '../../core.domain/shared/filters/search-filter-base';
import { IResponse } from '../../core.domain/shared/models/common/iexception';
import { FilterService } from '../../core.domain/shared/services/common/filter.service';
import { SidebarService } from '../../core.domain/shared/services/sideBar.service';

@Component({
  selector: 'app-project-mold-list',
  templateUrl: './project-mold-list.component.html',
  styleUrls: ['./project-mold-list.component.scss'],
  animations: [mainContentAnimation()],
})
export class ProjectMoldListComponent implements OnInit {
  @ViewChild('moldDetailPopup', { static: true })
  moldDetailPopup: TemplateRef<any>;
  sidebarState!: string;
  client: Client;
  molds: Mold[];
  mold: Mold;
  searchfilterMold: string = '';
  filter: SearchFilterBase;
  indices: number[];
  clientId: number;
  label: string;
  constructor(
    private filterService: FilterService,
    private sidebarService: SidebarService,
    private route: ActivatedRoute, // this will give us access to the label of the user passed in the project
    private clientService: ClientService,
    private moldService: MoldService,
    private modalService: NgbModal
  ) {}

  ngOnInit(): void {
    this.clientId = +this.route.snapshot.paramMap.get('id');
    this.client=Client.createInstance(Client);
    this.get();
    this.molds = this.client.Molds ;
    // this.get();
    this.sidebarService.sidebarStateObservable$.subscribe(
      (newState: string) => {
        this.sidebarState = newState;
      }
    );
  }

  openModalMold() {
    this.mold = Mold.createInstance(Mold);
    this.modalService.open(this.moldDetailPopup, {
      centered: true,
      size: 'md',
    });
  }
  onSave() {
    if (this.mold.Id) {
      this.update();
    } else {
      this.create();
    }
  }
  private update() {
    this.moldService
      .update(this.mold)
      .subscribe((response: IResponse<IMoldInfo>) => {
        if (response.IsSuccessful) {
          this.mold = new Mold(response.Data);
        }
      });
  }

  private create() {
    this.mold.ClientId = this.clientId;
    this.moldService
      .create(this.mold)
      .subscribe((response: IResponse<IMoldInfo>) => {
        if (response.IsSuccessful) {
          this.mold = new Mold(response.Data);
        }
        this.get();
      });
  }
  get() {
    this.clientService
      .get(this.clientId)
      .subscribe((response: IResponse<IClientInfo>) => {
        if (response.IsSuccessful) {
          this.client = new Client(response.Data);
          this.molds = this.client.Molds ;
          this.label = this.client.Label ;
        }
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
        this.get();
      });
  }
  editMold(moldId: number) {
    this.mold = this.molds.find((u) => u.Id == moldId);
    this.modalService.open(this.moldDetailPopup, {
      centered: true,
      size: 'md',
    });
  }

  // getMolds(page: number = 1) {
  //   let filter = this.filterService.getFilterBase(false);
  //   filter.SearchInput = this.searchfilterMold;
  //   this.filter.Page = page;

  //   if (this.filter.Page < 1) {
  //     this.filter.Page = 1;
  //   }
  //   this.moldService
  //     .getAll(filter)
  //     .subscribe((response: IResponse<IMoldInfo[]>) => {
  //       if (response.IsSuccessful) {
  //         this.molds = response.Data
  //           ? response.Data.map((info) => new Mold(info))
  //           : [];
  //         this.filter.TotalCount = response.TotalCount;
  //       }
  //     });
  // }
}
