import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { LvfNotificationType } from 'src/app/views/core.domain/_shared/enums/lvf-user-notification';
import { User } from 'src/app/views/core.domain/_shared/models/user/user';
import { UserNotification } from 'src/app/views/core.domain/_shared/models/user/user-notification';
import { ContextService } from 'src/app/views/core.domain/_shared/services/common/context.service';
import { UserService } from 'src/app/views/core.domain/_shared/services/user.service';
import { environment } from "src/environments/environment";
import { AuthentificationService } from '../../../../../views/authentification.domain/_shared/services/authentification.service';
import { NavigationService } from '../../../../../views/core.domain/_shared/services/common/navigation.service';
import { SearchService } from '../../../../services/search.service';


interface INotification {
  Icon: string;
  LvfNotificationType: LvfNotificationType;
  LvfNotificationLabel: string;
  NotificationDate: string;
  Count: number;
}
@Component({
  selector: 'app-header-sidebar-large',
  templateUrl: './header-sidebar-large.component.html',
  styleUrls: ['./header-sidebar-large.component.scss']
})

export class HeaderSidebarLargeComponent implements OnInit {

  notifications: UserNotification[];
  isCount: Boolean;
  user: User;
  dialogService: any;
  defaultAvatar = `${environment.staticFilesUrl}/avatar.png`;
  logo = `${environment.style.headerLogo}`;

  constructor(
    private navService: NavigationService,
    public searchService: SearchService,
    private auth: AuthentificationService,
    public contextService: ContextService,
    private userSevice: UserService,
    private router: Router,
    private translate: TranslateService,
  ) {

  }

  ngOnInit() {
    if (this.contextService.data.User) {
      this.user = new User(this.contextService.data.User);
      this.notifications = this.contextService.data.Notifications.map(n => n).filter(n => n.Count > 0);
    }
  }


  navigate(lvfNotificationType: LvfNotificationType) {

    switch (lvfNotificationType) {
      case LvfNotificationType.PRICING_EXPIRATION_ALERT:
        this.router.navigateByUrl('/pricing/list');
        break;
      default:
        break;
    }
  }

  toggelSidebar() {
    const state = this.navService.sidebarState;

    if (state.childnavOpen && state.sidenavOpen) {
      return state.childnavOpen = false;
    }

    if (!state.childnavOpen && state.sidenavOpen) {
      return state.sidenavOpen = false;
    }

    if (!state.sidenavOpen && !state.childnavOpen) {
      state.sidenavOpen = true;
      setTimeout(() => {
        state.childnavOpen = true;
      }, 50);
    }
  }

  signout() {
    this.auth.signout();
  }

  changeLanguage(lang: string) {
    this.translate.use(lang);
    this.translate.setDefaultLang(lang);
  }
}
