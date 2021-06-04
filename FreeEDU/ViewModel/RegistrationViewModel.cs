using FreeEDU.FreeEDU_Service;
using FreeEDU.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using FreeEDU.Model;
using System.ComponentModel.DataAnnotations;
using FreeEDU.Core.Validators;
using FreeEDU.Core.Converters;

namespace FreeEDU.ViewModel
{
	class RegistrationViewModel: ObservableObject, IViewModel
	{
		[Required(ErrorMessage ="Please enter login")]
		[StringLength(25, ErrorMessage = "Login 3-25 symbols", MinimumLength=3)]
		public string Login { get; set; }

		[Required(ErrorMessage = "Please enter e-mail")]
		[EmailAddress(ErrorMessage ="Enter correct E-mail")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Please enter password")]
		[StringLength(12, ErrorMessage = "Password 6-12 symbols", MinimumLength = 6)]
		public string Pass { get; set; }

		[Required(ErrorMessage = "Please reqeat password")]
		[Compare("Pass", ErrorMessage = "Passwords didn't match")]
		public string RepPass { get; set; }

		private LoginWindowViewModel _WindowVM { get; set; }

		#region Registrationcommand
		public RelayCommand RegistrationCommand { get; set; }

		private void DoRegistration(object obj)
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
					data = service.CreateAcount(Login, Email, MD5Hasher.GetHash(Pass));
				}
			}
			catch
			{
				_WindowVM.ErrorMsg = "Server isn't responding";
				_WindowVM.ChangePageCommand.Execute(PageViews.Login);
				return;
			}

			if(data == null)
			{
				_WindowVM.ErrorMsg = "Login is already taken";
				return;
			}
			else
			{
				Account account = Account.GetAccount();
				account.Init(data);
				MainWindow mainWindow = new MainWindow();
				_WindowVM.CloseWindowCommand.Execute(null);
				mainWindow.Show();
			}
		}
		#endregion

		#region
		public RelayCommand CanselCommand { get; set; }

		private void DoCansel(object obj)
		{
			_WindowVM.CurrentWidth = 300;
			_WindowVM.ChangePageCommand.Execute(obj);
		}
		#endregion

		public RegistrationViewModel(BaseViewModel baseView)
		{
			_WindowVM = (LoginWindowViewModel)baseView;

			RegistrationCommand = new RelayCommand(DoRegistration);
			CanselCommand = new RelayCommand(DoCansel);
			_WindowVM.ErrorMsg = "";
		}
	}
}
