using Silmoon.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silmoon.Maui.BindingConverts
{
    public class EnumNameConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is Enum)
            {
                return (value as Enum).GetDisplayName();
            }
            else if (value is string)
            {
                return value.ToString();
            }
            else
            {
                return value;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is Enum)
            {
                return (value as Enum).GetDisplayName();
            }
            else if (value is string)
            {
                return value.ToString();
            }
            else
            {
                return value;
            }
        }
    }
}
