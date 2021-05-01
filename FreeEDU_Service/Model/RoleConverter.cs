using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeEDU_Service.Model
{
	 static class RoleConverter
	{
		public static Roles GetRoles(int role)
		{
			switch(role)
			{
				case 0: { return Roles.Admin; }
				case 1: { return Roles.Teacher; }
				default: { return Roles.Pupil; }
			}
		}
	}
}
