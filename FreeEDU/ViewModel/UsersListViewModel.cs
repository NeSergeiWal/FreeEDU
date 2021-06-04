using FreeEDU.Core;
using FreeEDU.FreeEDU_Service;
using FreeEDU.Model;
using FreeEDU.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FreeEDU.ViewModel
{
	class UsersListViewModel : ObservableObject, IViewModel
	{
		public ObservableCollection<User> Users { get; set; }

		private BaseViewModel _WindowVM { get; set; }

		private string searchMsg;

		public string SearchMsg 
		{ 
			get => searchMsg;
			set
			{
				searchMsg = value;
				OnPropertyChanged("SearchMsg");
			}
		}

		private List<User> _Users { get; set; }

		#region RemoveUserCommand
		public RelayCommand RemoveUserCommand { get; set; }

		private async void DoRemoveUser(object obj)
		{
			User user = obj as User;
			Users.Remove(user);
			try
			{
				using (FreeEDU_ServiceClient service = new FreeEDU_ServiceClient())
				{
					await service.RemoveAccountAsync(user.Login);
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
			_WindowVM.ErrorMsg = user.Login + "was remove";
		}
		#endregion

		#region ChangeRoleCommand
		public RelayCommand ChangeRoleCommand { get; set; }

		private async void DoChangeRole(object obj)
		{
			User user = obj as User;

			try
			{
				using (FreeEDU_ServiceClient service = new FreeEDU_ServiceClient())
				{
					await service.ChangeRoleAsync(user.GetUserModul());
				}
			}
			catch
			{
				Account.GetAccount().Init(new string[] { null, null, null });
				LoginWindow loginWindow = new LoginWindow();
				_WindowVM.ErrorMsg = "Server isn't responding";
				_WindowVM.CloseWindowCommand.Execute(null);
				loginWindow.Show();
			}

			_WindowVM.ErrorMsg = "Edit successeful";
		}
		#endregion

		#region SearchCommand
		public RelayCommand SearchCommand { get; set; }

		private void DoSearch(object obj)
		{
			Users.Clear();

			foreach (User user in _Users)
			{
				if (Regex.IsMatch(user.Login, @"\w*" + SearchMsg + @"\w*", RegexOptions.IgnoreCase))
				{
					Users.Add(user);
				}
			}

			SearchMsg = "";
		}
		#endregion

		#region UpFilterCommand
		public RelayCommand UpFilterCommand { get; set; }

		private void DoUpFilter(object obj)
		{
			List<User> temp = new List<User>();
			temp = Users.OrderBy(us => us.Login).ToList();
			Users.Clear();

			foreach (User user in temp)
			{
				Users.Add(user);
			}
		}
		#endregion

		#region DownFilterCommand
		public RelayCommand DownFilterCommand { get; set; }

		private void DoDownFilter(object obj)
		{
			List<User> temp = new List<User>();
			temp = Users.OrderByDescending(us => us.Login).ToList();
			Users.Clear();

			foreach (User user in temp)
			{
				Users.Add(user);
			}
		}
		#endregion

		private void FillUsersCollection()
		{
			try
			{
				using (FreeEDU_ServiceClient service = new FreeEDU_ServiceClient())
				{
					foreach (string[] user in service.GetAccountsAsync().Result)
					{
						Users.Add(User.Init(user));
						_Users.Add(Users.Last());
					}
				}
			}
			catch
			{
				Account.GetAccount().Init(new string[] { null, null, null });
				LoginWindow loginWindow = new LoginWindow();
				_WindowVM.ErrorMsg = "Server isn't responding";
				_WindowVM.CloseWindowCommand.Execute(null);
				loginWindow.Show();
			}
		}

		public UsersListViewModel(BaseViewModel window)
		{
			Users = new ObservableCollection<User>();
			_Users = new List<User>();

			_WindowVM = window;

			RemoveUserCommand = new RelayCommand(DoRemoveUser);
			ChangeRoleCommand = new RelayCommand(DoChangeRole);
			SearchCommand = new RelayCommand(DoSearch);
			UpFilterCommand = new RelayCommand(DoUpFilter);
			DownFilterCommand = new RelayCommand(DoDownFilter);


			FillUsersCollection();
		}
	}
}
