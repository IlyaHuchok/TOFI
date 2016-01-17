using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BankPresentation.Validation
{
    public static class ClientValidation
    {
        public const int LastNameMaxLength = 100;
        public const int NameMaxLength = 100;
        public const int PatronymicMaxLength = 100;
        public static DateTime MinBirthDate = new DateTime(1910, 1, 1);
        public static DateTime MaxBirthDate = DateTime.Now.AddYears(-18);
        public static DateTime MinPassportExpirationDate = DateTime.Now;
        public static DateTime MaxPassportExpirationDate = DateTime.Now.AddYears(100);
        public const int PassportIdentityNoMaxLength = 14;
        public const int PassportNoMaxLength = 9;
        public const int AddressMaxLength = 200;
        public const int PasswordAuthorityMaxLength = 200;

        // symbol '+' (zero or one time)f
        // 1-3 digits
        // symbol '-' (zero or one time)
        // 9-10 digits
        private static Regex MobileRegex = new Regex(@"^\+?[0-9]{1,3}-?[0-9]{9,10}$");
        public static int MobileMaxLength = 15;

        private static Regex EmailRegex =  new Regex(@"[a-zA-Z0-9!#$%&'*+\=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+\=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?");

        //14 chars from the list (BLR only)
        private static Regex PassportIdentityNoRegex = new Regex(@"^[3-6][0-9]{6}[ABCKEM][0-9]{3}(GB|PB|BA|BI)[0-9]$"); ///may be better!!!
        //(REGION_CODE)1234567
        private static Regex PassportNoRegex =  new Regex(@"^(AB|BM|HB|KH|MP|MC|KB|PP)[0-9]{7}$");
        //Ababa von Assd Mmmmm-n-Qwerty der aaaa-Bbbbb
        private static Regex NameRegex =  new Regex(@"^[A-Z]+[a-z]*((\s|-)[A-Z]?[a-z]*)*$");

        public static ValidationResult Validate(string lastName, string name, /*string patronymic,*/ DateTime birthday, string mobile, string email, string passportNo,
            DateTime passwordExpiration, string passportIdentityNo, string passportAuthority, string placeOfResidence, string registrationAddress)
        {
            var result = new ValidationResult
            {
                IsValid = true
            };

            var error = new StringBuilder();

            if (!NameRegex.IsMatch(lastName))
            {
                error.AppendLine("Wrong lastname format! Can have only words separated by single spaces or dashes. Lastname must start with capital letter");
                result.IsValid = false;
            }

            if (!NameRegex.IsMatch(name))
            {
                error.AppendLine("Wrong name format!\n Can have only words separated by single spaces or dashes. Name must start with capital letter");
                result.IsValid = false;
            }

            //if (!NameRegex.IsMatch(patronymic))
            //{
            //    error.AppendLine("Wrong patronymic format!\n Can have only words separated by single spaces or dashes. Patronymic must start with capital letter");
            //    result.IsValid = false;
            //}

            if (!MobileRegex.IsMatch(mobile))
            {
                error.AppendLine("Wrong mobile phone format!\n Enter your phone number in the following format: +1-1234567890 3754412345678 23-1234567890");
                result.IsValid = false;
            }
            
            if (!EmailRegex.IsMatch(email))
            {
                error.AppendLine("Wrong email!\n Enter your email address in format: yourAccountName@yoursite.xyz");
                result.IsValid = false;
            }

            if (!PassportNoRegex.IsMatch(passportNo))
            {
                error.AppendLine("Wrong passport number!\n Enter your passport number in format: AB1122333!");
                result.IsValid = false;
            }

            if (!PassportIdentityNoRegex.IsMatch(passportIdentityNo))
            {
                error.AppendLine("Wrong passport identity number format!\n");
                result.IsValid = false;
            }

            result.Error = error.ToString();
            return result;
        }
    }
}
