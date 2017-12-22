using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace p.Log
{
	internal class ConsoleLog : ILog
	{
		public void Log(string value, params object[] args)
		{
			Console.WriteLine(value, args);
		}
	}
}
