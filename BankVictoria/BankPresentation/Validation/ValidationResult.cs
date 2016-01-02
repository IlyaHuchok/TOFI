using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankPresentation.Validation
{
    public class ValidationResult
    {
        public bool IsValid { get; set; }
        public string Error { get; set; }
    }
}
