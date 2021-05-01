using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeEDU_Service.Model
{
	public class Account
	{
		public int Id { get; set; }

		public string Nikname { get; set; }

		public string Email { get; set; }

		public Roles Role { get; set; }

		public int UserId { get; set; }
	}
}
