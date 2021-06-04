using FreeEDU.Core;
using System;
using System.Collections.ObjectModel;

namespace FreeEDU.Model.Course.CourseItem
{
	[Serializable]
	class CourseQuestion : ObservableObject, ICourseItem
	{
		public CourseItemType ItemType { get => CourseItemType.Question; }

		public ObservableCollection<QuestionAnswer> Answers { get; set; }

		public string Question { get; set; }

		[NonSerialized]
		private CheackResults _cheakResult = CheackResults.Uncheack;

		public CheackResults CheakResult
		{
			get => _cheakResult;
			set
			{
				if (_cheakResult == CheackResults.Ckecked)
				{
					foreach(var answer in Answers)
					{
						if(answer.IsRight != answer.IsSelected)
						{
							_cheakResult = CheackResults.Mistake;
							break;
						}
						else
						{
							_cheakResult = CheackResults.Right;
						}
					}
				}
				else
				{
					_cheakResult = CheackResults.Ckecked;
				}
				OnPropertyChanged("CheakResult");
			}
		}

		public CourseQuestion()
		{
			Answers = new ObservableCollection<QuestionAnswer>();
		}
	}
}
