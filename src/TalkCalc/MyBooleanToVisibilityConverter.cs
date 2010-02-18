
using System.Globalization;
using System.Windows.Data;
using System.Windows;
using System;

namespace TalkCalc
{
    public class MyBooleanToVisibilityConverter : IValueConverter
    {
        public bool Invert { get; set; }


        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var flag = false;

            if (value is bool)
                flag = (bool)value;
            else if (value is bool?)
                flag = ((bool?)value).GetValueOrDefault();
            else
                flag = (bool)System.Convert.ChangeType(value, typeof(bool));

            if (Invert) flag = !flag;
            return flag ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = false;

            if (value is Visibility)
                result = ((Visibility)value == Visibility.Visible);
            else
                result = ((Visibility)Enum.Parse(typeof(Visibility), value.ToString()) ==
                    Visibility.Visible);

            return Invert ? !result : result;
        }
    }
}