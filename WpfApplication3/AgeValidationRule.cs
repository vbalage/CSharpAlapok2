using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfApplication3
{
    public class AgeValidationRule : ValidationRule

    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int intValue;
            var success = int.TryParse(value.ToString(), out intValue);
            
            if(!success)
                return new ValidationResult(false, "Not a number");

            if (intValue < 0 || intValue > 150)
            {
                return new ValidationResult(false, "Invalid age");
            }

            return ValidationResult.ValidResult;

        }
    }
}
