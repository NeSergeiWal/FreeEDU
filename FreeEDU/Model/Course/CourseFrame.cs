using FreeEDU.Core;
using FreeEDU.Model.Course.CourseItem;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeEDU.Model.Course
{
	[Serializable]
	class CourseFrame : ObservableObject
	{
		public int Number { get; set; }

		public ObservableCollection<CourseItem.CourseText> CourseTheory { get; set; }

		public ObservableCollection<CourseItem.CourseQuestion> CourseQuestions { get; set; }

		private CourseFrameType _type;

		public CourseFrameType Type 
		{
			get => _type;
			set
			{
				_type = value;
				OnPropertyChanged("Type");
			}
		}

		public CourseFrame()
		{
			CourseTheory = new ObservableCollection<CourseItem.CourseText>();
			CourseQuestions = new ObservableCollection<CourseItem.CourseQuestion>();
		}
	}
}
