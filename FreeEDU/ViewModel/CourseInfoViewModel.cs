using FreeEDU.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeEDU.ViewModel
{
	class CourseInfoViewModel:IViewModel
	{
		private BaseViewModel _WindowVM { get; set; }

		#region StartCommand
		public RelayCommand StartCommand { get; set; }

		private void DoStart(object obj)
		{
			_WindowVM.ChangePageCommand.Execute(obj);
		}
		#endregion

		public CourseInfoViewModel(BaseViewModel windowVM)
		{
			_WindowVM = windowVM;

			StartCommand = new RelayCommand(DoStart);
		}
	}
}
