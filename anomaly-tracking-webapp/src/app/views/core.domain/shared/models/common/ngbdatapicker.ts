/**
 * Ngbdate picker uses to map data from server to template's datepicker.
 */
export class NGBDatePicker {

    year: number;
    month: number;
    day: number;

    /**
     * @constructor
     * Creates a new instance of NGBDatePicker.
     */
    constructor(datestring: string) {

        if (datestring && datestring.trim() != "" && datestring.indexOf("/") > 0) {
            let splitted = datestring.split('/');

            this.day = +splitted[0];
            this.month = +splitted[1];
            this.year = +splitted[2];
        }
    }

    static instance(year: number, month: number, day: number): NGBDatePicker {
        return { year: year, month: month, day: day };
    }

    static convert(date: Date) {
        return { year: date.getFullYear(), month: date.getMonth() + 1, day: date.getDate() };
    }
}