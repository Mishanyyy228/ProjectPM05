using System;
using System.Globalization;
using System.Windows.Data;

public class UnixTimestampConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is int timestamp && timestamp > 0)
        {
            // Преобразуем timestamp в DateTime
            var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(timestamp);
            return dateTime.ToLocalTime().ToString("dd/MM/yyyy HH:mm:ss");
        }
        else
        {
            return string.Empty;
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}