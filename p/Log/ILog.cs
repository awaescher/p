using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace p.Log
{
	internal interface ILog
	{
		void Log(string value, params object[] args);
	}
}
