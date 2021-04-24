using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace FreeEDU_Host
{
	class Program
	{
		static void Main(string[] args)
		{
			using(var host = new ServiceHost(typeof(FreeEDU_Service.FreeEDU_Service)))
			{
				host.Open();
				Console.ReadLine();
			}
		}
	}
}
