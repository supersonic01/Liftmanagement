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
   public class DecimalConverter : IValueConverter
   {
       public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
       {
           return value;
       }

       public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
       {
           if (value == null)
           {
               return null;
           }
           if (value is string stringValue && targetType == typeof(decimal))
           {
               var decSeparator = culture.NumberFormat.NumberDecimalSeparator;
               var normString = decSeparator == "."
                   ? stringValue.Replace(",", ".")
                   : stringValue.Replace(".", ",");
               if (!normString.EndsWith(decSeparator) && decimal.TryParse(normString, out var decResult))
               {
                   return decResult;
               }
           }
           return DependencyProperty.UnsetValue;
       }
   }
   
}
