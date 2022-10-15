import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FileUploadComponent } from './media-upload.component';
import { TranslateModule } from '@ngx-translate/core';
import { FileUploadRoutingModule } from './media-upload-routing.module';
import { HttpClientModule } from '@angular/common/http';
import { ImageCropperModule } from 'ngx-img-cropper';
import { NgbTooltipModule } from '@ng-bootstrap/ng-bootstrap';



@NgModule({
  exports: [
    TranslateModule,
    FileUploadComponent,
    HttpClientModule
  ],
  declarations: [FileUploadComponent],
  imports: [
    CommonModule,
    TranslateModule,
    FileUploadRoutingModule,
    HttpClientModule,
    ImageCropperModule,
    NgbTooltipModule,
  ]
})
export class FileUploadModule { }
