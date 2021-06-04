using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data.SqlClient;
using System.IO;
using FreeEDU_Service.DataBase;
using System.Threading.Tasks;
using FreeEDU_Service.Core;
using System.Net.Mail;
using System.Net;
using System.Data.Entity.Validation;
using System.Text.Json;

namespace FreeEDU_Service
{
	// ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "FreeEDU_Service" в коде и файле конфигурации.
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
	public class FreeEDU_Service : IFreeEDU_Service
	{
		public string[] GetAccount(string email, string hashPass)
		{
			try
			{
				using(FreeEDU_DB db = new FreeEDU_DB())
				{
					USERS user = db.USERS.FirstOrDefault(us => (us.Email == email || us.Login == email) 
						&& us.HashPass == hashPass);

					if (user != default(USERS))
					{
						ACCOUNTS account = db.ACCOUNTS.First(acc => acc.Id == user.Id);
						return new string[] { user.Login, account.Role, FileManager.GetJson(account.Img_Url) };
					}
				}
			}
			catch { }

			return null;
		}

		public string[] CreateAcount(string login, string email, string hashPass)
		{
			try
			{
				byte[] image;
				using (FileStream stream = new FileStream(@"..\..\..\FreeEDU_Service\Resourses\Icons\DefaultIcon.png", FileMode.Open))
				{
					image = new byte[stream.Length];
					stream.Read(image, 0, image.Length);
				}

				using (FreeEDU_DB db = new FreeEDU_DB())
				{
					USERS newUser = new USERS() { Login = login, Email = email, HashPass = hashPass };
					db.USERS.Add(newUser);
					db.ACCOUNTS.Add(new ACCOUNTS() { Role = "stn", Img_Url = FileManager.CreateJsonFile(@"Users\" + login + ".json", JsonSerializer.Serialize(image)) });
					db.SaveChanges();
					return new string[] { login, "stn", null, FileManager.GetJson(db.ACCOUNTS.Find(newUser.Id).Img_Url) };
				}
			}
			catch(Exception e) { Console.WriteLine(e.Message); }

			return null;
		}

		public void CreateCourse(string[] course)
		{
			try
			{
				using (FreeEDU_DB db = new FreeEDU_DB())
				{
					string login = course[1];
					int teacherId = db.USERS.First(acc => acc.Login == login).Id;
					db.COURSES.Add(new COURSES
					{
						Name = course[0],
						Teacher_Id = teacherId,
						CreateDate = DateTime.Parse(course[2]),
						UpdateDate = DateTime.Parse(course[3]),
						Language = course[4],
						Info = course[5],
						Img_url = FileManager.CreateJsonFile(@"Courses\" + course[0] + "Img.json", course[7]),
						Url = FileManager.CreateJsonFile(@"Courses\" + course[0] + "Content.json", course[6])
					});
					db.SaveChanges();
				}
			}
			catch(Exception e) { Console.WriteLine(e.Message); }
		}

		public List<string[]> GetCourses()
		{
			List<string[]> courses = new List<string[]>();

			using (FreeEDU_DB db = new FreeEDU_DB())
			{
				foreach (COURSES course in db.COURSES)
				{
					courses.Add(new string[]
					{
							course.Id.ToString(),
							course.Name,
							db.USERS.Find(course.Teacher_Id).Login,
							course.CreateDate.ToShortDateString(),
							course.UpdateDate.ToShortDateString(),
							course.Language,
							course.Info,
							FileManager.GetJson(course.Url),
							FileManager.GetJson(course.Img_url)
					});
				}
			}
			
			return courses;
		}

		public void CreateComment(string[] comment)
		{
			using (FreeEDU_DB db = new FreeEDU_DB())
			{
				string login = comment[1], message = comment[2];
				int userId = db.USERS.First(us => us.Login == login).Id;
				db.COMMENTS.Add(new COMMENTS()
				{
					Course_Id = int.Parse(comment[0]),
					User_Id = userId,
					Message = message
				});
				db.SaveChanges();
			}
		}

		public List<string[]> GetComments(int courseId)
		{
			List<string[]> comments = new List<string[]>();
			
			using (FreeEDU_DB db = new FreeEDU_DB())
			{
				foreach (COMMENTS comment in db.COMMENTS.Where(c => c.Course_Id == courseId))
				{
					comments.Add(new string[]
					{
							db.USERS.Find(comment.User_Id).Login,
							comment.Message,
							FileManager.GetJson(db.ACCOUNTS.Find(comment.User_Id).Img_Url)
					});
				}
			}
			
			return comments;
		}

		public bool EditAccount(string login, string image)
		{
			try
			{
				using (FreeEDU_DB db = new FreeEDU_DB())
				{
					int accountId = db.USERS.First(us => us.Login == login).Id;
					FileManager.SetJson(db.ACCOUNTS.Find(accountId).Img_Url, image);
					return true;
				}
			}
			catch { }

			return false;
		}

		public void EditCourse(string[] course)
		{
			try
			{
				using (FreeEDU_DB db = new FreeEDU_DB())
				{
					COURSES currentCourse = db.COURSES.Find(int.Parse(course[0]));
					currentCourse.Name = course[1];
					currentCourse.Language = course[2];
					currentCourse.Info = course[3];
					FileManager.SetJson(currentCourse.Url, course[4]);
					db.SaveChanges();
				}
			}
			catch { }
		}

		public void RemoveAccount(string login)
		{
			try
			{
				using (FreeEDU_DB db = new FreeEDU_DB())
				{
					USERS user = db.USERS.First(us => us.Login == login);
					ACCOUNTS account = db.ACCOUNTS.Find(user.Id);

					foreach(var comment in db.COMMENTS.Where(c=>c.User_Id == user.Id))
					{
						db.COMMENTS.Remove(comment);
					}
					foreach (var request in db.REQUESTS.Where(req => req.Account_Id == user.Id))
					{
						db.REQUESTS.Remove(request);
					}
					foreach (var course in db.COMPLETED.Where(c => c.User_Id == user.Id))
					{
						db.COMPLETED.Remove(course);
					}
					foreach (var course in db.COURSES.Where(c => c.Teacher_Id == user.Id))
					{
						db.COURSES.Remove(course);
					}
					FileManager.DeleteFile(account.Img_Url);
					db.ACCOUNTS.Remove(account);
					db.USERS.Remove(user);
					db.SaveChanges();
				}
			}
			catch(Exception e) { Console.WriteLine(e.Message); }
		}

		public void RemoveCourse(int courseId)
		{
			try
			{
				using (FreeEDU_DB db = new FreeEDU_DB())
				{
					COURSES course = db.COURSES.Find(courseId);
					db.COURSES.Remove(course);
					db.SaveChanges();
				}
			}
			catch { }
		}

		public List<string> GetEmails()
		{
			List<string> emails = new List<string>();
			
			using (FreeEDU_DB db = new FreeEDU_DB())
			{
				foreach(USERS user in db.USERS)
				{
					emails.Add(user.Email);
				}
			}
			
			return emails;
		}

		public void SendRequest(string login)
		{
			using (FreeEDU_DB db = new FreeEDU_DB())
			{
				int accountId = db.USERS.First(us => us.Login == login).Id;

				if (db.REQUESTS.FirstOrDefault(req => req.Account_Id == accountId) == default(REQUESTS))
				{
					db.REQUESTS.Add(new REQUESTS()
					{
						Account_Id = accountId
					});
					db.SaveChanges();

					//MailAddress from = new MailAddress(db.USERS.Find(1).Email, "Administration");
					//SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
					//smtp.Credentials = new NetworkCredential(db.USERS.Find(1).Email, "41+52+41");
					//smtp.EnableSsl = true;

					//MailAddress to = new MailAddress(db.USERS.Find(accountId).Email);

					//MailMessage m = new MailMessage(from, to);
					//m.Subject = "Request to teacher role";
					//m.Body = "<h2>Your request will be reviewed. We will write to you shortly.</h2>";
					//m.IsBodyHtml = true;

					//smtp.Send(m);
				}
			}
		}

		public void AcceptRequest(string login)
		{
			using (FreeEDU_DB db = new FreeEDU_DB())
			{
				int accountId = db.USERS.First(us => us.Login == login).Id;
				REQUESTS request = db.REQUESTS.First(req => req.Account_Id == accountId);

				db.REQUESTS.Remove(request);
				db.ACCOUNTS.Find(accountId).Role = "tch";
				db.SaveChanges();

				//MailAddress from = new MailAddress(db.USERS.Find(1).Email, "Administration");
				//SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
				//smtp.Credentials = new NetworkCredential(db.USERS.Find(1).Email, "41+52+41");
				//smtp.EnableSsl = true;

				//MailAddress to = new MailAddress(db.USERS.Find(accountId).Email);

				//MailMessage m = new MailMessage(from, to);
				//m.Subject = "Request to teacher role";
				//m.Body = "<h2>We accepted your request to teacher role.</h2>";
				//m.IsBodyHtml = true;

				//smtp.Send(m);
			}
		}

		public void RejectRequest(string login)
		{
			using (FreeEDU_DB db = new FreeEDU_DB())
			{
				int accountId = db.USERS.First(us => us.Login == login).Id;
				REQUESTS request = db.REQUESTS.First(req => req.Account_Id == accountId);

				db.REQUESTS.Remove(request);
				db.SaveChanges();

				//MailAddress from = new MailAddress(db.USERS.Find(1).Email, "Administration");
				//SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
				//smtp.Credentials = new NetworkCredential(db.USERS.Find(1).Email, "41+52+41");
				//smtp.EnableSsl = true;

				//MailAddress to = new MailAddress(db.USERS.Find(accountId).Email);

				//MailMessage m = new MailMessage(from, to);
				//m.Subject = "Request to teacher role";
				//m.Body = "<h2>We didn't accept your request to teacher role.</h2>";
				//m.IsBodyHtml = true;

				//smtp.Send(m);
			}
		}

		public void ChangeRole(string[] account)
		{
			using (FreeEDU_DB db = new FreeEDU_DB())
			{
				string login = account[0];
				int accountId = db.USERS.First(us => us.Login == login).Id;
				db.ACCOUNTS.Find(accountId).Role = account[1];
				db.SaveChanges();
			}
		}

		public List<string[]> GetRequests()
		{
			List<string[]> requests = new List<string[]>();

			using (FreeEDU_DB db = new FreeEDU_DB())
			{
				foreach(REQUESTS request in db.REQUESTS)
				{
					requests.Add(new string[]
					{
						db.USERS.Find(request.Account_Id).Login,
						db.USERS.Find(request.Account_Id).Email,
						FileManager.GetJson(db.ACCOUNTS.Find(request.Account_Id).Img_Url)
					});
				}
			}
			
			return requests;
		}

		public List<string[]> GetAccounts()
		{
			List<string[]> accounts = new List<string[]>();

			using (FreeEDU_DB db = new FreeEDU_DB())
			{
				foreach (ACCOUNTS account in db.ACCOUNTS)
				{
					if (account.Role != "adm")
					{
						accounts.Add(new string[]
						{
							db.USERS.Find(account.Id).Login,
							db.USERS.Find(account.Id).Email,
							FileManager.GetJson(account.Img_Url),
							account.Role
						});
					}
				}
			}
			
			return accounts;
		}

		public List<string[]> GetComletedCourses(string login)
		{
			List<string[]> courses = new List<string[]>();

			using (FreeEDU_DB db = new FreeEDU_DB())
			{
				int userId = db.USERS.First(us => us.Login == login).Id;

				foreach (COURSES course in db.COURSES)
				{
					foreach (COMPLETED completeCourse in db.COMPLETED.Where(c => c.User_Id == userId))
					{
						if (course.Id == completeCourse.Course_Id)
						{
							courses.Add(new string[]
							{
							course.Id.ToString(),
							course.Name,
							db.USERS.Find(course.Teacher_Id).Login,
							course.CreateDate.ToShortDateString(),
							course.UpdateDate.ToShortDateString(),
							course.Language,
							course.Info,
							FileManager.GetJson(course.Url),
							FileManager.GetJson(course.Img_url)
							});
							break;
						}
					}
				}
			}
			
			return courses;
		}

		public void AddComletedCourses(string login, int courseId)
		{
			using (FreeEDU_DB db = new FreeEDU_DB())
			{
				int userId = db.USERS.First(us => us.Login == login).Id;

				if (db.COMPLETED.FirstOrDefault(c => c.Course_Id == courseId && c.User_Id == userId) == default(COMPLETED))
				{
					db.COMPLETED.Add(new COMPLETED() { User_Id = userId, Course_Id = courseId });
					db.SaveChanges();
				}
			}
		}
	}
}
