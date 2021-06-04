using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace FreeEDU_Service
{
	// ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IFreeEDU_Service" в коде и файле конфигурации.
	[ServiceContract]
	public interface IFreeEDU_Service
	{
		[OperationContract]
		string[] GetAccount(string email, string hashPass);

		[OperationContract]
		string[] CreateAcount(string login, string Email, string hashPass);

		[OperationContract]
		void CreateCourse(string[] course);

		[OperationContract]
		List<string[]> GetCourses();

		[OperationContract]
		void AddComletedCourses(string login, int courseId);

		[OperationContract]
		List<string[]> GetComletedCourses(string login);

		[OperationContract]
		void CreateComment(string[] comment);

		[OperationContract]
		List<string[]> GetComments(int courseId);

		[OperationContract]
		bool EditAccount(string login, string image);

		[OperationContract]
		void EditCourse(string[] course);

		[OperationContract]
		void RemoveAccount(string login);

		[OperationContract]
		void RemoveCourse(int courseId);

		[OperationContract]
		List<string> GetEmails();

		[OperationContract]
		void SendRequest(string login);

		[OperationContract]
		void AcceptRequest(string login);

		[OperationContract]
		void RejectRequest(string login);

		[OperationContract]
		List<string[]> GetRequests();

		[OperationContract]
		void ChangeRole(string[] account);

		[OperationContract]
		List<string[]> GetAccounts();
	}
}
