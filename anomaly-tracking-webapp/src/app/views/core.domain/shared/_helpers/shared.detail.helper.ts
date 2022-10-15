export class DetailHelper {

    public static emailValidityChecker(email: string): string {

        let emailContentIndex: number = email?.indexOf("@");
        let emailDomainIndex: number = email?.lastIndexOf(".");

        let emailPseudo = email?.substring(0, emailContentIndex);
        let emailDomain = email?.substring(emailContentIndex + 1, emailDomainIndex);
        let emaiExtention = email?.substring(emailDomainIndex + 1);

        let isExtensionInvalid = /[^a-z]/;
        let isEmailInvalid = /[^a-zA-Z0-9.-]/;
        let isDomainValid = /[^a-zA-Z0-9-]/;

        let emailValidityError = "";

        if (email && email.trim() != "") {

            if (isEmailInvalid?.test(emailPseudo) || !emailPseudo) {
                emailValidityError = "app.shared.emailfieldrequired";
            }

            else if (isDomainValid?.test(emailDomain) || !emailDomain) {
                emailValidityError = "app.shared.emaildomaininvalid";
            }

            else if (emailPseudo && emailDomain && emaiExtention?.length > 5) {
                emailValidityError = "app.shared.emailextensionlengthinvalid";
            }

            else if (emailPseudo && emailDomain && (isExtensionInvalid?.test(emaiExtention) || !emaiExtention)) {
                emailValidityError = "app.shared.emailextensioninputinvalid";
            }
        }

        return emailValidityError;
    }

}
