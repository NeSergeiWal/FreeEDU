using FreeEDU.Core.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace FreeEDU.Model.Course.CourseItem
{
	class CourseImg : ICourseItem
	{
		public CourseItemType ItemType { get => CourseItemType.Image; }

		public BitmapImage Image { get; private set; }

		private byte[] _byteImage;

		public byte[] ByteImage
		{
			get => _byteImage;
			set 
			{
				_byteImage = value;
				Image = ImageConverter.BytesToImage(_byteImage);
			}
		}
	}
}
