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
using System.Text.RegularExpressions;
using FreeEDU.View;

namespace FreeEDU.ViewModel
{
	class CoursesListViewModel : ObservableObject, IViewModel
	{
		public ObservableCollection<Course> Courses { get; set; }

		private string searchMsg;

		public string SearchMsg
		{
			get => searchMsg;
			set
			{
				searchMsg = value;
				OnPropertyChanged("SearchMsg");
			}
		}

		private List<Course> _Courses { get; set; }

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

		private async void DoDelete(object obj)
		{
			Course currentCourse = obj as Course;

			Courses.Remove(currentCourse);
			try
			{
				using (FreeEDU_ServiceClient service = new FreeEDU_ServiceClient())
				{
					await service.RemoveCourseAsync(currentCourse.Id);
				}
			}
			catch
			{
				Account.GetAccount().Init(new string[] { null, null, null });
				LoginWindow loginWindow = new LoginWindow();
				_WindowVM.ErrorMsg = "Server isn't responding";
				_WindowVM.CloseWindowCommand.Execute(null);
				loginWindow.Show();
				return;
			}
			_WindowVM.ErrorMsg = "Course was delete";
		}
		#endregion

		#region SearchCommand
		public RelayCommand SearchCommand { get; set; }

		private void DoSearch(object obj)
		{
			Courses.Clear();

			foreach(Course course in _Courses)
			{
				if(Regex.IsMatch(course.Name, @"\w*" + SearchMsg + @"\w*", RegexOptions.IgnoreCase))
				{
					Courses.Add(course);
				}
			}
		}
		#endregion

		#region UpFilterCommand
		public RelayCommand UpFilterCommand { get; set; }

		private void DoUpFilter(object obj)
		{
			List<Course> temp = new List<Course>();
			temp = Courses.OrderBy(c => Convert.ToDateTime(c.UpdateDate)).ToList();
			Courses.Clear();

			foreach(Course course in temp)
			{
				Courses.Add(course);
			}
		}
		#endregion

		#region DownFilterCommand
		public RelayCommand DownFilterCommand { get; set; }

		private void DoDownFilter(object obj)
		{
			List<Course> temp = new List<Course>();
			temp = Courses.OrderByDescending(c => Convert.ToDateTime(c.UpdateDate)).ToList();
			Courses.Clear();

			foreach (Course course in temp)
			{
				Courses.Add(course);
			}
		}
		#endregion

		private void FillCourses()
		{
			try
			{
				using (FreeEDU_ServiceClient service = new FreeEDU_ServiceClient())
				{
					foreach (string[] course in service.GetCoursesAsync().Result)
					{
						Courses.Add(Course.Init(course));
						_Courses.Add(Courses.Last());
					}
				}
			}
			catch
			{
				Account.GetAccount().Init(new string[] { null, null, null });
				LoginWindow loginWindow = new LoginWindow();
				_WindowVM.ErrorMsg = "Server isn't responding";
				_WindowVM.CloseWindowCommand.Execute(null);
				loginWindow.Show();
			}
		}

		public CoursesListViewModel(BaseViewModel windowVM)
		{
			_WindowVM = windowVM;

			Courses = new ObservableCollection<Course>();
			_Courses = new List<Course>();

			ToCourseCommand = new RelayCommand(DoCourse);
			DeleteCommand = new RelayCommand(DoDelete);
			SearchCommand = new RelayCommand(DoSearch);
			UpFilterCommand = new RelayCommand(DoUpFilter);
			DownFilterCommand = new RelayCommand(DoDownFilter);

			FillCourses();
		}
	}
}
