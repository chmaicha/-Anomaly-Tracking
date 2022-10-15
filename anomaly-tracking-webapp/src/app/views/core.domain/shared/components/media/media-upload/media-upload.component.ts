import { Component, OnInit, Output, Input, EventEmitter, ViewChild, TemplateRef, ElementRef } from '@angular/core';
import { Media } from '../../../models/common/media';
import { CropperSettings, ImageCropperComponent } from 'ngx-img-cropper';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-file-upload',
  templateUrl: './media-upload.component.html',
  styleUrls: ['./media-upload.component.scss']
})
export class FileUploadComponent implements OnInit {
  @ViewChild('deletemedia', { static: true }) deletemedia: TemplateRef<any>;
  @Input() media: Media;
  @Input() defaultMedia: string = null;
  @Output() selected = new EventEmitter<any>();
  @Output() onDeletePicture = new EventEmitter<Media>();

  fileData: File = null;
  cropperSettings: CropperSettings;
  imageCropData: any;
  previewUrl: any = null;
  formData: FormData;


  constructor(
    private modalService: NgbModal,
  ) {
    this.cropperSettings = new CropperSettings();
    this.cropperSettings.width = 100;
    this.cropperSettings.height = 100;
    this.cropperSettings.croppedWidth = 100;
    this.cropperSettings.croppedHeight = 100;
    this.cropperSettings.canvasWidth = 400;
    this.cropperSettings.canvasHeight = 300;
    this.cropperSettings.cropperDrawSettings.lineDash = true;
    this.cropperSettings.cropperDrawSettings.dragIconStrokeWidth = 0;
    this.imageCropData = {};

  }

  ngOnInit() {
    this.formData = new FormData();
  }


  public onDelete() {
    this.modalService.open(this.deletemedia, {centered: true})
  }


  //Cropper Modal
  // public open(modal: any) {
  //   this.modalService.open(modal, { ariaLabelledBy: 'modal-basic-title' })
  // }

  public fileProgress(fileInput: any) {
    this.fileData = <File>fileInput.target.files[0];
    this.preview();
  }

  private preview() {
    // Show preview 
    var mimeType = this.fileData.type;
    if (mimeType.match(/image\/*/) == null) {
      return;
    }

    var reader = new FileReader();
    reader.readAsDataURL(this.fileData);
    reader.onload = (_event) => {
      this.previewUrl = reader.result;
    }
    this.onSelectPicture();
  }

  private onSelectPicture() {

    if (this.fileData) {
      this.formData = new FormData();
      this.formData.append(this.fileData.type, this.fileData, this.fileData.name);
    }

    this.selected.emit(this.formData);
  }

  public resetUpload() {
    this.formData = null;
    this.fileData = null;

    if (this.media.Id) {
      this.media = Media.createMediaInstance();
    } else {
      this.previewUrl = null;
    }
    this.onSelectPicture();
    this.onUploadPicture();
  }

  private onUploadPicture(){
    this.onDeletePicture.emit(this.media);
  }

}
