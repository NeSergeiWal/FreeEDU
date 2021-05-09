using FreeEDU.Core;
using FreeEDU.Model;
using FreeEDU.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeEDU.ViewModel
{
	class AccountViewModel:IViewModel
	{
		public Account Account { get; set; }

		private BaseViewModel _WindowVM { get; set; }

		#region
		public RelayCommand ExitCommand { get; set; }

		private void DoExit(object obj)
		{
			Account.FullSetProps(null, null, null);
			LoginWindow loginWindow = new LoginWindow();
			_WindowVM.CloseWindowCommand.Execute(null);
			loginWindow.Show();
		}
		#endregion

		public AccountViewModel(BaseViewModel windowVM)
		{
			Account = Account.GetAccount();

			_WindowVM = windowVM;

			ExitCommand = new RelayCommand(DoExit);
		}
	}
}
