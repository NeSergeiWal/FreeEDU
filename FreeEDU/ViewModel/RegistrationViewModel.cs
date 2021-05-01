using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeEDU.ViewModel
{
	class RegistrationViewModel:IViewModel
	{
		private LoginWindowViewModel _WindowVM { get; set; }

		public RegistrationViewModel(BaseViewModel baseView)
		{
			_WindowVM = (LoginWindowViewModel)baseView;
		}
	}
}
