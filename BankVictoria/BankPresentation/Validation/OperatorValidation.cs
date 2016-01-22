using System;
using System.Collections.Generic;
using System.Globalization;
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
                result.Error += "Wrong passport number!\n Enter your passport number in format: AB1122333!" + Environment.NewLine;
                result.IsValid = false;
            }
            if (PassportNo.Length > PassportNoMaxLength)
            {
                result.Error += "PassportNo must not be longer than "+ PassportNoMaxLength + " symbols!" + Environment.NewLine;
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
                result.Error += "Wrong ToPay format! Can have only numbers" + Environment.NewLine;
                result.IsValid = false;
            }
            else if (Regex.IsMatch(ToPay, @"\.\d\d\d"))
            {
                result.Error += "Cannot have more than 2 value digits after '.'" + Environment.NewLine;
                result.IsValid = false;
            }
            else if (Convert.ToDecimal(ToPay) < 0)
            {
                result.Error += "Wrong ToPay format! Can have only non-negative numbers" + Environment.NewLine;
                result.IsValid = false;
            }
            else if (Convert.ToDecimal(ToPay) == 0)
            {
                result.Error += "ToPay can't be zero" + Environment.NewLine;
                result.IsValid = false;
            }
            if (ToPay.Length > ToPayMaxLength)
            {
                result.Error += "ToPay must not be longer than "+ ToPayMaxLength + " symbols!" + Environment.NewLine;
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
                result.Error += "Wrong Request id format! Can have only numbers" + Environment.NewLine;
                result.IsValid = false;
            }
            else if (Convert.ToDecimal(RequestId) < 0)
            {
                result.Error += "Wrong RequestId format! Can have only non-negative numbers" + Environment.NewLine;
                result.IsValid = false;
            }
            if (RequestId.Length > RequestIdMaxLength)
            {
                result.Error += "RequestId must not be longer than "+ RequestIdMaxLength + " symbols!" + Environment.NewLine;
                result.IsValid = false;
            }
            return result;
        }
    }
}
