using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Silmoon.Xamarin.BindingConverts
{
    public class ByteArrayToImageSourceConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ByteToImageSource(value as byte[]);
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
        public static ImageSource ByteToImageSource(byte[] imageBytes) => imageBytes == null || imageBytes.Length == 0 ? null : ImageSource.FromStream(() => new MemoryStream(imageBytes));
    }
}
