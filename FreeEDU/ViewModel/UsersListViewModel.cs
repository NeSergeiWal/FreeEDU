using FreeEDU.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeEDU.ViewModel
{
	class UsersListViewModel:IViewModel
	{
		public ObservableCollection<User> Users { get; set; }

		public UsersListViewModel()
		{
			Users = new ObservableCollection<User>();
			Users.Add(new User() { Login = "User1", Email = "qwe@mail.ru", Role = Roles.Student });
			Users.Add(new User() { Login = "User2", Email = "qwe@mail.ru", Role = Roles.Teacher });
			Users.Add(new User() { Login = "User3", Email = "qwe@mail.ru", Role = Roles.Student });
		}
	}
}
