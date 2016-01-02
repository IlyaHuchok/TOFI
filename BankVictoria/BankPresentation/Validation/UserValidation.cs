using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace BankPresentation.Validation
{
    public static class UserValidation
    {
        public const int UserNameMaxLength = 100;
        public const int PasswordMaxLength = 200;

        public const int PasswordMinLength = 5;
        // Aasdfasdf.asBFsf-sadfa.adf-AdsS
        private static Regex UserNameRegex = new Regex(@"^[a-zA-Z]+([\._-][a-zA-Z]+)*$");
        // must have at least one letter
        private static Regex PasswordRegex = new Regex(@"[a-zA-Z]+");

        public static ValidationResult Validate(string userName, string password, string passwordRepeat)
        {
            var result = new ValidationResult
            {
                IsValid = true
            };

            if (!UserNameRegex.IsMatch(userName))
            {
                result.Error = "Username can consist only of letters, words in username can be separated by single uderscores(_), dashes(-) or dots(.)!\n"+
                    "Username must begin and end with letter!"
                    +"Example: 'I_m.newly-created_User'";
                result.IsValid = false;
            }

            if (password.Length < PasswordMinLength)
            {
                result.Error = "Password must be at least 5 symbols long!";
                result.IsValid = false;
            }

            if (password != passwordRepeat)
            {
                result.Error = "Password and re-enterd password do not match!";
                result.IsValid = false;
            }

            if (!PasswordRegex.IsMatch(password))
            {
                result.Error = "Password must contain letters!";
                result.IsValid = false;
            }

            return result;
        }
    }
}
