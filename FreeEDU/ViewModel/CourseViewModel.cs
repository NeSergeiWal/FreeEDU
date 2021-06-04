using FreeEDU.Core;
using FreeEDU.FreeEDU_Service;
using FreeEDU.Model;
using FreeEDU.Model.Course;
using FreeEDU.Model.Course.CourseItem;
using FreeEDU.View;
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

		private BaseViewModel _WindowVM { get; set; }

		private CourseFrame _currentFrame;

		public CourseFrame CurrentFrame 
		{
			get => _currentFrame;
			set
			{
				_currentFrame = value;

				Check = (_currentFrame.Type == CourseFrameType.Theory) ? Visibility.Collapsed : Visibility.Visible;
				OnPropertyChanged("CurrentFrame");
			}
		}

		private string _actionMsg;

		public string ActionMsg
		{
			get => _actionMsg;
			set 
			{
				if (CurrentFrame.Number != _CourseFrames.Count - 1)
				{
					_actionMsg = (IsChecked) ? "Refresh" : "Cheack";
				}
				else
				{
					_actionMsg = (IsChecked) ? "Refresh" : "Finish";
				}
				OnPropertyChanged("ActionMsg");
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
				Next = Visibility.Visible;
				Check = Visibility.Collapsed;
				IsChecked = false;
				ActionMsg = "";
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
				Next = (CurrentFrame.Number == _CourseFrames.Count - 1 
					|| (CurrentFrame.Type == CourseFrameType.Question && CheackFrame())) ?
					Visibility.Collapsed : Visibility.Visible;
				Previous = Visibility.Visible;
				IsChecked = false;
				Check = (CurrentFrame.Number == _CourseFrames.Count - 1 || CurrentFrame.Type == CourseFrameType.Question) ?
					Visibility.Visible : Visibility.Collapsed;
				ActionMsg = "";
			}
		}
		#endregion

		#region CheckCommand
		public RelayCommand CheckCommand { get; set; }

		private async void DoCheckCommand(object obj)
		{
			if (!IsChecked)
			{
				IsChecked = true;
				bool res = CheackFrame();
				if (res && CurrentFrame.Number != _CourseFrames.Count - 1)
				{
					Next = Visibility.Visible;
					Check = Visibility.Collapsed;
					_WindowVM.ErrorMsg = "You reight. Go next!";
				}
				else if (!res)
				{
					Next = Visibility.Collapsed;
					ActionMsg = "";
					_WindowVM.ErrorMsg = "You lose. Refresh questions!";
					return;
				}
				else
				{
					_WindowVM.ChangePageCommand.Execute(PageViews.CourseList);
					_WindowVM.ErrorMsg = "Test was add to your complete tests";
					Account account = Account.GetAccount();
					Course course = CurrentCourse.GetCurrentCourse();
					try
					{
						using (FreeEDU_ServiceClient service = new FreeEDU_ServiceClient())
						{
							await service.AddComletedCoursesAsync(account.Login, course.Id);
						}
					}
					catch
					{
						account.Init(new string[] { null, null, null });
						LoginWindow loginWindow = new LoginWindow();
						_WindowVM.ErrorMsg = "Server isn't responding";
						_WindowVM.CloseWindowCommand.Execute(null);
						loginWindow.Show();
					}
				}

			}
			else
			{
				IsChecked = false;
				foreach (CourseQuestion question in CurrentFrame.CourseQuestions)
				{
					foreach (QuestionAnswer answer in question.Answers)
					{
						answer.IsSelected = false;
					}
					question.CheakResult = 0;
				}
				ActionMsg = "";
			}
		}
		#endregion

		private bool CheackFrame()
		{
			bool res = true;
			foreach(var question in CurrentFrame.CourseQuestions)
			{
				question.CheakResult = 0;
				if(question.CheakResult != CheackResults.Right)
				{
					res = false;
				}
			}

			return res;
		}

		public CourseViewModel(BaseViewModel window)
		{
			Course course = CurrentCourse.GetCurrentCourse();
			_CourseFrames = course.CourseFrames;
			CurrentFrame = _CourseFrames[0];
			_WindowVM = window;

			Previous = Visibility.Collapsed;
			ActionMsg = "";
			Next = (CurrentFrame.Number != _CourseFrames.Count - 1) ? Visibility.Visible : Visibility.Collapsed;

			PreviousFrameCommand = new RelayCommand(DoPreviousFrame);
			NextFrameCommand = new RelayCommand(DoNextFrame);
			CheckCommand = new RelayCommand(DoCheckCommand);
		}
	}
}
