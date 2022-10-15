using Shared.Core.Model.Common;
using Shared.Core.Model.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Shared.Core.Business.Helpers
{
    /// <summary>
    /// Helper that applies the management rules.
    /// </summary>
    public static class ManagementRuleHelper
    {
        public static void CheckRequestParameters(EntityBase entityBase, int id = 0, string errorneousMessage = "app.error.invalidparameter")
        {
            if (entityBase == null || id != entityBase.Id)
            {
                throw new ArgumentException(errorneousMessage);
            }
        }

        public static void CheckEmailFormat(string email, bool isRequired = true, string errorneousMessage = "app.error.invalidemail")
        {
            if (!isRequired && string.IsNullOrWhiteSpace(email)) return;

            Regex emailregex = new Regex(@"[^@ \t\r\n]+@[^@ \t\r\n]+\.[^@ \t\r\n]+");
            if (!emailregex.IsMatch(email))
            {
                throw new ArgumentException(errorneousMessage);
            }
        }

        public static void CheckPhoneOrFaxNumberFormat(string phoneOrFaxNumber, bool isRequired = true, string errorneousMessage = "app.error.invalidphonenumber")
        {
            if (!isRequired && string.IsNullOrWhiteSpace(phoneOrFaxNumber)) return;

            Regex phoneregex = new Regex(@"^(0\d|\+[\d]{2}[1-9]|0\s\d|\+[\d]{2}\s[1-9]|0\d\s|\+[\d]{2}[1-9]\s|0\s\d\s|\+[\d]{2}\s[1-9]\s|00[\d]{2}\d|00[\d]{2}\d\s|00[\d]{2}\s\d|00[\d]{2}\s\d\s)((\d{2})|(\d{2}\s)|(\s\d{2})|(\s\d{2}\s)){4}$");
            if (!phoneregex.IsMatch(phoneOrFaxNumber))
            {
                throw new ArgumentException(errorneousMessage);
            }
        }

        public static void CheckPictureNameFormat(string pictureName, string errorneousMessage = "app.error.invalidpicture")
        {
            Regex PictureIdregex = new Regex(@"\b[\w@$!%*#?&+.()_:ù^€£,;'`çéè|µ=}{<>-]{1,}\.(jpg|jpeg|png|gif|jfif|JPG|JPEG|PNG|GIF|JFIF)$");

            if (!PictureIdregex.IsMatch(pictureName))
            {
                throw new ArgumentException(errorneousMessage);
            }
        }

        public static void CheckStringFormat(string text, bool isRequired = true, int? minLength = 0, int? maxLength = 100, string errorneousMsg = "app.error.invalidstring")
        {
            if (!isRequired && string.IsNullOrWhiteSpace(text)) return;

            if (isRequired || minLength.HasValue || maxLength.HasValue)
            {
                if (string.IsNullOrWhiteSpace(text))
                {
                    throw new ArgumentException(errorneousMsg);
                }
            }

            if (minLength.HasValue && text.Length < minLength)
            {
                throw new ArgumentException(errorneousMsg);
            }

            if (maxLength.HasValue && text.Length > maxLength)
            {
                throw new ArgumentException(errorneousMsg);
            }
        }

        /// <summary>
        /// Checks the given address.
        /// </summary>
        /// <param name="address">Address to check</param>
        /// <param name="shouldBeCompletedIfFilled">Ensures that the address is correctly filled if asked</param>
        public static void CheckAddressFormat(Address address, bool isRequired, bool shouldBeCompletedIfFilled = false, string errorneousMessage = "app.error.invalidaddress")
        {
            if (!isRequired && address == null) return;

            if (isRequired && address == null) throw new ArgumentException(errorneousMessage);

            if (shouldBeCompletedIfFilled && address != null)
            {
                List<bool> addressFields = new List<bool>{
                    string.IsNullOrWhiteSpace(address.StreetName),
                    string.IsNullOrWhiteSpace(address.ZipCode),
                    string.IsNullOrWhiteSpace(address.City),
                    string.IsNullOrWhiteSpace(address.CountryCode)
                };

                if (addressFields.Distinct().Count() != 1)
                {
                    throw new ArgumentException(errorneousMessage);
                }
            }
        }

        /// <summary>
        /// Checks whether or not the given text is a number.
        /// </summary>
        /// <param name="text">Input to test</param>
        /// <param name="errorneousMsg"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static bool CheckNumber(string text, string errorneousMsg = "")
        {
            int.TryParse(text, out int number);

            if (number == 0)
            {
                throw new ArgumentException(errorneousMsg);
            }

            return true;
        }

        public static void CheckPasswordFormat(string password, string errorneousMessage = "app.error.invalidpassword")
        {
            Regex passwordregex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*#?&+.()_:ù^€£|çéèà|µ=}{/*~§$£¤°;])[\w@$!%*#?&+.()_:ù^€£|çéèà|µ=}{/*~§$£¤°;]{8,25}$");
            if (string.IsNullOrWhiteSpace(password) || !passwordregex.IsMatch(password))
            {
                throw new ArgumentException(errorneousMessage);
            }
        }

        public static void CheckThresholdFormat(int threshold, int? minValue = 1, int? maxValue = 100, string errorneousMessage = "app.error.invalidthreshold")
        {
            if (minValue.HasValue)
            {
                if (threshold < minValue)
                {
                    throw new ArgumentException(errorneousMessage);
                }
            }

            if (maxValue.HasValue)
            {
                if (threshold > maxValue)
                {
                    throw new ArgumentException(errorneousMessage);
                }
            }
        }

        public static void CheckAmount(double amount, double min, double max, string messageKey = "app.error.invalidamount")
        {
            if (amount < min || amount > max)
            {
                throw new ArgumentException(messageKey);
            }
        }

        public static void CheckSiretFormat(string siret, bool isRequired = true, string errorneousMessage = "app.error.invalidsiret")
        {
            if (!isRequired && string.IsNullOrWhiteSpace(siret)) return;

            Regex siretregex = new Regex(@"\b(\d){14}\b");
            if (!siretregex.IsMatch(siret))
            {
                throw new ArgumentException(errorneousMessage);
            }
        }

        public static void CheckVatValueFormat(double VATValue, double minValue = 0, double maxValue = 20, string errorneousMessage = "app.error.invalidvatvalue")
        {
            if (VATValue < minValue || VATValue > maxValue)
            {
                throw new ArgumentException(errorneousMessage);
            }
        }

        public static void CheckVATNumberFormat(string vatNumber, bool isRequired = true, string errorneousMessage = "app.error.invalidvatNumber")
        {
            if (!isRequired && string.IsNullOrWhiteSpace(vatNumber)) return;

            Regex vatNumberregex = new Regex(@"^(([A-Z]){2}[A-Z0-9]{2}[\d]{3,7}[(A-Z0-9|WA|FA|BO2|B\d)]{3}|(CHE)([\d]{3}\.){2}([\d]{3})|[\d]{9}(MVA))$");
            if (!vatNumberregex.IsMatch(vatNumber))
            {
                throw new ArgumentException(errorneousMessage);
            }
        }

        public static void CheckStartDateValidity(DateTime? startDate, DateTime? endDate = null, string errorneousMessage = "app.error.enddatepriortostartdate")
        {
            if (!startDate.HasValue)
            {
                throw new ArgumentException(errorneousMessage);
            }

            if (endDate.HasValue && startDate.Value > endDate.Value)
            {
                throw new ArgumentException(errorneousMessage);
            }
        }

        public static void CheckDateValidity(DateTime? date, string errorneousMessage = "app.error.invaliddate")
        {
            if (!date.HasValue || date == DateTime.MinValue || date == DateTime.MaxValue)
            {
                throw new ArgumentException(errorneousMessage);
            }
        }

        /// <summary>
        /// Checks whether the date string is valid (the expected string format is dd/mm/yyyy).
        /// </summary>
        /// <param name="dateString">Date string to check</param>
        /// <param name="errorneousMsg">Errorneous message</param>
        /// <returns>Parsed date</returns>
        public static DateTime CheckDateStringFormat(string dateString, string errorneousMsg = "app.error.invaliddate")
        {
            DateTime? dateTime = Parse(dateString, '/');
            if (dateTime == null)
            {
                throw new ArgumentException(errorneousMsg);
            }

            return dateTime.Value;
        }

        private static DateTime? Parse(string inputDate, char separator)
        {
            if (string.IsNullOrWhiteSpace(inputDate))
            {
                return null;
            }

            string[] splitted = inputDate.Split(new char[] { separator });

            return new DateTime(Convert.ToInt32(splitted[2]), Convert.ToInt32(splitted[1]), Convert.ToInt32(splitted[0]));
        }
    }
}