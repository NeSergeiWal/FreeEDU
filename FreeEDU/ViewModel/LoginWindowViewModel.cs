using FreeEDU.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeEDU.ViewModel
{
	public class LoginWindowViewModel : BaseViewModel
	{
		private int _currentWidth;
		public int CurrentWidth 
		{
			get => _currentWidth;
			set
			{
				_currentWidth = value;
				OnPropertyChanged("CurrentWidth");
			}
		}

		public LoginWindowViewModel()
		{
			CurrentWidth = 300;
			CurrentPage = new LoginViewModel(this);
		}
	}
}
