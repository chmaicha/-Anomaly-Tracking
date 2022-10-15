import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { LocalService } from '../../anomalytracking.domain/_shared/services/local.service';
import { FacePositionComponent } from '../../core.domain/shared/components/face-position/face-position.component';


@Component({
  selector: 'app-dash',
  templateUrl: './dash.component.html',
  styleUrls: ['./dash.component.scss'],

})
export class DashComponent implements OnInit{
  CurrentUserRole: string;
  sidebarState! : string;
  constructor(private dialog : MatDialog,
    private localStore : LocalService,
    private router: Router,


  ) { }
  ngOnInit(): void {
    this.CurrentUserRole = this.localStore.getData('CurrentUserRole')
    if(this.CurrentUserRole === '1'){
      throw new Error('Method not implemented.');

    }else{

      this.router.navigateByUrl('/anomaly-declaration')
    }
  }


  OpenFacePosition() {
    this.dialog.open(FacePositionComponent, {
    width:'70%',
    height:'90%',
    });
  }



}
