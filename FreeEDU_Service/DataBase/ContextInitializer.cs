using FreeEDU_Service.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FreeEDU_Service.DataBase
{
	class ContextInitializer : DropCreateDatabaseIfModelChanges<FreeEDU_DB>
	{
        protected override void Seed(FreeEDU_DB db)
        {
            try
            {
                byte[] image;
                using (FileStream stream = new FileStream(@"..\..\..\FreeEDU_Service\Resourses\Icons\DefaultIcon.png", FileMode.Open))
                {
                    image = new byte[stream.Length];
                    stream.Read(image, 0, image.Length);
                }

                db.USERS.Add(new USERS() { Login = "Admin", Email = "freeuser_000@mail.com", HashPass = "ZwsUcorZkCrsujLiL6T2vQ==" });
                db.ACCOUNTS.Add(new ACCOUNTS() { Role = "adm", Img_Url = FileManager.CreateJsonFile(@"Users\Admin.json", JsonSerializer.Serialize(image)) });
                db.SaveChanges();
            }
            catch(Exception e)
			{
                Console.WriteLine(e.Message);
			}
        }
    }
}
