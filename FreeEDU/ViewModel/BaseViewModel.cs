using FreeEDU.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FreeEDU.ViewModel
{
	class BaseViewModel : ObservableObject
	{
		private string _winName;

		#region CloseWindowCommand
		public RelayCommand CloseWindowCommand { get; set; }
		
		private void DoCloseWindow(object obj)
		{
			foreach (var window in Application.Current.Windows)
			{
				if(window.ToString() == _winName)
				{
					Window win = (Window)window;
					win.Close();
				}
			}
		}
		#endregion

		#region MinimazeWindowCommand
		public RelayCommand MinimazeWindowCommand { get; set; }

		private void DoMinimazeWindow(object obj)
		{
			foreach (var window in Application.Current.Windows)
			{
				if(window.ToString() == _winName)
				{
					Window win = (Window)window;
					win.WindowState = (win.WindowState == WindowState.Minimized) ? WindowState.Normal : WindowState.Minimized;
				}
			}
		}
		#endregion

		#region MaximazeWindowCommand
		public RelayCommand MaximazeWindowCommand { get; set; }

		private void DoMaximazeWindow(object obj)
		{
			foreach (var window in Application.Current.Windows)
			{
				if (window.ToString() == _winName)
				{
					Window win = (Window)window;
					win.WindowState = (win.WindowState == WindowState.Maximized) ? WindowState.Normal : WindowState.Maximized;
				}
			}
		}
		#endregion

		public BaseViewModel(string winName)
		{
			_winName = winName;

			CloseWindowCommand = new RelayCommand(DoCloseWindow);
			MinimazeWindowCommand = new RelayCommand(DoMinimazeWindow);
			MaximazeWindowCommand = new RelayCommand(DoMaximazeWindow);
		}
	}
}
