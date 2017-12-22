using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace p.Commands
{
	public abstract class Command
	{
		public abstract char Prefix { get; }

		public abstract bool Execute(string processName);
	}
}
