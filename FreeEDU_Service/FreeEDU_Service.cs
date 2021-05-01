using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data.SqlClient;
using FreeEDU_Service.Model;

namespace FreeEDU_Service
{
	// ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "FreeEDU_Service" в коде и файле конфигурации.
	[ServiceBehavior]
	public class FreeEDU_Service : IFreeEDU_Service
	{
		private const string connectionString = "Server=WIN-GVMOUGER3HF;Database=FreeEDU;Trusted_Connection=True;";
		private static SqlConnection Connection { get; set; }
		private SqlCommand Command { get; set; }
		private SqlDataReader Reader { get; set; }

		public string GetAccount(string login, string hashPassword)
		{
			//Account account = new Account();

			//Command.CommandText = $"SELECT top(1) Id FROM USERS WHERE Login={login} AND HashPassword={hashPassword}";
			//Reader = Command.ExecuteReader();
			//if (!Reader.HasRows)
			//{
			//	return null;
			//}
			//Reader.Read();

			//Command.CommandText = $"SELECT top(1) Id, Nikname, E-mail, Role FROM Accounts WHERE User_id={Reader.GetInt32(0)}";
			//Reader = Command.ExecuteReader();
			//Reader.Read();

			//return new Account()
			//{
			//	Id = Reader.GetInt32(0),
			//	Nikname = Reader.GetString(1),
			//	Email = Reader.GetString(2),
			//	Role = RoleConverter.GetRoles(Reader.GetInt32(3)),
			//	UserId = Reader.GetInt32(4)
			//};
			string str = null;
			using(FreeEDU_DB db = new FreeEDU_DB())
			{
				str = db.Table_2.FirstOrDefault().Text;
			}
			return str;
		}

		static FreeEDU_Service()
		{
			Connection = new SqlConnection(connectionString);
			Connection.Open();
			if(!(Connection.State == System.Data.ConnectionState.Open))
			{ }
		}

		public FreeEDU_Service()
		{
			Command = new SqlCommand();
			Command.Connection = Connection;
		}
	}
}
