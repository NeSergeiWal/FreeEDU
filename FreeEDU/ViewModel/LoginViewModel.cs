using FreeEDU.Core;
using FreeEDU.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeEDU.ViewModel
{
	class LoginViewModel:BaseViewModel
	{
		#region LoginCommand
		public RelayCommand LoginCommand { get; set; }

		private void DoLogin(object obj)
		{
			MainWindow mainWindow = new MainWindow();
			mainWindow.Show();
			CloseWindowCommand.Execute(null);
		}
		#endregion

		#region RegistrationCommand
		public RelayCommand RegistrationCommand { get; set; }

		private void DoRegistration(object obj)
		{
			RegistrationWindow registrationWindow = new RegistrationWindow();
			registrationWindow.Show();
			CloseWindowCommand.Execute(null);
		}
		#endregion

		public LoginViewModel():base("FreeEDU.View.LoginWindow")
		{
			LoginCommand = new RelayCommand(DoLogin);
			RegistrationCommand = new RelayCommand(DoRegistration);
		}
	}
}
