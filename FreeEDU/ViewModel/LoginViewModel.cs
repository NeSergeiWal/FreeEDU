using FreeEDU.FreeEDU_Service;
using FreeEDU.Core;
using FreeEDU.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using FreeEDU.Model;
using System.ComponentModel.DataAnnotations;
using FreeEDU.Core.Validators;
using FreeEDU.Model.Course;
using System.Text.Json;

namespace FreeEDU.ViewModel
{
	class LoginViewModel:IViewModel
	{
		[Required(ErrorMessage = "Please enter e-mail or login")]
		[StringLength(100, ErrorMessage = "Min lenght 3 symbols", MinimumLength = 3)]
		public string Email { get; set; }

		[Required(ErrorMessage = "Please enter password")]
		[StringLength(12, ErrorMessage = "Password 6-12 symbols", MinimumLength = 6)]
		public string Pass { get; set; }

		private LoginWindowViewModel _WindowVM { get; set; }

		#region LoginCommand
		public RelayCommand LoginCommand { get; set; }

		private void DoLogin(object obj)
		{
			string error = CustomValidator.Validate(this);
			if (error != null)
			{
				_WindowVM.ErrorMsg = error;
				return;
			}

			string[] data = null;
			try
			{
				using (FreeEDU_ServiceClient service = new FreeEDU_ServiceClient())
				{
					data = service.GetAccount(Email, MD5Hasher.GetHash(Pass));
				}
			}
			catch
			{
				_WindowVM.ErrorMsg = "Server isn't responding";
				_WindowVM.ChangePageCommand.Execute(PageViews.Login);
				return;
			}

			if (data == null)
			{
				_WindowVM.ErrorMsg = "Account not found";
				return;
			}

			Account account = Account.GetAccount();
			account.Init(data);

			MainWindow mainWindow = new MainWindow();
			_WindowVM.CloseWindowCommand.Execute(null);
			mainWindow.Show();
		}
		#endregion

		#region RegistrationPageCommand
		public RelayCommand RegistrationPageCommand { get; set; }

		private void DoRegistrationPage(object obj)
		{
			_WindowVM.CurrentWidth = 525;
			_WindowVM.ErrorMsg = "Registration form";
			_WindowVM.ChangePageCommand.Execute(obj);
		}
		#endregion

		public LoginViewModel(BaseViewModel baseView)
		{
			_WindowVM = (LoginWindowViewModel)baseView;
			LoginCommand = new RelayCommand(DoLogin);
			RegistrationPageCommand = new RelayCommand(DoRegistrationPage);
		}
	}
}
