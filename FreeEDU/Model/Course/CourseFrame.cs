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
	class CourseFrame : ObservableObject
	{
		public int Number { get; set; }

		public ObservableCollection<ICourseItem> CourseItems { get; set; }

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
			CourseItems = new ObservableCollection<ICourseItem>();
		}
	}
}
