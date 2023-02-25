using Silmoon.Extension;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Silmoon.Xamarin.BindingConverts
{
    public class IsNullOrEmptyConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null) return true;

            if (value is string str && str.IsNullOrEmpty())
            {
                return true;
            }
            else if (value is ICollection collection && collection.Count > 1)
            {
                return false;
            }
            else if (value is Array array && array.Length > 1)
            {
                return false;
            }

            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
