using Momentum.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Humanizer;
using Humanizer.Configuration;
using Humanizer.DateTimeHumanizeStrategy;

namespace Momentum.Converters
{
    public class HumanizeDateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Configurator.DateTimeHumanizeStrategy = new PrecisionDateTimeHumanizeStrategy(1);

            if (value is DateTime)
            {
                DateTime dateTime = (DateTime)value;

                return dateTime.Humanize(false);
            }

            if (value is DateTimeOffset)
            {
                DateTimeOffset dateTimeOffset = (DateTimeOffset)value;

                return dateTimeOffset.Humanize();
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
