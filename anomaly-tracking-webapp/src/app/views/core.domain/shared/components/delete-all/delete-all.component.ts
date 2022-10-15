import { Component, OnInit, Input, ViewChild, TemplateRef, Output, EventEmitter } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
@Component({
  selector: 'app-delete-all',
  templateUrl: './delete-all.component.html',
  styleUrls: ['./delete-all.component.scss']
})
export class DeleteAllComponent implements OnInit {

  @Input() question: string;
  @Input() isToolbar: boolean = false
  @Output() delete = new EventEmitter<void>();
  @ViewChild('deleteConfirmModal', { static: true }) editTaskModel: TemplateRef<any>;


  constructor(
    private modalService: NgbModal,
  ) { }

  ngOnInit() { }

  comfirm() {
    this.modalService.open(this.editTaskModel, { ariaLabelledBy: 'modal-basic-title', centered: true })
      .result.then((result) => {
        this.delete.emit();
      }, (reason) => {
      });
  }
}
