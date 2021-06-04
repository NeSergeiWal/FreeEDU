using FreeEDU.Core.Converters;
using FreeEDU.FreeEDU_Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FreeEDU.Model
{
	[Serializable]
	class Account
	{
		public string Login { get; set; }

		public Roles Role { get; set; }

		public byte[] ByteImage { get; set; }

		public ObservableCollection<FreeEDU.Model.Course.Course> Courses { get; set; }

		private static readonly Account _account;

		public static Account GetAccount() => _account;

		public void Init(string[] data)
		{
			Login = data[0];
			Role = RoleConverter.GetRoles(data[1]);
			
			Courses = new ObservableCollection<Course.Course>();
			if (Login != null)
			{
				UpdateCompletedCourses();
			}

			ByteImage = (data[2] != null) ? JsonSerializer.Deserialize<byte[]>(data[2]) : null;
		}

		public string[] GetAccountModul()
		{
			return new string[]
			{
				Login,
				JsonSerializer.Serialize(ByteImage)
			};
		}

		public void UpdateCompletedCourses()
		{
			using (FreeEDU_ServiceClient service = new FreeEDU_ServiceClient())
			{
				foreach (string[] course in service.GetComletedCoursesAsync(Login).Result)
				{
					Courses.Add(Course.Course.Init(course));
				}
			}
		}

		static Account() => _account = new Account();

		private Account() { }
	}
}
