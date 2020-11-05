using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Liftmanagement.Converters
{
    public class DateDateConverter : IValueConverter
    {
      
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var isStringValue = value is string;

            if (value == null)
            { 
                return DateTime.MaxValue;
            }else if (value is string)
            {
                if (string.IsNullOrWhiteSpace(value.ToString()))
                {
                    return DateTime.MaxValue;
                }
            }


            DateTime dateTime;
          
            //var dd = DateTime.TryParseExact(value.ToString(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime);

            dateTime = DateTime.Parse(value.ToString());

            return dateTime;

        }

    }
}
