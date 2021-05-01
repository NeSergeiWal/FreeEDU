using FreeEDU.Core;
using FreeEDU.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeEDU.ViewModel
{
	class LoginViewModel:IViewModel
	{
		private LoginWindowViewModel _WindowViewModel { get; set; }

		#region LoginCommand
		public RelayCommand LoginCommand { get; set; }

		private void DoLogin(object obj)
		{
			MainWindow mainWindow = new MainWindow();
			_WindowViewModel.CloseWindowCommand.Execute(null);
			mainWindow.Show();
		}
		#endregion

		#region RegistrationPageCommand
		public RelayCommand RegistrationPageCommand { get; set; }

		private void DoRegistrationPage(object obj)
		{
			_WindowViewModel.CurrentWidth = 525;
			_WindowViewModel.ChangePageCommand.Execute(obj);
		}
		#endregion

		public LoginViewModel(BaseViewModel baseView)
		{
			_WindowViewModel = (LoginWindowViewModel)baseView;

			LoginCommand = new RelayCommand(DoLogin);
			RegistrationPageCommand = new RelayCommand(DoRegistrationPage);
		}
	}
}
