using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankPresentation.Validation
{
    class RequestValidation
    {
        public const int SalaryMaxLength = 12;
        public const int AmountMaxLength = 12;

        public static ValidationResult Validate(string Amount, string Salary)
        {
            var result = new ValidationResult
            {
                IsValid = true
            };

            var error = new StringBuilder();
            decimal amount,salary;
            if (!Decimal.TryParse(Amount, out amount))
            {
                result.Error += "Wrong Amount format! Can have only numbers" + Environment.NewLine;
                result.IsValid = false;
            }
            else if (Convert.ToDecimal(Amount) < 0)
            {
                result.Error += "Wrong Amount format! Can have only non-negative numbers" + Environment.NewLine;
                result.IsValid = false;
            }
            else if (Convert.ToDecimal(Amount) == 0)
            {
                result.Error += "Amount can't be zero" + Environment.NewLine;
                result.IsValid = false;
            }
            if (Amount.Length > AmountMaxLength)
            {
                result.Error += "Amount must not be longer than 12 symbols!" + Environment.NewLine;
                result.IsValid = false;
            }


            if (!Decimal.TryParse(Salary, out salary))
            {
                result.Error += "Wrong Salary format! Can have only numbers" + Environment.NewLine;
                result.IsValid = false;
            }
            else if (Convert.ToDecimal(Salary) < 0)
            {
                result.Error += "Wrong Salary format! Can have only non-negative numbers" + Environment.NewLine;
                result.IsValid = false;
            }
            else if (Convert.ToDecimal(Salary) == 0)
            {
                result.Error += "Salary can't be zero" + Environment.NewLine;
                result.IsValid = false;
            }
            if (Salary.Length > SalaryMaxLength)
            {
                result.Error += "Salary must not be longer than 12 symbols!" + Environment.NewLine;
                result.IsValid = false;
            }
            return result;
        }
    }
}
