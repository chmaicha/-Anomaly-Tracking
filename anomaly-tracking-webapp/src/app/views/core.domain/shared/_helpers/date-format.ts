import { NgbDateParserFormatter, NgbDateStruct } from "@ng-bootstrap/ng-bootstrap";

import { Injectable } from "@angular/core";
import * as moment from "moment";

@Injectable()
export class MomentDateFormatter extends NgbDateParserFormatter {

    readonly DELIMITER = '/';
    readonly DT_FORMAT = `DD${this.DELIMITER}MM${this.DELIMITER}YYYY`;

    parse(value: string): NgbDateStruct {
        if (value) {
            value = value.trim();
            let mdt = moment(value, this.DT_FORMAT)
            return {
                month: mdt.month(),
                day: mdt.day(),
                year : mdt.year(),
              };
        }
        return null;
    }

    format(date: NgbDateStruct): string {
        
        if (date) {
        
            let mdt = moment(date.day + this.DELIMITER + date.month + this.DELIMITER + date.year, this.DT_FORMAT);
            return mdt.isValid() ? mdt.format(this.DT_FORMAT) : '';
        }
        return '';
    }
}