using FreeEDU.Core;
using FreeEDU.Model;
using FreeEDU.Model.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeEDU.ViewModel
{
	class CourseInfoViewModel:IViewModel
	{
		public Course Course { get; set; }

		private BaseViewModel _WindowVM { get; set; }

		#region ChangePageCommand
		public RelayCommand ChangePageCommand { get; set; }

		private void DoChangePage(object obj)
			=> _WindowVM.ChangePageCommand.Execute(obj);
		#endregion

		public CourseInfoViewModel(BaseViewModel windowVM)
		{
			Course = CurrentCourse.GetCurrentCourse();

			_WindowVM = windowVM;

			ChangePageCommand = new RelayCommand(DoChangePage);
		}
	}
}
