using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeEDU_Service.Core
{
	static class FileManager
	{
		public static string CreateJsonFile(string path)
		{
			string dirName = path.Split(new char[] { '\\' }).First();
			if (!Directory.Exists(dirName))
			{
				Directory.CreateDirectory(dirName);
			}

			File.Create(path);

			return path;
		}

		public static string CreateJsonFile(string path, string json)
		{
			string dirName = path.Split(new char[] { '\\' }).First();
			if (!Directory.Exists(dirName))
			{
				Directory.CreateDirectory(dirName);
			}

			using (StreamWriter stream = new StreamWriter(path))
			{
				stream.WriteLine(json);
			}
			return path;
		}

		public static string GetJson(string path)
		{
			using (StreamReader stream = new StreamReader(path))
			{
				return stream.ReadToEnd();
			}
		}

		public static void SetJson(string path, string json)
		{
			using (StreamWriter stream = new StreamWriter(path))
			{
				stream.WriteLine(json);
			}
		}

		public static void DeleteFile(string path)
		{
			if(File.Exists(path))
			{
				File.Delete(path);
			}
		}
	}
}
