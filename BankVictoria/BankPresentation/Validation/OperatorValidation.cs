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
        private static Regex PassportNoRegex = new Regex(@"^(AB|BM|HB|KH|MP|MC|KB|PP)[0-9]{7}$");
        public const int PassportNoMaxLength = 9;
        public const int ToPayMaxLength = 12;
        public const int RequestIdMaxLength = 10;

        public static ValidationResult ValidatePassportNo(string PassportNo)
        {
            var result = new ValidationResult
            {
                IsValid = true
            };

            var error = new StringBuilder();

            if (!PassportNoRegex.IsMatch(PassportNo))
            {
                error.AppendLine("Wrong passport number!\n Enter your passport number in format: AB1122333!");
                result.IsValid = false;
            }
            if (PassportNo.Length > PassportNoMaxLength)
            {
                error.AppendLine("PassportNo must not be longer than 9 symbols!");
                result.IsValid = false;
            }


            return result;
        }

        public static ValidationResult ValidateToPay(string ToPay)
        {

            var result = new ValidationResult
            {
                IsValid = true
            };
            decimal topay;
            var error = new StringBuilder();
            if (!Decimal.TryParse(ToPay, out topay))
            {
                error.AppendLine("Wrong ToPay format! Can have only numbers");
                result.IsValid = false;
            }
            else if (Convert.ToDecimal(ToPay) < 0)
            {
                error.AppendLine("Wrong ToPay format! Can have only non-negative numbers");
                result.IsValid = false;
            }
            if (ToPay.Length > ToPayMaxLength)
            {
                error.AppendLine("ToPay must not be longer than 12 symbols!");
                result.IsValid = false;
            }
            return result;
        }

        public static ValidationResult ValidateRequestId(string RequestId)
        {
            var result = new ValidationResult
            {
                IsValid = true
            };

            var error = new StringBuilder();
            int request;

            if (!Int32.TryParse(RequestId, out request))
            {
                error.AppendLine("Wrong Request id format! Can have only numbers");
                result.IsValid = false;
            }
            return result;
        }
    }
}
