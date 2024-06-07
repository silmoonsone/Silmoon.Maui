using Silmoon.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silmoon.Maui.Converters
{
    public class DisplayNameConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is Enum @enum)
                return @enum.GetDisplayName();
            else if (value is string)
                return value.ToString();
            else
                return value;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is Enum @enum)
                return @enum.GetDisplayName();
            else if (value is string)
                return value.ToString();
            else
                return value;
        }
    }
}
