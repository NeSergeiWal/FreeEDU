using FreeEDU.Core;
using FreeEDU.FreeEDU_Service;
using FreeEDU.Model;
using FreeEDU.Model.Course;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeEDU.ViewModel
{
	class CourseInfoViewModel : ObservableObject, IViewModel
	{
		private string commentMsg;

		public Course Course { get; set; }

		public string CommentMsg 
		{ 
			get => commentMsg;
			set
			{
				commentMsg = value;
				OnPropertyChanged("CommentMsg");
			}
		}

		public ObservableCollection<Comment> Comments { get; set; }

		private BaseViewModel _WindowVM { get; set; }

		#region ChangePageCommand
		public RelayCommand ChangePageCommand { get; set; }

		private void DoChangePage(object obj)
			=> _WindowVM.ChangePageCommand.Execute(obj);
		#endregion

		#region AddCommentCommand
		public RelayCommand AddCommentCommand { get; set; }

		private async void DoAddComment(object obj)
		{
			if (CommentMsg != string.Empty)
			{
				Account account = Account.GetAccount();

				Comment newComment = new Comment()
				{
					Login = account.Login,
					ByteImage = account.ByteImage,
					Message = CommentMsg
				};

				using (FreeEDU_ServiceClient service = new FreeEDU_ServiceClient())
				{
					await service.CreateCommentAsync(newComment.GetCommentModul(Course.Id));
				}

				Comments.Add(newComment);

				CommentMsg = "";
			}
		}
		#endregion

		private void FillCommentsCollection()
		{
			using (FreeEDU_ServiceClient service = new FreeEDU_ServiceClient())
			{
				foreach (string[] comment in service.GetCommentsAsync(Course.Id).Result)
				{
					Comments.Add(Comment.Init(comment));
				}
			}
		}

		public CourseInfoViewModel(BaseViewModel windowVM)
		{
			Course = CurrentCourse.GetCurrentCourse();
			Comments = new ObservableCollection<Comment>();
			_WindowVM = windowVM;

			ChangePageCommand = new RelayCommand(DoChangePage);
			AddCommentCommand = new RelayCommand(DoAddComment);

			FillCommentsCollection();
		}
	}
}
