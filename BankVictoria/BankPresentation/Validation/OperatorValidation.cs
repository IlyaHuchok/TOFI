using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
namespace BankPresentation.Validation
{
    public class OperatorValidation
    {
        private static Regex NumericField = new Regex(@"[0-9]");
        public const int PassportNoMaxLength = 9;
        public const int ToPayMaxLength = 12;
        public static ValidationResult Validate(string RequestId, string PasswordNo, string ToPay)
        {
            var result = new ValidationResult
            {
                IsValid = true
            };

            var error = new StringBuilder();

            if (!NumericField.IsMatch(RequestId))
            {
                error.AppendLine("Wrong Request id format! Can have only numbers");
                result.IsValid = false;
            }

            if (!NumericField.IsMatch(PasswordNo))
            {
                error.AppendLine("Wrong PasswordNo format! Can have only numbers");
                result.IsValid = false;
            }

            if (!NumericField.IsMatch(ToPay))
            {
                error.AppendLine("Wrong ToPay format! Can have only numbers");
                result.IsValid = false;
            }

            if (PasswordNo.Length > PassportNoMaxLength)
            {
                error.AppendLine("PasswordNo must not be longer than 9 symbols!");
                result.IsValid = false;
            }

            if (ToPay.Length > ToPayMaxLength)
            {
                error.AppendLine("ToPay must not be longer than 12 symbols!");
                result.IsValid = false;
            }
            return result;
        }
    }
}
