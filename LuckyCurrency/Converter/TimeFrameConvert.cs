using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;

namespace LuckyCurrency.Converter
{
    public class TimeFrameConvertExtension : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
            => new TimeFrameConvert();
    }

    class TimeFrameConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            return new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ConvertInterval((string)value);
        }

        string ConvertInterval(string timeframeL)
        {
            switch (timeframeL)
            {
                case "1m":
                    return "1";
                case "3m":
                    return "3";
                case "5m":
                    return "5";
                case "15m":
                    return "15";
                case "30m":
                    return "30";
                case "1h":
                    return "60";
                case "2h":
                    return "120";
                case "4h":
                    return "240";
                case "6h":
                    return "360";
                case "D":
                    return "D";
                case "W":
                    return "W";
                case "M":
                    return "M";
                default:
                    throw new Exception("Invalid spacing format");
            }

        }
    }
}
