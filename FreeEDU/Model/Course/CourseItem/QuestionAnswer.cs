using FreeEDU.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace FreeEDU.Model.Course.CourseItem
{
	[Serializable]
	class QuestionAnswer : ObservableObject, ICourseItem
	{
		public CourseItemType ItemType { get => CourseItemType.Answer; }

		public string Answer { get; set; }

		public bool IsRight { get; set; }

		private bool _isSelected;

		public bool IsSelected
		{
			get => _isSelected;
			set
			{
				_isSelected = value;
				OnPropertyChanged("IsSelected");
			}
		}
	}
}
