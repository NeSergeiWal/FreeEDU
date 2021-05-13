using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeEDU.Model.Course.CourseItem
{
	class CourseText : ICourseItem
	{
		public CourseItemType ItemType { get => CourseItemType.Text; }

		public string Text { get; set; }
	}
}
