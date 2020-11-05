using System;
using System.Windows.Controls;

namespace Liftmanagement.Common
{
    public class MandatoryRule : ValidationRule
    {
        public string Name
        {
            get;
            set;
        }
        

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if(String.IsNullOrEmpty((string)value))
            {
                if (string.IsNullOrWhiteSpace(Name))
                    Name = "Field";
                return new ValidationResult(false, Name + " is mandatory.");
            }
            return ValidationResult.ValidResult;
        }
    }
}
