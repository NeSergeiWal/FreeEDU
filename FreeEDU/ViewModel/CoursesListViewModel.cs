using FreeEDU.FreeEDU_Service;
using FreeEDU.Core;
using FreeEDU.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeEDU.ViewModel
{
	class CoursesListViewModel : IViewModel
	{
		public static ObservableCollection<Course> Courses { get; set; }

		private BaseViewModel _WindowVM { get; set; }

		#region ToCourseCommand
		public RelayCommand ToCourseCommand { get; set; }
		#endregion

		static CoursesListViewModel()
		{
			FreeEDU_ServiceClient service = new FreeEDU_ServiceClient(); 
		}

		public CoursesListViewModel(BaseViewModel windowVM)
		{
			_WindowVM = windowVM;

			ToCourseCommand = new RelayCommand(o => _WindowVM.ChangePageCommand.Execute(o));
		}
	}
}
