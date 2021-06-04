using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FreeEDU.Model
{
	class Comment
	{
		public string Login { get; set; }

		public string Message { get; set; }

		public byte[] ByteImage { get; set; }

		public static Comment Init(string[] data)
		=> new Comment()
		{
			Login = data[0],
			Message = data[1],
			ByteImage = JsonSerializer.Deserialize<byte[]>(data[2]),
		};

		public string[] GetCommentModul(int courseId)
		=> new string[]
			{
				courseId.ToString(),
				Login,
				Message
			};
	}
}
