using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeEDU.ViewModel
{
	class CourseViewModel:IViewModel
	{
		public ObservableCollection<Task> Pages { get; set; }

		public CourseViewModel()
		{

		}
	}
}
