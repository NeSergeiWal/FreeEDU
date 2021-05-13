using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeEDU.Model
{
	class User
	{
		public string Login { get; set; }

		public string Email { get; set; }

		public Roles Role { get; set; }
	}
}
