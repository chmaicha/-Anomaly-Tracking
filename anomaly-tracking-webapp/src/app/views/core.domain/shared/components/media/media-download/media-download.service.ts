import { Injectable, Output, EventEmitter } from '@angular/core';
import { Media } from '../../../models/common/media';

export interface IMediaDownloaderHandler {

  addDocument: (media: Media) => void;
  deleteDocument: (mediaId: number) => void;
  deleteDocuments: (indices: number[]) => void;
  getDocuments: (page: number) => void;
}

@Injectable({
  providedIn: 'root'
})
export class MediaDownloadService {

  @Output() pageNumber: EventEmitter<number> = new EventEmitter();

  entityId: number;
  documents: Media[];
  folderOpen: boolean;

  page: number;
  totalCount: number;
  pageSize: number = 12;
  indices: number[];
 
  // Callback to proceed before loading documents.
  beforeLoadingCallback: () => void;

  // Callback to proceed when closing the media document component.
  closeHandler: (mediaCount : number) => void;

  private handler: IMediaDownloaderHandler;

  constructor() { }

  addDocument(media: Media) {
    this.handler.addDocument(media);
  }

  deleteDocument(mediaId: number) {
    this.handler.deleteDocument(mediaId);
  }

  getDocuments(page: number) {
    this.handler.getDocuments(page);
  }

  deleteDocuments() {
    this.handler.deleteDocuments(this.indices);
  }

  clear() {
    this.page = 1;
    this.documents = [];
  }

  registerHandler(handler: IMediaDownloaderHandler) {
    this.handler = handler;
  }
}
