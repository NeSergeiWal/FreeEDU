using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using static System.Net.Mime.MediaTypeNames;

namespace FreeEDU.Core.Converters
{
	public class ImageConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value != null)
			{
				BitmapImage image = new BitmapImage();

				MemoryStream stream = new MemoryStream((byte[])value);
				image.BeginInit();
				image.StreamSource = stream;
				image.EndInit();

				return image;
			}

			return @"D:\Новая папка (4)\FreeEDU\FreeEDU\Resources\Images\answer_icon.png";
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
			=> DependencyProperty.UnsetValue;
	}
}
