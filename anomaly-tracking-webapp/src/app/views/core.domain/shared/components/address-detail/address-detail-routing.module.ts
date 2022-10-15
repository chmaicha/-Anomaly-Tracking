import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AddressDetailComponent } from './address-detail.component';


const routes: Routes = [
  {
    path: 'detail',
    component: AddressDetailComponent
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AddressDetailRoutingModule { }
