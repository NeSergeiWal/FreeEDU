using FreeEDU.Model.Course.CourseItem;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace FreeEDU.Core.Converters
{
	class CheackResultConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value != null)
			{
				switch ((CheackResults)value)
				{
					case CheackResults.Right: { return Brushes.LightGreen; }
					case CheackResults.Mistake: { return Brushes.Red; }
					default: { return Brushes.White; }
				}
			}

			return null;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
			=> DependencyProperty.UnsetValue;
	}
}
