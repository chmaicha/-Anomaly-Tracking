import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { FileUploadComponent } from './media-upload.component';


const routes: Routes = [
  {
    path: 'file-upload',
    component: FileUploadComponent
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  declarations: [],
  exports: [RouterModule]
})
export class FileUploadRoutingModule { }
