using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silmoon.Maui.Converters
{
    public class StreamToImageSourceConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ByteToImageSource(value as Stream);
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
        public static ImageSource ByteToImageSource(Stream stream) => stream == null || stream.Length == 0 ? null : ImageSource.FromStream(() => stream);
    }
}
