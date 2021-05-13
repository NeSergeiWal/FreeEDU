using FreeEDU.Core;
using FreeEDU.Core.Converters;
using FreeEDU.Model.Course.CourseItem;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace FreeEDU.Model.Course
{
	class Course : ObservableObject
	{
		public string Name { get; set; }

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

		public string Teacher { get; set; }

		public string CreateDate { get; set; }

		public string UpdateDate { get; set; }

		public Languages Language { get; set; }

		public string Info { get; set; }

		public Visibility Delete { get; set; }

		public ObservableCollection<CourseFrame> CourseFrames { get; set; }

		public Course()
		{
			Account account = Account.GetAccount();
			Delete = (Teacher == account.Login || account.Role == Roles.Admin) ? Visibility.Visible : Visibility.Collapsed;
		}
	}
}
