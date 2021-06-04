using FreeEDU.Core;
using FreeEDU.FreeEDU_Service;
using FreeEDU.Model;
using FreeEDU.Model.Course;
using FreeEDU.Model.Course.CourseItem;
using FreeEDU.View;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FreeEDU.ViewModel
{
	class MainWindowViewModel:BaseViewModel
	{
		private int _menuWeidth;
		public int MenuWeidth
		{
			get => _menuWeidth;
			set
			{
				_menuWeidth = value;
				OnPropertyChanged("MenuWeidth");
			}
		}

		public Account Account { get => Account.GetAccount(); }

		#region ShowOrHideCommand
		public RelayCommand ShowOrHideCommand { get; set; }

		private void DoShowOrHide(object obj)
		{
			MenuWeidth = (MenuWeidth == 0) ? 200 : 0;
		}
		#endregion

		#region AlertCommand
		public RelayCommand AlertCommand { get; set; }

		private async void DoAlert(object obj)
		{
			await Task.Run(() =>
			{
				MailAddress from = new MailAddress("freeuser_000@mail.ru", "FreeEDU");
				MailAddress to = null;
				SmtpClient smtp = new SmtpClient("smtp.mail.ru", 2525);
				smtp.Credentials = new NetworkCredential("freeuser_000@mail.ru", "DvWgd9QRgo9VRDUIk2tJ");
				smtp.EnableSsl = true;

				try
				{
					using (FreeEDU_ServiceClient service = new FreeEDU_ServiceClient())
					{
						foreach (string email in service.GetEmailsAsync().Result)
						{
							to = new MailAddress(email);
							MailMessage m = new MailMessage(from, to);
							m.Subject = "FreeEDU";
							m.Body = "New courses have appeared in FreeEDU. It's time to learn!";
							smtp.Send(m);
						}
					}
				}
				catch
				{
					Account.GetAccount().Init(new string[] { null, null, null });
					LoginWindow loginWindow = new LoginWindow();
					ErrorMsg = "Server isn't responding";
					CloseWindowCommand.Execute(null);
					loginWindow.Show();
					return;
				}

				ErrorMsg = "Alert successful!";
			});
		}
		#endregion

		#region SendRequestCommand
		public RelayCommand SendRequestCommand { get; set; }

		private async void DoSendRequest(object obj)
		{
			try
			{
				using (FreeEDU_ServiceClient service = new FreeEDU_ServiceClient())
				{
					Account account = Account.GetAccount();
					await service.SendRequestAsync(account.Login);
				}
			}
			catch
			{
				Account.GetAccount().Init(new string[] { null, null, null });
				LoginWindow loginWindow = new LoginWindow();
				ErrorMsg = "Server isn't responding";
				CloseWindowCommand.Execute(null);
				loginWindow.Show();
				return;
			}

			ErrorMsg = "Send successful!";
		}
		#endregion

		public MainWindowViewModel()
		{
			MenuWeidth = 200;
			ErrorMsg = "Work space was active. Study time!";
			CurrentPage = new CoursesListViewModel(this);

			ShowOrHideCommand = new RelayCommand(DoShowOrHide);
			AlertCommand = new RelayCommand(DoAlert);
			SendRequestCommand = new RelayCommand(DoSendRequest);
		}
	}
}
