import { LvfCountries } from "../../enums/lvf-countries";
import { EnumHelper } from "../../_helpers/enum.helper";

/***
 * Represents the address object info.
 */
export interface IAddressInfo {

	Id: number;
	StreetNumber: string;
	StreetName: string;
	ZipCode: string;
	City: string;
	CountryCode: string;
	FurtherInformation: string;
}

/**
 * @class Address
 * @classdesc Represents an address object.
 */
export class Address {

	Id: number;
	StreetNumber: string;
	StreetName: string;
	ZipCode: string;
	City: string;
	CountryCode: string;
	FurtherInformation: string;

	// Client side fields
	Country: string;

	/**
	 * @constructor
	 * Creates an instance of address.
	 */
	constructor(info: IAddressInfo) {
		this.Id = info.Id;
		this.StreetNumber = info.StreetNumber;
		this.StreetName = info.StreetName;
		this.ZipCode = info.ZipCode;
		this.City = info.City;
		this.CountryCode = info.CountryCode;
		this.FurtherInformation = info.FurtherInformation;
	}

	static createInstance(info: IAddressInfo): Address {
		if (!info) {
			return new Address(<IAddressInfo>{});
		}

		return new Address(<IAddressInfo>{
			Id: info.Id,
			StreetNumber: info.StreetNumber,
			StreetName: info.StreetName,
			ZipCode: info.ZipCode,
			City: info.City,
			CountryCode: info.CountryCode,
			FurtherInformation : info.FurtherInformation,
		});
	}

	getCountry(): string {
		let lvfCountries = EnumHelper.getEnumValues(
			`app.country.`,
			LvfCountries,
			null,
			"string"
		);

		return lvfCountries.find((c) => c.value.toString() == this.CountryCode)
			? lvfCountries.find((c) => c.value.toString() == this.CountryCode).label
			: "";
	}

	 toString(errorneousmessage: string = "") {

		let output = this.StreetNumber?.trim()? `${this.StreetNumber}, ` : "";
		output += this.StreetName ? `${this.StreetName}, ` : "";
		output += this.FurtherInformation?.trim()? `${this.FurtherInformation}, ` : "";
		output += this.ZipCode ? `${this.ZipCode} ` : "";
		output += this.City ? `${this.City} ` : "";
		//output += this.CountryCode ? `${this.getCountry()} ` : "";

		if (output.trim() == "") {
			return errorneousmessage;
		}

		return output;
	}

	toArray() {
		this.Country = this.getCountry();
		let firstLine = this.StreetNumber ? `${this.StreetNumber}, ` : "";
		firstLine += this.StreetName ? `${this.StreetName}` : "";
		let secondLine = this.ZipCode ? `${this.ZipCode} ` : "";
		secondLine += this.City ? `${this.City} ` : "";
		let thirdLine = this.Country ? `${this.Country}` : "";
		let output = [firstLine, secondLine, thirdLine];
		return output;
	}

	 IsComplete(): boolean {
		let output = [
			this.City == "" || this.City == null,
			this.CountryCode == "" || this.CountryCode == null,
			this.StreetName == "" || this.StreetName == null,
			this.StreetNumber == "" || this.StreetNumber == null,
			this.ZipCode == "" || this.ZipCode == null,
		];

		if (output.filter((o) => o == true).length == output.length ||this.StreetNumber == null ||
			output.filter((o) => o == false).length == output.length
		) {
			return true;
		} else if (
			(this.StreetNumber == "" || this.StreetNumber == null) &&
			output.filter((o) => o == false).length == output.length - 1
		) {
			return true;
		} else {
			return false;
		}
	}

	public Isprovided(): boolean {
		if (
			this.City == "" ||
			this.City == null ||
			this.CountryCode == "" ||
			this.CountryCode == null ||
			this.StreetName == "" ||
			this.StreetName == null ||
			this.ZipCode == "" ||
			this.ZipCode == null
		) {
			return false;
		} else {
			return true;
		}
	}
}
