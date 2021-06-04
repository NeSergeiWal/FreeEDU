using FreeEDU.Core;
using FreeEDU.Core.Converters;
using FreeEDU.Model.Course.CourseItem;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace FreeEDU.Model.Course
{
	[Serializable]
	class Course : ObservableObject
	{
		private int id;

		public int Id { get; set; }

		public string Name { get; set; }

		public string Teacher { get; set; }

		public string CreateDate { get; set; }

		public string UpdateDate { get; set; }

		public Languages Language { get; set; }

		public byte[] ByteImage { get; set; }

		public string Info { get; set; }

		public Visibility Delete { get; set; }

		public ObservableCollection<CourseFrame> CourseFrames { get; set; }

		public Course()
		{
			Account account = Account.GetAccount();
			Delete = (Teacher == account.Login || account.Role == Roles.Admin) ? Visibility.Visible : Visibility.Collapsed;
		}

		public string[] GetCourseModul()
		=> new string[]
			{
				Name,
				Teacher,
				CreateDate,
				UpdateDate,
				Enum.GetName(typeof(Languages), Language),
				Info,
				JsonSerializer.Serialize(ByteImage),
				JsonSerializer.Serialize(CourseFrames)
			};

		public string[] GetUpdateModul()
		=> new string[]
		{
			Id.ToString(),
			Name,
			Enum.GetName(typeof(Languages), Language),
			Info,
			JsonSerializer.Serialize(this)
		};

		public static Course Init(string[] data)
		{
			Course newCourse = new Course();

			newCourse.Id = int.Parse(data[0]);
			newCourse.Name = data[1];
			newCourse.Teacher = data[2];
			newCourse.CreateDate = data[3];
			newCourse.UpdateDate = data[4];
			newCourse.Language = (Languages)Enum.Parse(typeof(Languages), data[5]);
			newCourse.Info = data[6];
			newCourse.CourseFrames = JsonSerializer.Deserialize<ObservableCollection<CourseFrame>>(data[8]);
			newCourse.ByteImage = JsonSerializer.Deserialize<byte[]>(data[7]);

			return newCourse;
		}
	}
}
