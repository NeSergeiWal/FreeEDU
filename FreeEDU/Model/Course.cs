using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeEDU.Model
{
	class Course
	{
		public string Name { get; set; }

		public string Teacher { get; set; }

		public DateTime CreateDate { get; set; }

		public DateTime UpdateDate { get; set; }

		public Languages Language { get; set; }

		public string Info { get; set; }
	}
}
