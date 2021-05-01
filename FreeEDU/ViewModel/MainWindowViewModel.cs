using FreeEDU.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeEDU.ViewModel
{
	class MainWindowViewModel:BaseViewModel
	{
		private int _menuWeidth;
		public int MenuWeidth
		{
			get => _menuWeidth;
			set
			{
				_menuWeidth = value;
				OnPropertyChanged("MenuWeidth");
			}
		}

		#region ShowOrHideCommand
		public RelayCommand ShowOrHideCommand { get; set; }

		private void DoShowOrHide(object obj)
		{
			MenuWeidth = (MenuWeidth == 0) ? 200 : 0;
		}
		#endregion

		public MainWindowViewModel()
		{
			MenuWeidth = 200;

			CurrentPage = new CoursesListViewModel(this);

			ShowOrHideCommand = new RelayCommand(DoShowOrHide);
		}
	}
}
