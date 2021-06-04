using FreeEDU.Core;
using FreeEDU.Model;
using FreeEDU.Model.Course;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeEDU.ViewModel
{
	class RegistrationCourseViewModel : IViewModel
	{
		private BaseViewModel _WindowVM { get; set; }

		public Course NewCourse { get; set; }

		#region BrowseCommand
		public RelayCommand BrowseCommand { get; set; }

		private void DoBrowse(object obj)
		{
			OpenFileDialog file = new OpenFileDialog();
			if (file.ShowDialog() == true)
			{
				byte[] image;
				using (FileStream stream = new FileStream(file.FileName, FileMode.Open))
				{
					image = new byte[stream.Length];
					stream.Read(image, 0, image.Length);
				}

				NewCourse.ByteImage = image;
			}
		}
		#endregion

		#region ChangePageCommand
		public RelayCommand ChangePageCommand { get; set; }

		private void DoChangePage(object obj)
		{
			if(obj is PageViews.Constructor)
			{
				if(NewCourse.Info == null || NewCourse.Name == null)
				{
					return;
				}
				CurrentCourse.SetCurrentCourse(NewCourse);
			}
			_WindowVM.ChangePageCommand.Execute(obj);
		}
		#endregion

		public RegistrationCourseViewModel(BaseViewModel windowVM)
		{
			_WindowVM = windowVM;

			NewCourse = new Course();

			BrowseCommand = new RelayCommand(DoBrowse);
			ChangePageCommand = new RelayCommand(DoChangePage);
		}
	}
}
