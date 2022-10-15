import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AddressDetailRoutingModule } from './address-detail-routing.module';
import { AddressDetailComponent } from './address-detail.component';
import { FormsModule } from '@angular/forms';
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';

@NgModule({
  exports: [
    TranslateModule,
    AddressDetailComponent,
  
  ],
  declarations: [AddressDetailComponent],
  imports: [
    CommonModule,
    FormsModule,
    TranslateModule,
    AddressDetailRoutingModule
  ]
})

export class AddressDetailModule { }
