using FreeEDU.Core;
using FreeEDU.FreeEDU_Service;
using FreeEDU.Model;
using FreeEDU.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FreeEDU.ViewModel
{
	class RequestsViewModel : ObservableObject, IViewModel
	{
		public ObservableCollection<Request> Requests { get; set; }

		private BaseViewModel _WindowVM { get; set; }

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

		private List<Request> _Requests { get; set; }

		#region AcceptCommand
		public RelayCommand AcceptCommand { get; set; }

		private async void DoAccept(object obj)
		{
			Request request = (Request)obj;
			Requests.Remove(request);

			try
			{
				using (FreeEDU_ServiceClient service = new FreeEDU_ServiceClient())
				{
					await service.AcceptRequestAsync(request.Login);
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

			_WindowVM.ErrorMsg = "Requests was accept";
		}
		#endregion
		
		#region RejectCommand
		public RelayCommand RejectCommand { get; set; }

		private async void DoReject(object obj)
		{
			Request request = (Request)obj;
			Requests.Remove(request);
			try
			{
				using (FreeEDU_ServiceClient service = new FreeEDU_ServiceClient())
				{
					await service.RejectRequestAsync(request.Login);
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
			_WindowVM.ErrorMsg = "Requests was reject";
		}
		#endregion

		#region SearchCommand
		public RelayCommand SearchCommand { get; set; }

		private void DoSearch(object obj)
		{
			Requests.Clear();

			foreach (Request request in _Requests)
			{
				if (Regex.IsMatch(request.Login, @"\w*" + SearchMsg + @"\w*", RegexOptions.IgnoreCase))
				{
					Requests.Add(request);
				}
			}
		}
		#endregion

		#region UpFilterCommand
		public RelayCommand UpFilterCommand { get; set; }

		private void DoUpFilter(object obj)
		{
			List<Request> temp = new List<Request>();
			temp = Requests.OrderBy(req => req.Login).ToList();
			Requests.Clear();

			foreach(Request request in temp)
			{
				Requests.Add(request);
			}
		}
		#endregion

		#region DownFilterCommand
		public RelayCommand DownFilterCommand { get; set; }

		private void DoDownFilter(object obj)
		{
			List<Request> temp = new List<Request>();
			temp = Requests.OrderByDescending(req => req.Login).ToList();
			Requests.Clear();

			foreach (Request request in temp)
			{
				Requests.Add(request);
			}
		}
		#endregion

		private void FillRequestCollection()
		{
			try
			{
				using (FreeEDU_ServiceClient service = new FreeEDU_ServiceClient())
				{
					foreach (string[] request in service.GetRequestsAsync().Result)
					{
						Requests.Add(Request.Init(request));
						_Requests.Add(Requests.Last());
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

		public RequestsViewModel(BaseViewModel window)
		{
			Requests = new ObservableCollection<Request>();
			_Requests = new List<Request>();

			_WindowVM = window;

			AcceptCommand = new RelayCommand(DoAccept);
			RejectCommand = new RelayCommand(DoReject);
			SearchCommand = new RelayCommand(DoSearch);
			UpFilterCommand = new RelayCommand(DoUpFilter);
			DownFilterCommand = new RelayCommand(DoDownFilter);

			FillRequestCollection();
		}
	}
}
