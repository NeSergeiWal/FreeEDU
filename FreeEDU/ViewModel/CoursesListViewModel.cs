using FreeEDU.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeEDU.ViewModel
{
	public class CoursesListViewModel : IViewModel
	{
		private BaseViewModel _WindowVM { get; set; }

		#region ToCourseCommand
		public RelayCommand ToCourseCommand { get; set; }
		#endregion

		public CoursesListViewModel(BaseViewModel windowVM)
		{
			_WindowVM = windowVM;

			ToCourseCommand = new RelayCommand(o => _WindowVM.ChangePageCommand.Execute(o));
		}
	}
}
