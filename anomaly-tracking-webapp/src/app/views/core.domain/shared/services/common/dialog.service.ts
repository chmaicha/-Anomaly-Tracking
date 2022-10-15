import { Injectable } from "@angular/core";
import { TranslateService } from "@ngx-translate/core";
import { ToastrService } from "ngx-toastr";
import { environment } from "src/environments/environment";
import { IResponse, IException } from "../../models/common/iexception";

@Injectable({
    providedIn: 'root'
})
export class DialogService {

    /**
     * @constructor
     * Create a signe instance of  DialogService.
     */
    constructor(private toastrService: ToastrService, private translateService: TranslateService) { }

    Show(response: IResponse<any>, displayMsg: boolean = false, isprogressBar: boolean = false, title: string = '') {
        if (!response) {
            return;
        }

        if (response.IsSuccessful) {
            if (!displayMsg) this.ShowSuccess(response.MessageKey, isprogressBar);
        } else {
            this.ShowError(response);
        }
    }

    ShowSuccess(message: string, isprogressBar: boolean = false, title: string = '') {
        if (!message) {
            return;
        }

        if (this.toastrService) {
            this.successBar(this.translateService.instant(message, title), isprogressBar);
        } else {
            alert(this.translateService.instant(message));
        }
    }

    ShowWarning(message: string, title: string = '') {
        if (this.toastrService) {
            this.errorBar(this.translateService.instant(message, title));
        } else {
            alert(this.translateService.instant(message));
        }
    }

    ShowError(error: IException) {
        if (!error) {
            return;
        }

        if (this.toastrService) {
            let errorneousEntity = error.ErrorneousEntity && error.ErrorneousEntity.trim() != '' ? `(${error.ErrorneousEntity ?? ''})` : '';

            let message = this.translateService ? `${errorneousEntity} ${this.translateService.instant(error.MessageKey)}` : '';
            if (!environment.production) {
                message = this.translateService ? `${this.translateService.instant(error.MessageKey)} ${errorneousEntity}` : '';
                if (error.InnerException && error.InnerException) {
                    message += `(InnerException)-> ${error.InnerException ?? ''}`;
                }
            }

            this.errorBar(this.translateService.instant(message), '');
        } else {
            alert(this.translateService.instant(error.MessageKey));
        }
    }

    private successBar(message: string, isprogressBar: boolean = false, title: string = '') {
        this.toastrService.success(message, title, { timeOut: 3000, closeButton: true, progressBar: isprogressBar });
    }

    private warningBar(message: string, title?: string) {
        this.toastrService.warning(message, title, { timeOut: 3000, closeButton: true, progressBar: false });
    }

    private errorBar(message: string, title: string = '') {
        this.toastrService.error(message, title, { timeOut: 3000, closeButton: true, progressBar: false });
    }
}