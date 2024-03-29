﻿using FreeEDU.Core;
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
using System.Text.Json;
using System.Threading.Tasks;

namespace FreeEDU.ViewModel
{
	class CourseConstructorViewModel : ObservableObject, IViewModel
	{
		private string _frameType;

		public string FrameType
		{
			get => _frameType;
			set 
			{
				_frameType = (CurrentFrame.Type == CourseFrameType.Theory) ? "Theory" : "Question";
				OnPropertyChanged("FrameType");
			}
		}


		private ICourseItem _selectedItem;

		public ICourseItem SelectedItem
		{
			get => _selectedItem;
			set 
			{ 
				_selectedItem = value;

				if(_selectedItem is CourseQuestion)
				{
					CurrentQuestion = (CourseQuestion)_selectedItem;
				}
			}
		}

		public CourseQuestion CurrentQuestion { get; set; }

		private CourseFrame _currentFrame;

		public CourseFrame CurrentFrame
		{
			get => _currentFrame;
			set 
			{ 
				_currentFrame = value;
				OnPropertyChanged("CurrentFrame");
			}
		}

		private ObservableCollection<CourseFrame> _CourseFrames { get; set; }

		private BaseViewModel _WindowVM { get; set; }
		
		#region AddTextCommand
		public RelayCommand AddTextCommand { get; set; }

		private void DoAddText(object obj)
		{
			if (CurrentFrame.Type == CourseFrameType.Theory)
			{
				if (CurrentFrame.CourseTheory.Count == 0
					|| CurrentFrame.CourseTheory[CurrentFrame.CourseTheory.Count - 1].Text != null)
				{
					CurrentFrame.CourseTheory.Add(new CourseText());
				}
			}
		}
		#endregion
		
		#region AddQuestionCommand
		public RelayCommand AddQuestionCommand { get; set; }

		private void DoAddQuestion(object obj)
		{
			if ((CurrentQuestion == null
				|| CurrentQuestion.Question != null)
				&& CurrentFrame.Type == CourseFrameType.Question)
			{
				CurrentQuestion = new CourseQuestion();
				CurrentQuestion.Answers.Add(new QuestionAnswer());
				CurrentFrame.CourseQuestions.Add(CurrentQuestion);
			}
		}
		#endregion
		
		#region AddAnswerCommand
		public RelayCommand AddAnswerCommand { get; set; }

		private void DoAddAnswer(object obj)
		{
			if (CurrentQuestion.Question != null && CurrentFrame.Type == CourseFrameType.Question)
			{
				CurrentQuestion.Answers.Add(new QuestionAnswer());
			}
		}
		#endregion
		
		#region DeleteItemCommand
		public RelayCommand DeleteItemCommand { get; set; }

		private void DoDeleteItem(object obj)
		{
			if (_selectedItem != null)
			{
				if (CurrentFrame.Type == CourseFrameType.Theory)
				{
					CurrentFrame.CourseTheory.Remove((CourseText)_selectedItem);
				}
				else
				{
					CurrentFrame.CourseQuestions.Remove((CourseQuestion)_selectedItem);
				}
			}
		}
		#endregion
		
		#region PreviousFrameCommand
		public RelayCommand PreviousFrameCommand { get; set; }

		private void DoPreviousFrame(object obj)
		{
			if(CurrentFrame.Number != 0)
			{
				CurrentFrame = _CourseFrames[CurrentFrame.Number - 1];
			}
		}
		#endregion
		
		#region NextFrameCommand
		public RelayCommand NextFrameCommand { get; set; }

		private void DoNextFrame(object obj)
		{
			if(CurrentFrame.Number != _CourseFrames.Count-1)
			{
				CurrentFrame = _CourseFrames[CurrentFrame.Number + 1];
			}
			else
			{
				if (CurrentFrame.CourseTheory.Count != 0 || CurrentFrame.CourseQuestions.Count != 0)
				{
					CourseFrame newFrame = new CourseFrame() { Number = CurrentFrame.Number + 1 };
					newFrame.Type = CourseFrameType.Theory;
					CurrentFrame = newFrame;
					FrameType = null;
					_CourseFrames.Add(newFrame);
				}
			}
		}
		#endregion
		
		#region FinishCommand
		public RelayCommand FinishCommand { get; set; }

		private async void DoFinish(object obj)
		{
			if (CurrentFrame.CourseTheory.Count != 0 || CurrentFrame.CourseQuestions.Count != 0)
			{
				Account account = Account.GetAccount();
				Course newCourse = CurrentCourse.GetCurrentCourse();
				newCourse.CourseFrames = _CourseFrames;
				newCourse.Teacher = account.Login;
				newCourse.CreateDate = newCourse.UpdateDate = DateTime.Now.ToShortDateString();
				try
				{
					using (FreeEDU_ServiceClient service = new FreeEDU_ServiceClient())
					{
						await service.CreateCourseAsync(newCourse.GetCourseModul());
					}
				}
				catch
				{
					_WindowVM.ErrorMsg = "Server isn't responding";
					Account.GetAccount().Init(new string[] { null, null, null });
					LoginWindow loginWindow = new LoginWindow();
					_WindowVM.CloseWindowCommand.Execute(null);
					loginWindow.Show();
					return;
				}

				_WindowVM.ChangePageCommand.Execute(obj);
				_WindowVM.ErrorMsg = "Create successful";
			}
		}
		#endregion
		
		#region DeleteFrameCommand
		public RelayCommand DeleteFrameCommand { get; set; }

		private void DoDeleteFrame(object obj)
		{
			if (_CourseFrames.Count != 1)
			{
				CourseFrame temp = CurrentFrame;
				CurrentFrame = (CurrentFrame.Number == 0) ? _CourseFrames[1] : _CourseFrames[0];
				_CourseFrames.Remove(temp);
			}
		}
		#endregion
		
		#region ChangeFrameTypeCommand
		public RelayCommand ChangeFrameTypeCommand { get; set; }

		private void DoChangeFrameType(object obj)
		{
			CurrentFrame.Type = (CurrentFrame.Type == CourseFrameType.Theory) ? CourseFrameType.Question : CourseFrameType.Theory;
			FrameType = null;
			if (CurrentFrame.Type == CourseFrameType.Theory)
			{
				CurrentFrame.CourseTheory.Clear();
			}
			else
			{
				CurrentFrame.CourseQuestions.Clear();
			}
		}
		#endregion

		public CourseConstructorViewModel(BaseViewModel windowVM)
		{
			_WindowVM = windowVM;

			_CourseFrames = new ObservableCollection<CourseFrame>();
			CurrentFrame = new CourseFrame() { Number = 0, Type = CourseFrameType.Theory };
			FrameType = null;

			_CourseFrames.Add(CurrentFrame);

			AddTextCommand = new RelayCommand(DoAddText);
			AddQuestionCommand = new RelayCommand(DoAddQuestion);
			AddAnswerCommand = new RelayCommand(DoAddAnswer);
			DeleteItemCommand = new RelayCommand(DoDeleteItem);
			PreviousFrameCommand = new RelayCommand(DoPreviousFrame);
			NextFrameCommand = new RelayCommand(DoNextFrame);
			FinishCommand = new RelayCommand(DoFinish);
			DeleteFrameCommand = new RelayCommand(DoDeleteFrame);
			ChangeFrameTypeCommand = new RelayCommand(DoChangeFrameType);
		}
	}
}
