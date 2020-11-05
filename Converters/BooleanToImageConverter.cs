using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Liftmanagement.Converters
{
    public class BooleanToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var uri = (bool)value
                ? "pack://application:,,,../Resources/Images/Icons/Dryicons-Aesthetica-2-Process-accept.ico"
                : "pack://application:,,,../Resources/Images/Icons/Dryicons-Aesthetica-2-Process.ico";
            return new BitmapImage(new Uri(uri));
            //  <Image Source="..\Resources\Images\Icons\Dryicons-Aesthetica-2-Process.ico" Height="30"/>
            //"pack://application:,,,../Resources/Images/Icons/Dryicons-Aesthetica-2-Process.ico", UriKind.RelativeOrAbsolute));
            //
        }

        public object ConvertBack(
          object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
   
}
