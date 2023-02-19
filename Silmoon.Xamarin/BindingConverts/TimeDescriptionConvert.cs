using Silmoon.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Silmoon.Xamarin.BindingConverts
{
    /// <summary>
    /// 时间描述转换器，将一个DateTime类型转换成一个String类型，以1分钟前、3小时前、10天前类似的方式描述这个时间
    /// </summary>
    public class TimeDescriptionConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is DateTime dateTime)
            {
                return dateTime.GetDescription();
            }
            else return value;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is DateTime dateTime)
            {
                return dateTime.GetDescription();
            }
            else return value;
        }
    }
}
