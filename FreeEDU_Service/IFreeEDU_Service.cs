using FreeEDU_Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace FreeEDU_Service
{
	// ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IFreeEDU_Service" в коде и файле конфигурации.
	[ServiceContract]
	public interface IFreeEDU_Service
	{
		[OperationContract]
		string GetAccount(string login, string hashPassword);

		[OperationContract]
		Account CreateAcount()
	}
}
