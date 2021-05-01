using FreeEDU.Core;
using FreeEDU.Core.Converters;
using FreeEDU.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FreeEDU.ViewModel
{
	public abstract class BaseViewModel : ObservableObject, IViewModel
	{
		private IViewModel _currentPage;
		public IViewModel CurrentPage 
		{
			get => _currentPage;
			set
			{
				_currentPage = value;
				OnPropertyChanged("CurrentPage");
			}
		}

		#region CloseAppCommand
		public RelayCommand CloseAppCommand { get; set; }
		
		private void DoCloseApp(object obj) => Application.Current.Shutdown();
		#endregion
		
		#region CloseWindowCommand
		public RelayCommand CloseWindowCommand { get; set; }
		
		private void DoCloseWindow(object obj)
		{
			foreach (Window window in Application.Current.Windows)
			{
				if(window.IsActive)
				{
					window.Close();
					return;
				}
			}
		}
		#endregion

		#region MinimazeWindowCommand
		public RelayCommand MinimazeWindowCommand { get; set; }

		private void DoMinimazeWindow(object obj)
		{
			foreach (Window window in Application.Current.Windows)
			{
				if(window.IsActive)
				{
					window.WindowState = (window.WindowState == WindowState.Minimized) ? WindowState.Normal : WindowState.Minimized;
					return;
				}
			}
		}
		#endregion

		#region MaximazeWindowCommand
		public RelayCommand MaximazeWindowCommand { get; set; }

		private void DoMaximazeWindow(object obj)
		{
			foreach (Window window in Application.Current.Windows)
			{
				if (window.IsActive)
				{
					window.WindowState = 
						(window.WindowState == WindowState.Maximized) ? WindowState.Normal : WindowState.Maximized;
					return;
				}
			}
		}
		#endregion
		
		#region ChangePageCommand
		public RelayCommand ChangePageCommand { get; set; }

		private void DoChangePage(object obj)
			=> CurrentPage = ViewModelConverter.GetViewModel(this, (PageViews)obj);
		#endregion

		public BaseViewModel()
		{
			CloseAppCommand = new RelayCommand(DoCloseApp);
			CloseWindowCommand = new RelayCommand(DoCloseWindow);
			MinimazeWindowCommand = new RelayCommand(DoMinimazeWindow);
			MaximazeWindowCommand = new RelayCommand(DoMaximazeWindow);
			ChangePageCommand = new RelayCommand(DoChangePage);
		}
	}
}
