using FreeEDU.Core;
using FreeEDU.FreeEDU_Service;
using FreeEDU.Model;
using FreeEDU.Model.Course;
using FreeEDU.View;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FreeEDU.ViewModel
{
	class AccountViewModel:IViewModel
	{
		public Account Account { get; set; }

		private BaseViewModel _WindowVM { get; set; }

		#region ExitCommand
		public RelayCommand ExitCommand { get; set; }

		private void DoExit(object obj)
		{
			Account.Init(new string[] { null, null, null });
			LoginWindow loginWindow = new LoginWindow();
			_WindowVM.CloseWindowCommand.Execute(null);
			loginWindow.Show();
		}
		#endregion

		#region RemoveUserCommand
		public RelayCommand RemoveUserCommand { get; set; }

		private async void DoRemoveUser(object obj)
		{
			try
			{
				using (FreeEDU_ServiceClient service = new FreeEDU_ServiceClient())
				{
					await service.RemoveAccountAsync(Account.GetAccount().Login);
				}
			}
			catch
			{
				_WindowVM.ErrorMsg = "Server isn't responding";
				Account.GetAccount().Init(new string[] { null, null, null });
				LoginWindow login = new LoginWindow();
				_WindowVM.CloseWindowCommand.Execute(null);
				login.Show();
				return;
			}
			_WindowVM.ErrorMsg = Account.GetAccount().Login + "was remove";
			Account.GetAccount().Init(new string[] { null, null, null });
			LoginWindow loginWindow = new LoginWindow();
			_WindowVM.CloseWindowCommand.Execute(null);
			loginWindow.Show();
		}
		#endregion

		#region BrowseCommand
		public RelayCommand BrowseCommand { get; set; }

		private async void DoBrowse(object obj)
		{
			byte[] image = null;
			OpenFileDialog file = new OpenFileDialog();
			if (file.ShowDialog() == true)
			{
				using (FileStream stream = new FileStream(file.FileName, FileMode.Open))
				{
					image = new byte[stream.Length];
					stream.Read(image, 0, image.Length);
				}
			}

			try
			{
				using (FreeEDU_ServiceClient service = new FreeEDU_ServiceClient())
				{
					await service.EditAccountAsync(Account.GetAccount().Login, JsonSerializer.Serialize(image));
				}
			}
			catch
			{
				_WindowVM.ErrorMsg = "Server isn't responding";
				Account.GetAccount().Init(new string[] { null, null, null });
				LoginWindow login = new LoginWindow();
				_WindowVM.CloseWindowCommand.Execute(null);
				login.Show();
			}
		}
		#endregion

		#region ToCourseCommand
		public RelayCommand ToCourseCommand { get; set; }

		private void DoCourse(object obj)
		{
			CurrentCourse.SetCurrentCourse((Course)obj);
			_WindowVM.ChangePageCommand.Execute(PageViews.CourseInfo);
		}
		#endregion

		#region DeleteCommand
		public RelayCommand DeleteCommand { get; set; }

		private async void DoDelete(object obj)
		{
			Course currentCourse = obj as Course;

			Account.Courses.Remove(currentCourse);

			using (FreeEDU_ServiceClient service = new FreeEDU_ServiceClient())
			{
				await service.RemoveCourseAsync(currentCourse.Id);
			}
		}
		#endregion

		public AccountViewModel(BaseViewModel windowVM)
		{
			Account = Account.GetAccount();
			Account.UpdateCompletedCourses();

			_WindowVM = windowVM;

			ExitCommand = new RelayCommand(DoExit);
			ToCourseCommand = new RelayCommand(DoCourse);
			DeleteCommand = new RelayCommand(DoDelete);
			RemoveUserCommand = new RelayCommand(DoRemoveUser);
			BrowseCommand = new RelayCommand(DoBrowse);
		}
	}
}
