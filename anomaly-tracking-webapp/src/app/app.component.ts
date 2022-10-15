import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { MDBBootstrapModule } from 'angular-bootstrap-md';
import { SidebarService } from './views/core.domain/shared/services/sideBar.service';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
 
})

export class AppComponent {

  constructor(
    private translateService: TranslateService,
  ) {
    translateService.addLangs(['en', 'fr']);
    translateService.setDefaultLang('en');
  }
 
}

