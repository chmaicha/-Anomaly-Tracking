import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from './header.component';

import { MatToolbarModule } from '@angular/material/toolbar';
import { ToggleSidebarModule } from '../sidebar/toggle-sidebar/toggle-sidebar.module';

@NgModule({
  declarations: [HeaderComponent],
  imports: [
    CommonModule,
    ToggleSidebarModule,
    MatToolbarModule,
  ],
  exports: [
    HeaderComponent,
  ]
})
export class HeaderModule { }