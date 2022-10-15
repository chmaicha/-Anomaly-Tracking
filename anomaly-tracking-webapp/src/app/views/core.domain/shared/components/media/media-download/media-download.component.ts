import { Component, OnInit } from '@angular/core';
import { of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { SharedAnimations } from '../../../../../../shared/animations/shared-animations';
import { IResponse } from '../../../models/common/iexception';
import { IMediaInfo, Media } from '../../../models/common/media';
import { DialogService } from '../../../services/common/dialog.service';
import { IToolbarOperationHandler, ToolbarService } from '../../toolbar/toolbar.service';
import { MediaUploadService } from '../media-upload/media-upload.service';
import { MediaDownloadService } from './media-download.service';

@Component({
  selector: 'app-media-download',
  templateUrl: './media-download.component.html',
  styleUrls: ['./media-download.component.scss'],
  animations: [SharedAnimations]
})
export class MediaDownloadComponent implements OnInit {

  indices: number[] = [];
  staticFilesUrl: string;

  constructor(
    public mediaDownloadService: MediaDownloadService,
    public mediaUploadService: MediaUploadService,
    public toolbarService: ToolbarService,
    private dialogService: DialogService
  ) { }

  ngOnInit() {

    if (this.mediaDownloadService.beforeLoadingCallback) {
      this.mediaDownloadService.beforeLoadingCallback();
    }

    this.staticFilesUrl = `${environment.staticFilesUrl}`;

    this.initializeToolbar();
  }

  ngOnDestroy() {
    this.toolbarService.restoreConfig();
    this.toolbarService.onBtnAddClick = null;

    if (this.mediaDownloadService.closeHandler) {
      this.mediaDownloadService.closeHandler(this.mediaDownloadService.documents?.length);
    }
  }

  onClose() {
    this.mediaDownloadService.folderOpen = false;
  }

  onGetDocuments(page: number) {
    // Reset the configuration for isSelected and allSelected fields
    this.toolbarService.isSelected = false;
    this.toolbarService.allSelected = false;
    this.mediaDownloadService.getDocuments(page)
  }

  onDeleteDocument() {
    this.mediaDownloadService.deleteDocuments();
    this.toolbarService.unavailableDelete = true;
    this.toolbarService.allSelected = this.toolbarService.isSelected = false;
  }

  onAllSelected() {
    this.toolbarService.isSelected = this.toolbarService.allSelected;
    this.mediaDownloadService.documents.map(c => {
      c.Selected = this.toolbarService.allSelected;
    });
  }

   onItemSelected() {
    this.toolbarService.isSelected = this.mediaDownloadService.documents.filter(e => e.Selected).length > 0;
    this.toolbarService.allSelected = this.mediaDownloadService.documents.filter(e => e.Selected).length == this.mediaDownloadService.documents.length;
  }

  onUploadFile(fileInput: any) {

    let fileData = <File>fileInput.target.files[0];
    if (fileData.size > 50000000) {
      this.dialogService.ShowWarning("app.error.filesizenotallowed");
      return;
    }

    const formData = new FormData();
    formData.append(fileData.type, fileData, this.withoutAccent(fileData.name));
    if (!formData) {
      return of(null);
    }

    this.mediaUploadService.uploadFile(formData)
      .subscribe((response: IResponse<IMediaInfo>) => {

        if (response.IsSuccessful) {
          let media = new Media(response.Data);
          media.FileOriginalName = fileData.name;

          this.mediaDownloadService.addDocument(media);
        }
      });
  }

  mediaIconSetter(documentName: string): string {
    let availableIcons = ['7z', 'avi', 'csv', 'dll', 'docx', 'doc', 'exe', 'html', 'jpeg', 'jpg', 'png', 'mp4', 'pdf', 'pptx', 'ppt', 'rtf', 'txt', 'xlsx', 'xls', 'zip'];
    let documentExtensionindex: number = documentName?.lastIndexOf(".");
    let defaultIcon: string = "file";
    let documentExtension: string = documentName?.substring(documentExtensionindex + 1)?.toLowerCase();
    return availableIcons.includes(documentExtension) ? `./assets/media-icons/${documentExtension}.ico` : `./assets/media-icons/${defaultIcon}.ico`;
  }

  private initializeToolbar() {
    this.toolbarService.allSelected = false;
    this.toolbarService.isSelected = false;
    this.toolbarService.configure(null, <IToolbarOperationHandler>{
      onItemSelectedHandler: () => this.onItemSelected(),
      selectAllHandler: () => this.onAllSelected(),
      deleteAllHandler: () => this.onDeleteDocument()
    }, true);

    this.toolbarService.unavailableExport = true;
    this.toolbarService.unavailableDelete = false;
  }

  private withoutAccent(str: string) {
    let accent = [
      /[\300-\306]/g, /[\340-\346]/g, // A, a
      /[\310-\313]/g, /[\350-\353]/g, // E, e
      /[\314-\317]/g, /[\354-\357]/g, // I, i
      /[\322-\330]/g, /[\362-\370]/g, // O, o
      /[\331-\334]/g, /[\371-\374]/g, // U, u
      /[\321]/g, /[\361]/g, // N, n
      /[\307]/g, /[\347]/g, // C, c
    ];
    let noaccent = ['A', 'a', 'E', 'e', 'I', 'i', 'O', 'o', 'U', 'u', 'N', 'n', 'C', 'c'];
    for (var i = 0; i < accent.length; i++) {
      str = str.replace(accent[i], noaccent[i]);
    }
    return str;
  }
}
