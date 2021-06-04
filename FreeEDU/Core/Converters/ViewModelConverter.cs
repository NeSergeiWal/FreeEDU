using FreeEDU.Model;
using FreeEDU.View;
using FreeEDU.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeEDU.Core.Converters
{
	public static class ViewModelConverter
	{
		public static IViewModel GetViewModel(BaseViewModel sender, PageViews viewModel)
		{
			switch (viewModel)
			{
				case PageViews.Login: { return new LoginViewModel(sender); }
				case PageViews.Registration: { return new RegistrationViewModel(sender); }
				case PageViews.CourseList: { return new CoursesListViewModel(sender); }
				case PageViews.CourseInfo: { return new CourseInfoViewModel(sender); }
				case PageViews.Course: { return new CourseViewModel(sender); }
				case PageViews.Account: { return new AccountViewModel(sender); }
				case PageViews.UsersList: { return new UsersListViewModel(sender); }
				case PageViews.Constructor: { return new CourseConstructorViewModel(sender); }
				case PageViews.RegistrationCourse: { return new RegistrationCourseViewModel(sender); }
				case PageViews.Requests: { return new RequestsViewModel(sender); }
				default: { return null; }
			}
		}
	}
}
