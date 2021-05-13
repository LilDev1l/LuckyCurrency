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
    public class TemplateConverterExtension : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
            => new TemplateConverter();
    }

    class TemplateConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] is double priceOrQty)
            {
                if(values[1] is int simbolsAfterComma)
                return string.Format("{0:F" + simbolsAfterComma + "}", priceOrQty);
            }
            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
