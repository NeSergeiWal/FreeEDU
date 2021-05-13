using FreeEDU.Core;
using FreeEDU.Model;
using FreeEDU.Model.Course;
using FreeEDU.Model.Course.CourseItem;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace FreeEDU.ViewModel
{
	class CourseViewModel : ObservableObject, IViewModel
	{
		private ObservableCollection<CourseFrame> _CourseFrames { get; set; }

		private CourseFrame _currentFrame;

		public CourseFrame CurrentFrame 
		{
			get => _currentFrame;
			set
			{
				_currentFrame = value;

				Check = (_currentFrame.Type == CourseFrameType.Theory) ? Visibility.Collapsed : Visibility.Visible;
			}
		}

		private bool _isChecked;

		public bool IsChecked
		{
			get => _isChecked;
			set 
			{
				_isChecked = value;
				OnPropertyChanged("IsChecked");
			}
		}

		private Brush _checkResult = Brushes.White;

		public Brush CheckResult 
		{
			get => _checkResult;
			set
			{
				if (IsChecked)
				{
					foreach (CourseQuestion question in CurrentFrame.CourseItems)
					{
						foreach (QuestionAnswer answer in question.Answers)
						{
							if (answer.IsRight != answer.IsSelected)
							{
								_checkResult = Brushes.Red;
								OnPropertyChanged("CheckResult");
								return;
							}
						}
					}


					_checkResult = Brushes.LightGreen;
					OnPropertyChanged("CheckResult");
					return;
				}

				_checkResult = Brushes.White;
				OnPropertyChanged("CheckResult");
			}
		}

		private Visibility _previous;

		public Visibility Previous
		{
			get => _previous;
			set 
			{ 
				_previous = value;
				OnPropertyChanged("Previous");
			}
		}

		private Visibility _next;

		public Visibility Next
		{
			get => _next;
			set 
			{ 
				_next = value;
				OnPropertyChanged("Next");
			}
		}

		private Visibility _check;

		public Visibility Check
		{
			get => _check;
			set 
			{ 
				_check = value;
				OnPropertyChanged("Check");
			}
		}


		#region PreviousFrameCommand
		public RelayCommand PreviousFrameCommand { get; set; }

		private void DoPreviousFrame(object obj)
		{
			if (CurrentFrame.Number != 0)
			{
				CurrentFrame = _CourseFrames[CurrentFrame.Number - 1];
				Previous = (CurrentFrame.Number == 0) ? Visibility.Collapsed : Visibility.Visible;
				IsChecked = false;
			}
		}
		#endregion

		#region NextFrameCommand
		public RelayCommand NextFrameCommand { get; set; }

		private void DoNextFrame(object obj)
		{
			if (CurrentFrame.Number != _CourseFrames.Count-1)
			{
				CurrentFrame = _CourseFrames[CurrentFrame.Number + 1];
				Next = (CurrentFrame.Number == _CourseFrames.Count - 1) ? Visibility.Collapsed : Visibility.Visible;
				IsChecked = false;
			}
		}
		#endregion

		#region CheckCommand
		public RelayCommand CheckCommand { get; set; }

		private void DoCheckCommand(object obj)
		{
			if (!IsChecked)
			{
				IsChecked = true;
				CheckResult = Brushes.Transparent;
				if (CheckResult == Brushes.LightGreen)
				{
					Next = Visibility.Collapsed;
				}
				else
				{
					Next = Visibility.Collapsed;
				}
			}
			else
			{
				IsChecked = false;
				foreach (CourseQuestion question in CurrentFrame.CourseItems)
				{
					foreach (QuestionAnswer answer in question.Answers)
					{
						answer.IsSelected = false;
					}
				}
				CheckResult = Brushes.Transparent;
			}
		}
		#endregion

		public CourseViewModel()
		{
			Course course = CurrentCourse.GetCurrentCourse();
			_CourseFrames = course.CourseFrames;
			CurrentFrame = _CourseFrames[0];

			PreviousFrameCommand = new RelayCommand(DoPreviousFrame);
			NextFrameCommand = new RelayCommand(DoNextFrame);
			CheckCommand = new RelayCommand(DoCheckCommand);
		}
	}
}
