﻿using FreeEDU.Model;
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

		public static string GetStringRoles(Roles role)
		{
			switch(role)
			{
				case Roles.Admin: { return "Admin"; }
				case Roles.Teacher: { return "Teacher"; }
				default: { return "Student"; }
			}
		}
		
		public static string GetSqlRoles(Roles role)
		{
			switch(role)
			{
				case Roles.Admin: { return "adm"; }
				case Roles.Teacher: { return "tch"; }
				default: { return "stn"; }
			}
		}
	}
}
