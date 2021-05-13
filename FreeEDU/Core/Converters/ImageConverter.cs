using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace FreeEDU.Core.Converters
{
	static class ImageConverter
	{
		public static BitmapImage BytesToImage(byte[] byteImage)
		{
			BitmapImage image = new BitmapImage();

			MemoryStream stream = new MemoryStream(byteImage);
			image.BeginInit();
			image.StreamSource = stream;
			image.EndInit();

			return image;
		}
	}
}
