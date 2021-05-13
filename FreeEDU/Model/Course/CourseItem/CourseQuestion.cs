using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeEDU.Model.Course.CourseItem
{
	class CourseQuestion : ICourseItem
	{
		public CourseItemType ItemType { get => CourseItemType.Question; }

		public string Question { get; set; }

		public ObservableCollection<QuestionAnswer> Answers { get; set; }

		public CourseQuestion()
		{
			Answers = new ObservableCollection<QuestionAnswer>();
		}
	}
}
