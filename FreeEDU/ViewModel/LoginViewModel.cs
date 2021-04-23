using FreeEDU.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeEDU.ViewModel
{
	class LoginViewModel:BaseViewModel
	{
		#region
		public RelayCommand LoginCommand { get; set; }

		private void DoLogin(object obj)
		{
			MainWindow mainWindow = new MainWindow();
			mainWindow.Show();
			CloseWindowCommand.Execute(null);
		}
		#endregion

		public LoginViewModel():base("FreeEDU.View.LoginWindow")
		{
			LoginCommand = new RelayCommand(DoLogin);
		}
	}
}
