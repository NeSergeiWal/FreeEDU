using FreeEDU.FreeEDU_Service;
using FreeEDU.Core;
using FreeEDU.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreeEDU.Model.Course;

namespace FreeEDU.ViewModel
{
	class CoursesListViewModel : ObservableObject, IViewModel
	{
		public static ObservableCollection<Course> Courses { get; set; }

		private BaseViewModel _WindowVM { get; set; }

		#region ToCourseCommand
		public RelayCommand ToCourseCommand { get; set; }

		private void DoCourse(object obj)
		{
			CurrentCourse.SetCurrentCourse((Course)obj);
			_WindowVM.ChangePageCommand.Execute(PageViews.CourseInfo);
		}
		#endregion

		#region DeleteCommand
		public RelayCommand DeleteCommand { get; set; }

		private void DoDelete(object obj)
		{
			Courses.Remove((Course)obj);
		}
		#endregion

		static CoursesListViewModel()
		{
			FreeEDU_ServiceClient service = new FreeEDU_ServiceClient(); 
		}

		public CoursesListViewModel(BaseViewModel windowVM)
		{
			_WindowVM = windowVM;

			ToCourseCommand = new RelayCommand(DoCourse);
			DeleteCommand = new RelayCommand(DoDelete);
		}
	}
}
