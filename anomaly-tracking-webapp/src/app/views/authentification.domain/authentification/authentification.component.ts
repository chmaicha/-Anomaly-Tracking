import { Component, EventEmitter, Output } from '@angular/core';
import { Router } from '@angular/router';
import { User } from '../../anomalytracking.domain/_shared/models/user/user';
import { ContextService } from '../../core.domain/shared/services/common/context.service';
import { DialogService } from '../../core.domain/shared/services/common/dialog.service';
import { AuthentificationService } from '../shared/services/authentification.service';
import { LocalService } from '../../anomalytracking.domain/_shared/services/local.service';
@Component({
  selector: 'app-authentification',
  templateUrl: './authentification.component.html',
  styleUrls: ['./authentification.component.scss'],
})
export class AuthentificationComponent {
  supervisor: string;
  public CurrentUserRole : any;
  public showPassword: boolean = false;
  user: User;
  IsSupervisor: boolean;
  WrongCodeMessage: boolean;
  WrongPasswordMessage : boolean;
  @Output() featureSelected = new EventEmitter<string>();


  constructor(
    private auth: AuthentificationService,
    private contextService: ContextService,
    private dialogService: DialogService,
    private router: Router,
    private localStore : LocalService,
  ) {}

  ngOnInit(): void {
    this.user = User.createInstance(User);
    this.IsSupervisor = false;

  }

  public togglePasswordVisibility(): void {
    this.showPassword = !this.showPassword;
  }

  login() {

    this.auth.login(this.user).subscribe((response) => {

      if (response.IsSuccessful) {
        this.contextService.setContextData(response.Data);
        if (this.contextService.data.User) {
          if (this.contextService.data.User.LvfUserRole == 1) {
            this.IsSupervisor = true;
            this.WrongPasswordMessage = false

            if(this.user.Password === this.contextService.data.User.Password){
              this.localStore.saveData('CurrentUserName' , this.contextService.data.User.FirstName)
              this.localStore.saveData('CurrentUserId' , String(this.contextService.data.User.Id))
              console.log(this.localStore.getData('CurrentUserId'))
              this.CurrentUserRole = this.contextService.data.User.LvfUserRole
              this.localStore.saveData('CurrentUserRole' , this.CurrentUserRole)
              this.router.navigateByUrl('/dashboard');

            }else{
              this.WrongPasswordMessage = true

            }
            if(this.user.Password ==undefined){
              this.WrongPasswordMessage = false

            }


          } else {
            this.localStore.saveData('CurrentUserName' , this.contextService.data.User.FirstName)
            this.localStore.saveData('CurrentUserId' , String(this.contextService.data.User.Id))
            this.CurrentUserRole = this.contextService.data.User.LvfUserRole
            this.localStore.saveData('CurrentUserRole' , this.CurrentUserRole)
            this.router.navigateByUrl('/anomaly-declaration');
        }
        }else{
          this.WrongCodeMessage = true

        }
      }
    });
  }

}