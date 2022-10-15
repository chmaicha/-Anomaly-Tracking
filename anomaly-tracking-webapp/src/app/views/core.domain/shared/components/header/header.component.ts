import { Component, OnInit } from '@angular/core';
import { SettingDialogueComponent } from 'src/app/views/supervisor.domain/setting-dialogue/setting-dialogue.component';
import { MatDialog } from '@angular/material/dialog';
import { ContextService } from '../../services/common/context.service';
import { LocalService } from 'src/app/views/anomalytracking.domain/_shared/services/local.service';
@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
  CurrentUsername : string;
  constructor(private dialog : MatDialog,
    private contextService: ContextService,
    private localStore : LocalService,
    ) { }
  OpenImprimentsettings() {
    this.dialog.open(SettingDialogueComponent, {
    width:'30%'
    });
  }
  ngOnInit() {
    this.CurrentUsername=this.localStore.getData('CurrentUserName')

  }
  logout(){
    this.localStore.removeData('CurrentUserName')
    this.localStore.removeData('CurrentUserRole')

  }

}
