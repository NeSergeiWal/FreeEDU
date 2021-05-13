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

		#region AddImageCommand
		public RelayCommand AddImageCommand { get; set; }

		private void DoAddImage(object obj)
		{
			if (CurrentFrame.Type == CourseFrameType.Theory)
			{
				OpenFileDialog file = new OpenFileDialog();
				if (file.ShowDialog() == true)
				{
					byte[] image;
					using (FileStream stream = new FileStream(file.FileName, FileMode.Open))
					{
						image = new byte[stream.Length];
						stream.Read(image, 0, image.Length);
					}

					CurrentFrame.CourseItems.Add(new CourseImg() { ByteImage = image });
				}
			}
		}
		#endregion
		
		#region AddTextCommand
		public RelayCommand AddTextCommand { get; set; }

		private void DoAddText(object obj)
		{
			if (CurrentFrame.Type == CourseFrameType.Theory)
			{
				CurrentFrame.CourseItems.Add(new CourseText());
			}
		}
		#endregion
		
		#region AddQuestionCommand
		public RelayCommand AddQuestionCommand { get; set; }

		private void DoAddQuestion(object obj)
		{
			if ((CurrentQuestion == null
				|| CurrentQuestion.Question != string.Empty)
				&& CurrentFrame.Type == CourseFrameType.Question)
			{
				CurrentQuestion = new CourseQuestion();
				CurrentFrame.CourseItems.Add(CurrentQuestion);
			}
		}
		#endregion
		
		#region AddAnswerCommand
		public RelayCommand AddAnswerCommand { get; set; }

		private void DoAddAnswer(object obj)
		{
			if (CurrentQuestion.Question != string.Empty && CurrentFrame.Type == CourseFrameType.Question)
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
				CurrentFrame.CourseItems.Remove(_selectedItem);
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
				if (CurrentFrame.CourseItems.Count != 0)
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

		private void DoFinish(object obj)
		{
			if (CurrentFrame.CourseItems.Count != 0)
			{
				Account account = Account.GetAccount();
				Course newCourse = CurrentCourse.GetCurrentCourse();
				newCourse.CourseFrames = _CourseFrames;
				newCourse.Teacher = account.Login;
				newCourse.CreateDate = newCourse.UpdateDate = DateTime.Now.ToShortDateString();
				_WindowVM.ChangePageCommand.Execute(obj);
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
			CurrentFrame.CourseItems.Clear();
		}
		#endregion

		public CourseConstructorViewModel(BaseViewModel windowVM)
		{
			_WindowVM = windowVM;

			_CourseFrames = new ObservableCollection<CourseFrame>();
			CurrentFrame = new CourseFrame() { Number = 0, Type = CourseFrameType.Theory };
			FrameType = null;

			_CourseFrames.Add(CurrentFrame);

			AddImageCommand = new RelayCommand(DoAddImage);
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
