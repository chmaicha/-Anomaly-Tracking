import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { mainContentAnimation } from '../../core.domain/shared/animation';
import { SidebarService } from '../../core.domain/shared/services/sideBar.service';
import { FacePositionComponent } from '../../core.domain/shared/components/face-position/face-position.component';

import {
  User,
  IUserInfo,
} from 'src/app/views/anomalytracking.domain/_shared/models/user/user';
import { UserService } from 'src/app/views/anomalytracking.domain/_shared/services/user.service';
import { SearchFilterBase } from 'src/app/views/core.domain/shared/filters/search-filter-base';
import { IResponse } from 'src/app/views/core.domain/shared/models/common/iexception';
import { FilterService } from 'src/app/views/core.domain/shared/services/common/filter.service';
import {
  EnumHelper,
  IEnumEntry,
} from '../../core.domain/shared/_helpers/enum.helper';
import { LvfUserRole } from '../../core.domain/shared/enums/lvf-user-role';
import { NgbModal, NgbModalConfig } from '@ng-bootstrap/ng-bootstrap';
import { Router, RouterLink } from '@angular/router';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { LocalService } from '../../anomalytracking.domain/_shared/services/local.service';

@Component({
  selector: 'app-user-management',
  templateUrl: './user-management.component.html',
  styleUrls: ['./user-management.component.scss'],
  animations: [mainContentAnimation()],
})
export class UserManagementComponent implements OnInit {
  @ViewChild('userDetailPopup', { static: true })
  userDetailPopup: TemplateRef<any>;
  reactiveform! : FormGroup;
  createoredit : string ;
  sidebarState!: string;
  users: User[];
  user: User;
  filter: SearchFilterBase;
  indices: number[];
  lvfUserRoles: IEnumEntry<LvfUserRole>[];
  searchfilterUser: string = '';
  public showPassword: boolean = false;
  public showPassword2: boolean = false;
  public showPassword3: boolean = false;
  RealPassword : string;
  public IsPasswordTrue: boolean ;
  oldpasswordrequired = 'true';
  newpasswordrequired = 'true';
  confirmpasswordrequired = 'true';
  public isoperator : string;
  public switch : boolean;
  ConfirmPassword : string;
  public ishidden : boolean = true;
  public reallvfroleuser : number;
  public operatortosuperuser : boolean;
  constructor(
    private localStore : LocalService,

    private dialog: MatDialog,
    private filterService: FilterService,
    private userService: UserService,
    private sidebarService: SidebarService,
    private modalService: NgbModal,
    private router: Router,
    config: NgbModalConfig,
    private formbuilder : FormBuilder
  ) {
    this.reactiveform = this.formbuilder.group({
      Password : new FormControl('',),
      FirstName : new FormControl('',),
      LastName : new FormControl('', ),
      ConfirmPassword : new FormControl('',),
      LvfUserRole : new FormControl('', ),
      Code : new FormControl('', ),
      OldPassword : new FormControl('', ),


    },
    {
      validators: [

      this.Mustmatch('Password', 'ConfirmPassword'),

       this.MustmatchOldPassword('OldPassword'),

      ]
    }


    )
  }

  public togglePasswordVisibility(): void {
    this.showPassword = !this.showPassword;
  }
  public togglePasswordVisibility2(): void {
    this.showPassword2 = !this.showPassword2;
  }
  public togglePasswordVisibility3(): void {
    this.showPassword3 = !this.showPassword3;
  }

  // function called when we change the userrole in update or create
  // it allows a certain fields in html file  to be shown or hidden
  onChangeRole($event){

    this.isoperator= $event.target.id;
    console.log(this.isoperator)
    if(this.isoperator === 'radio-2'){
      this.ishidden === true
    }
    else if(this.isoperator==="radio-1"  ) {

        this.ishidden === false
      console.log(this.ishidden)

    }

  }
  // function thats controls the switch button, also allows certain fields to be shown or hidden
  AllowChangePassword($event){

    this.switch = $event.target.checked
    if($event.target.checked ===  true){
      this.ishidden=false
    }else if ($event.target.checked ===  false){

      this.ishidden=true

    }
  }
  // returns error message from a validator function
  get f(){
    return this.reactiveform.controls;
  }

  //validator function that verifies if the passwords match
  Mustmatch(Password: any, ConfirmPassword: any){

    return(formGroup:FormGroup)=>{
      const Passwordcontrol = formGroup.controls[Password];
      const ConfirmPasswordcontrol = formGroup.controls[ConfirmPassword];

      if(ConfirmPasswordcontrol.errors && !ConfirmPasswordcontrol.errors['Mustmatch']){

        return;
      }
      if(Passwordcontrol.value !== ConfirmPasswordcontrol.value && this.user.LvfUserRole === 1
         && (this.createoredit==='create' || this.switch === true) ||(Passwordcontrol.value !== ConfirmPasswordcontrol.value && this.user.LvfUserRole === 1
           && this.reallvfroleuser === 2 )){

          ConfirmPasswordcontrol.setErrors({Mustmatch : true});



      }else {
        ConfirmPasswordcontrol.setErrors(null);
      }
    }

  }
  // verify if old password is correct
  MustmatchOldPassword(OldPassword: any){
    return(formGroup:FormGroup)=>{
      const OldPasswordControl = formGroup.controls[OldPassword];

      if(OldPasswordControl.errors && !OldPasswordControl.errors['MustmatchOldPassword']){
        return;
      }
      if(OldPasswordControl.value !== this.RealPassword && this.switch===true && OldPasswordControl.value!=="" ){
        OldPasswordControl.setErrors({MustmatchOldPassword : true});
        this.IsPasswordTrue = true


      }else{
        OldPasswordControl.setErrors(null);
      }
    }

  }

  ngOnInit() {
    if(this.localStore.getData('CurrentUserName')){
      if(this.localStore.getData('CurrentUserRole') ==='1'){
        this.lvfUserRoles = EnumHelper.getEnumValues(
          'app.lvfuserrole.',
          LvfUserRole
        );
        this.filter = this.filterService.getFilterBase(true);
        this.getUsers();
        this.sidebarService.sidebarStateObservable$.subscribe(
          (newState: string) => {
            this.sidebarState = newState;
          }
        );


      }else{
        this.router.navigateByUrl('/anomaly-declaration');

      }

    }else{
      this.router.navigateByUrl('/');

    }


  }

  onSave() {
    if (this.user.Id) {
      this.update();
    } else {
      this.create();
    }
  }

  private update() {
    if(this.user.LvfUserRole === 2){
      this.user.Password=""
    }
    this.userService
      .update(this.user)
      .subscribe((response: IResponse<IUserInfo>) => {
        if (response.IsSuccessful) {
          this.user = new User(response.Data);
        }
      });
  }
  private create() {
    if(this.user.LvfUserRole === 2){
      this.user.Password=""
    }
    this.userService
      .create(this.user)
      .subscribe((response: IResponse<IUserInfo>) => {
        if (response.IsSuccessful) {
          this.user = new User(response.Data);
          this.getUsers();
        }
      });
  }
  openModal() {
    this.ishidden=false

    this.user = User.createInstance(User);
    this.modalService.open(this.userDetailPopup, {
      centered: true,
      size: 'md',

    });
    this.createoredit = "create"
  }

  editUser(userId: number) {
    this.ishidden=true
    this.user = this.users.find((u) => u.Id == userId);
    this.reallvfroleuser= this.user.LvfUserRole
    this.modalService.open(this.userDetailPopup, {
      centered: true,
      size: 'md',
    });
    this.RealPassword = this.user.Password
    this.user.Password=''
    this.createoredit = "edit"

  }

  getUsers(page: number = 1) {
    let filter = this.filterService.getFilterBase(false);
    filter.SearchInput = this.searchfilterUser;
    this.filter.Page = page;

    if (this.filter.Page < 1) {
      this.filter.Page = 1;
    }
    this.userService
      .getAll(filter)
      .subscribe((response: IResponse<IUserInfo[]>) => {
        if (response.IsSuccessful) {
          this.users = response.Data
            ? response.Data.map((info) => new User(info))
            : [];
          this.filter.TotalCount = response.TotalCount;
        }
      });
      console.log(this.users)
  }

  onDeleteUser(userId: number) {
    this.indices = [];
    this.indices.push(userId);

    return this.userService
      .deleteUsers(this.indices)
      .subscribe((response: IResponse<number[]>) => {
        if (response.IsSuccessful) {
          if (this.users.length - response.Data.length == 0) {
            this.filter.Page = this.filter.Page - 1;
          }
        }
        this.getUsers();
      });
  }
}
