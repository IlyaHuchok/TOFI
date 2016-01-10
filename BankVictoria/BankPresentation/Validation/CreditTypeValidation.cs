using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankPresentation.Validation
{
    public static class CreditTypeValidation
    {
        public static int NameMaxLength = 100;
        public static int MaxTimeMonth = 1200;

        public static ValidationResult Validate(
            string name,
            string intTimeMonth,
            string decPercentPerYear,
            string currency,
            string decFinePercent,
            string decMinAmount,
            string decMaxAmount)
        {
            var result = new ValidationResult() { IsValid = true };

            if (string.IsNullOrEmpty(name))
            {
                result.IsValid = false;
                result.Error = "Name must be not empty!\n";
            }
            else
            {
                if (name.Length > NameMaxLength)
                {
                    result.IsValid = false;
                    result.Error = string.Format("Name cannot be more than {0} symbols long!\n", NameMaxLength);
                }
            }

            int timeMonth;
            if (int.TryParse(intTimeMonth, out timeMonth))
            {
                if (timeMonth <= 0)
                {
                    result.IsValid = false;
                    result.Error += "Time must be positive number!\n";
                }
                if (timeMonth >= MaxTimeMonth)
                {
                    result.IsValid = false;
                    result.Error += string.Format("Time cannot be more than {0}!\n", MaxTimeMonth);
                }
            }
            else
            {
                result.Error += "Time must be valid positive integer number!\n";
            }

            decimal percentPerYear;
            if (decimal.TryParse(decPercentPerYear, out percentPerYear))
            {
                if (percentPerYear <= 0)
                {
                    result.IsValid = false;
                    result.Error += "Percent Per Year must be positive number!\n";
                }
            }
            else
            {
                result.IsValid = false;
                result.Error += "Percent Per Year must be valid positive integer number!\n";
            }

            decimal finePercentPerYear;
            if (decimal.TryParse(decFinePercent, out finePercentPerYear))
            {
                if (finePercentPerYear <= 0)
                {
                    result.IsValid = false;
                    result.Error += "Fine Percent Per Year must be positive number!\n";
                }
            }
            else
            {
                result.IsValid = false;
                result.Error += "Fine Percent Per Year must be valid positive integer number!\n";
            }

            decimal minAmount;
            if (decimal.TryParse(decMinAmount, out minAmount))
            {
                if (minAmount <= 0)
                {
                    result.IsValid = false;
                    result.Error += "Min Amount must be positive number!\n";
                }
            }
            else
            {
                result.IsValid = false;
                result.Error += "Min Amount must be valid positive integer number!\n";
            }

            decimal maxAmount;
            if (decimal.TryParse(decMaxAmount, out maxAmount))
            {
                if (maxAmount <= 0)
                {
                    result.IsValid = false;
                    result.Error += "Max Amount must be positive number!\n";
                }
                if (maxAmount < minAmount)
                {
                    result.IsValid = false;
                    result.Error += "Max Amount must be greater than Min AMount!\n";
                }
            }
            else
            {
                result.IsValid = false;
                result.Error += "Max Amount must be valid positive integer number!\n";
            }

            return result;
        }
    }
}
