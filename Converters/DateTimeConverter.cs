using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Liftmanagement.Converters
{
    public class DateTimeConverter : IValueConverter
    {
      
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                DateTime test = (DateTime)value;
                string date = test.ToString("dd.MM.yyyy");
                return (date);
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                string test = (string)value;
                DateTime date;
                var dd = DateTime.TryParseExact(test, "dd.MM.yyyy", CultureInfo.InvariantCulture,DateTimeStyles.None,out date);

                if (dd)
                {
                    return date;
                }              
            }
            return Helper.Helper.DefaultDate.Date;
        }

    }
}
