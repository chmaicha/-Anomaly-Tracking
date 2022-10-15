import { Component, OnInit, Input } from "@angular/core";
import { TranslateService } from "@ngx-translate/core";
import { LvfCountries } from "../../enums/lvf-countries";
import { Address } from "../../models/common/address";
import { IEnumEntry, EnumHelper } from "../../_helpers/enum.helper";

@Component({
	selector: "app-address-detail",
	templateUrl: "./address-detail.component.html",
	styleUrls: ["./address-detail.component.scss"],
})
export class AddressDetailComponent implements OnInit {
	@Input() address: Address;
	@Input() title: string;
	@Input() required?: boolean;

	lvfCountries: IEnumEntry<LvfCountries>[];

	constructor(private translateService: TranslateService) { }

	ngOnInit() {
		this.lvfCountries = EnumHelper.getEnumValues(
			`app.country.`,
			LvfCountries,
			null,
			"string"
		);
		this.lvfCountries = this.lvfCountries.sort((a, b) =>
			this.translateService
				.instant(a.label)
				.localeCompare(this.translateService.instant(b.label))
		);
	}

}
