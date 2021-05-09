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

namespace FreeEDU.ViewModel
{
	class LoginViewModel:IViewModel
	{
		[Required(ErrorMessage = "Please enter e-mail")]
		[EmailAddress(ErrorMessage = "Enter correct E-mail")]
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

			FreeEDU_ServiceClient service = new FreeEDU_ServiceClient();
			var data = service.GetAccount(Email, MD5Hasher.GetHash(Pass));

			if (data.Item1 == null)
			{
				_WindowVM.ErrorMsg = "Make sure to enter the correct e-mail and password";
				return;
			}

			Account account = Account.GetAccount();
			account.FullSetProps(data.Item1, data.Item2, data.Item3);

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
