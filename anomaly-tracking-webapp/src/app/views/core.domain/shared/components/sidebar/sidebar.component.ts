import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { LocalService } from 'src/app/views/anomalytracking.domain/_shared/services/local.service';
import { SettingDialogueComponent } from 'src/app/views/supervisor.domain/setting-dialogue/setting-dialogue.component';
import { iconAnimation, labelAnimation, sidebarAnimation } from '../../animation';
import { SidebarService } from '../../services/sideBar.service';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss'],
  animations: [
    sidebarAnimation(),
    iconAnimation(),
    labelAnimation(),
  ]
})
export class SidebarComponent implements OnInit {
  sidebarState!: string;
  CurrentUserRole : string;
  constructor(
    private sidebarService: SidebarService,
    private dialog : MatDialog,
    private localStore : LocalService,
  ) { }
  OpenImprimentsettings() {
    this.dialog.open(SettingDialogueComponent, {
    width:'30%'
    });
  }
  ngOnInit() {
    this.CurrentUserRole = this.localStore.getData('CurrentUserRole')
    this.sidebarService.sidebarStateObservable$.
      subscribe((newState: string) => {
        this.sidebarState = newState;
      });
  }
}
