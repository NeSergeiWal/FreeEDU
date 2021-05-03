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

			MD5 hasher = MD5.Create();
			string hashPass = Convert.ToBase64String(hasher.ComputeHash(Encoding.UTF8.GetBytes(Pass)));

			FreeEDU_ServiceClient service = new FreeEDU_ServiceClient();
			var data = service.CreateAcount(Login, Email, hashPass);

			if(data.Item1 == null)
			{
				_WindowVM.ErrorMsg = "Login is already taken";
				return;
			}
			else
			{
				Account account = Account.GetAccount();
				account.FullSetProps(data.Item1, data.Item2);
				MainWindow mainWindow = new MainWindow();
				_WindowVM.CloseWindowCommand.Execute(null);
				mainWindow.Show();
			}
		}
		#endregion

		public RegistrationViewModel(BaseViewModel baseView)
		{
			_WindowVM = (LoginWindowViewModel)baseView;
			Pass = "";
			Login = "Artem";
			RegistrationCommand = new RelayCommand(DoRegistration);
		}
	}
}
