using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeEDU.Model.Course
{
	class CurrentCourse
	{
		private static readonly CurrentCourse _currentCourse;

		private Course _Course { get; set; }

		public static Course GetCurrentCourse() => _currentCourse._Course;

		public static void SetCurrentCourse(Course course) => _currentCourse._Course = course;

		static CurrentCourse() => _currentCourse = new CurrentCourse();

		private CurrentCourse() { }
	}
}
