using System;

namespace Shared.Core.Business.Helpers
{
    public class Utility
    {
        /// <summary>
        /// A function to generate passwords.
        /// </summary>

        public static string GeneratePassword()
        {
            string uppercasechars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string lowercasechars = "abcdefghijklmnopqrstuvwxyz";
            string numberchars = "0123456789";
            string specialcharacterchars = "@$!%#?&+.()_:";

            char[] stringChars = new char[12];
            Random random = new Random();

            stringChars[0] = uppercasechars[random.Next(uppercasechars.Length)];
            stringChars[1] = numberchars[random.Next(numberchars.Length)];
            stringChars[2] = lowercasechars[random.Next(lowercasechars.Length)];
            stringChars[3] = specialcharacterchars[random.Next(specialcharacterchars.Length)];
            stringChars[4] = numberchars[random.Next(numberchars.Length)];
            stringChars[5] = uppercasechars[random.Next(uppercasechars.Length)];
            stringChars[6] = specialcharacterchars[random.Next(specialcharacterchars.Length)];
            stringChars[7] = lowercasechars[random.Next(lowercasechars.Length)];
            stringChars[8] = numberchars[random.Next(numberchars.Length)];
            stringChars[9] = specialcharacterchars[random.Next(specialcharacterchars.Length)];
            stringChars[10] = lowercasechars[random.Next(lowercasechars.Length)];
            stringChars[11] = uppercasechars[random.Next(uppercasechars.Length)];

            return new String(stringChars);
        }
    }
}
