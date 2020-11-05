using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Liftmanagement.Converters
{
    public class MultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter,
            CultureInfo culture)
        {
            //return values.Cast<bool>().Any(x => x) ?
            //    Visibility.Collapsed : Visibility.Visible;

            foreach (var d in values.Cast<double>())
            {
                Console.WriteLine(d);
            }

            return 200;
        }

        public object[] ConvertBack(object value, Type[] targetTypes,
            object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}