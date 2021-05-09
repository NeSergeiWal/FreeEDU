using System;
using System.Security.Cryptography;
using System.Text;

namespace FreeEDU.Core
{
	static class MD5Hasher
	{ 
		public static string GetHash(string obj)
		{
			MD5 hasher = MD5.Create();
			return Convert.ToBase64String(hasher.ComputeHash(Encoding.UTF8.GetBytes(obj)));
		}
	}
}
