using FreeEDU.Core.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace FreeEDU.Model
{
	class User
	{
		public string Login { get; set; }

		public string Email { get; set; }

		public byte[] ByteImage { get; set; }

		public Roles Role { get; set; }

		public static User Init(string[] data)
		=> new User()
		{
			 Login = data[0],
			 Email = data[1],
			 ByteImage = JsonSerializer.Deserialize<byte[]>(data[2]),
			 Role = RoleConverter.GetRoles(data[3])
		};

		public string[] GetUserModul()
		=> new string[]
		{
			Login,
			RoleConverter.GetSqlRoles(Role)
		};
	}
}
