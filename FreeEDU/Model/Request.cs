using FreeEDU.Core.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace FreeEDU.Model
{
	class Request
	{
		public string Login { get; set; }

		public string Email { get; set; }

		public byte[] ByteImage { get; set; }

		public static Request Init(string[] data)
		=> new Request()
			{
				 Login = data[0],
				 Email = data[1],
				 ByteImage = JsonSerializer.Deserialize<byte[]>(data[2])
			};
	}
}
