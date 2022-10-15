import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SidebarComponent } from './sidebar.component';
import { MatIconModule } from '@angular/material/icon';
import{MatDividerModule}from '@angular/material/divider';
import{MatListModule}from '@angular/material/list';
@NgModule({
  declarations: [SidebarComponent],
  imports: [
    CommonModule,
    MatIconModule,
    MatDividerModule,
    MatListModule,
  ],
  exports: [
    SidebarComponent,
  ]
})
export class SidebarModule { }