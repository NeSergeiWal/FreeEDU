using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeEDU.Model
{
	class CurrentCourse
	{
		private static readonly CurrentCourse _currentCourse;

		public Course Course { get; set; }

		public static Course GetCurrentCourse() => _currentCourse.Course;

		static CurrentCourse() => _currentCourse = new CurrentCourse();

		private CurrentCourse() { }
	}
}
