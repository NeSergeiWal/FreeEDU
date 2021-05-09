using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data.SqlClient;
using System.IO;

namespace FreeEDU_Service
{
	// ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "FreeEDU_Service" в коде и файле конфигурации.
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
	public class FreeEDU_Service : IFreeEDU_Service
	{
		public (string, string, string) GetAccount(string email, string hashPass)
		{
			try
			{
				using(FreeEDU_DB db = new FreeEDU_DB())
				{
					USERS user = db.USERS.FirstOrDefault(us => us.Email == email && us.HashPass == hashPass);

					if (user != default(USERS))
					{
						string json = null;

						ACCOUNTS account = db.ACCOUNTS.First(acc => acc.Login == user.Login);
						using(StreamReader file = new StreamReader(account.Url))
						{
							json = file.ReadToEnd();
						}
						return (account.Login, account.Role, json);
					}
				}
			}
			catch { }
			
			return (null, null, null);
		}

		public (string, string) CreateAcount(string login, string email, string hashPass)
		{
			try
			{
				using (FreeEDU_DB db = new FreeEDU_DB())
				{
					string jsonPath = @"Users_courses\" + login + ".json";
					File.Create(jsonPath);
					db.USERS.Add(new USERS() { Login = login, Email = email, HashPass = hashPass });
					db.ACCOUNTS.Add(new ACCOUNTS() { Login = login, Role = "stn", Url = jsonPath });
					db.SaveChanges();
					ACCOUNTS account = db.ACCOUNTS.First(a => a.Login == login);
					return (account.Login, account.Role);
				}
			}
			catch { }

			return (null, null);
		}

		public string GetCourses()
		{

			return null;
		}

	}
}
