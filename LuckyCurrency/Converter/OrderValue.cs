using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;

namespace LuckyCurrency.Converter
{
    public class OrderValueExtension : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
            => new ValuesToObject();
    }

    class OrderValue : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values[0] is double priceOrder)
            {
                if (values[1] is double qtyOrder)
                    if(values[2] is string quoteCurrency)
                        return string.Format("{0:0.0000} {1}", priceOrder * qtyOrder, quoteCurrency);
            }
            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
