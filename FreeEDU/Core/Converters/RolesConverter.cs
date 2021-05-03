using FreeEDU.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeEDU.Core.Converters
{
	static class RoleConverter
	{
		public static Roles GetRoles(string role)
		{
			switch (role)
			{
				case "adm": { return Roles.Admin; }
				case "tch": { return Roles.Teacher; }
				default: { return Roles.Student; }
			}
		}
	}
}
