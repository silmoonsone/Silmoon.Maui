using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silmoon.Maui.Converters
{
    /// <summary>
    /// 传入的值加上一个传入的参数的值，返回结果，传入的值是一个数字，可以是int、long、float、double、decimal等
    /// </summary>
    public class IntegerPlusConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return value;
            parameter ??= 1m;

            try
            {
                decimal valueDecimal = System.Convert.ToDecimal(value, culture);
                decimal parameterDecimal = System.Convert.ToDecimal(parameter, culture);

                decimal result = valueDecimal + parameterDecimal;

                if (targetType == typeof(int))
                    return (int)result;
                if (targetType == typeof(uint))
                    return (uint)result;
                if (targetType == typeof(short))
                    return (short)result;
                if (targetType == typeof(ushort))
                    return (ushort)result;
                if (targetType == typeof(long))
                    return (long)result;
                if (targetType == typeof(ulong))
                    return (ulong)result;
                if (targetType == typeof(float))
                    return (float)result;
                if (targetType == typeof(double))
                    return (double)result;
                if (targetType == typeof(decimal))
                    return result;

                return result.ToString(culture);
            }
            catch
            {
                return value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return value;
            parameter ??= 1m;

            try
            {
                decimal valueDecimal = System.Convert.ToDecimal(value, culture);
                decimal parameterDecimal = System.Convert.ToDecimal(parameter, culture);

                decimal result = valueDecimal - parameterDecimal;

                if (targetType == typeof(int))
                    return (int)result;
                if (targetType == typeof(uint))
                    return (uint)result;
                if (targetType == typeof(short))
                    return (short)result;
                if (targetType == typeof(ushort))
                    return (ushort)result;
                if (targetType == typeof(long))
                    return (long)result;
                if (targetType == typeof(ulong))
                    return (ulong)result;
                if (targetType == typeof(float))
                    return (float)result;
                if (targetType == typeof(double))
                    return (double)result;
                if (targetType == typeof(decimal))
                    return result;

                return result.ToString(culture);
            }
            catch
            {
                return value;
            }
        }
    }
}
