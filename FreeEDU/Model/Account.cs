using FreeEDU.Core.Converters;
using FreeEDU.Core.Serializers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeEDU.Model
{
	class Account
	{
		public string Login { get; set; }

		public Roles Role { get; set; }

		public ObservableCollection<Course> Courses { get; set; }

		private static readonly Account _account;

		public static Account GetAccount() => _account;

		public void FullSetProps(string login, string role, string coursesJson = null)
		{
			Login = login;
			Role = RoleConverter.GetRoles(role);
			Courses = CourseCollectionSerializer.Deserialize(coursesJson);
		}

		static Account() => _account = new Account();

		private Account() { }
	}
}
