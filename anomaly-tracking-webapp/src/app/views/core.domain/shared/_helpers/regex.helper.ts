
export class RegexHelper {
    public static readonly emailregex = [/^[a-zA-Z0-9.-_]+@[a-zA-Z0-9-_]+\.[a-z]{2,5}$/];
    public static readonly siretregex = [/\b(\d){14}\b/];
    public static readonly vatNumberregex = [/^(([A-Z]){2}[A-Z0-9]{2}[\d]{3,7}[(A-Z0-9|WA|FA|BO2|B\d)]{3}|(CHE)([\d]{3}\.){2}([\d]{3})|[\d]{9}(MVA))$/];
    public static readonly phoneregex = [/^(0\d|\+[\d]{2}[1-9]|0\s\d|\+[\d]{2}\s[1-9]|0\d\s|\+[\d]{2}[1-9]\s|0\s\d\s|\+[\d]{2}\s[1-9]\s|00[\d]{2}\d|00[\d]{2}\d\s|00[\d]{2}\s\d|00[\d]{2}\s\d\s)((\d{2})|(\d{2}\s)|(\s\d{2})|(\s\d{2}\s)){4}$/];
    public static readonly passwordregex = [/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*#?&+.()_:ù^€£,;'`çéè|µ=}{<>-])[\w@$!%*#?&+.()_:ù^€£,;'`çéè|µ=}{<>-]{8,25}$/];
    public static readonly pictureregex = [/\b[\w@$!%*#?&+.()_:ù^€£,;'`çéè|µ=}{<>-]{1,}\.(jpg|jpeg|png|gif)\b/];

    public static readonly siretcheck = [/\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/];
    public static readonly tvacheck = [/[A-Z]/, /[A-Z]/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/];
    public static readonly phonecheck = [/[+]|0/, /\d/, /\d/, " ", /\d/, " ", /\d/, /\d/, " ", /\d/, /\d/, " ", /\d/, /\d/, " ", /\d/, /\d/];
    public static readonly shortphonecheck = [/0|[+]/, /\d/, " ", /\d/, /\d/, " ", /\d/, /\d/, " ", /\d/, /\d/, " ", /\d/, /\d/];
    public static readonly datecheck = [/[0-3]/, /\d/,"/", /[0-1]/, /\d/, "/", /[1-2]/, /\d/, /\d/, /\d/];
}