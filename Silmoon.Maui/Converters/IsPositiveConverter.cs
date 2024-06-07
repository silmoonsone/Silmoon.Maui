using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silmoon.Maui.Converters
{
    /// <summary>
    /// 传入的值如果是正数，则返回true，否则返回false，传入的值必须是一个数字类型。
    /// </summary>
    public class IsPositiveConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return false;
            try
            {
                decimal valueDecimal = System.Convert.ToDecimal(value, culture);
                return valueDecimal > 0;
            }
            catch
            {
                return false;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
