using System;
using System.Windows.Controls;
using System.Windows.Forms;
using Org.BouncyCastle.Crypto;

namespace Liftmanagement.Common
{
    public class DatetimeRule : ValidationRule
    {

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            var isStringDate = value is string;
            var isDate = value is DateTime;

            if (isStringDate)
            {
                if (String.IsNullOrEmpty((string)value))
                    return new ValidationResult(false,"Not valid Date");
            }else if (isDate)
            {
                var date = (DateTime)value;
                if (date < Helper.Helper.DefaultDate)
                    return new ValidationResult(false, "Not valid Date");
            }

            return ValidationResult.ValidResult;
        }
    }
}
