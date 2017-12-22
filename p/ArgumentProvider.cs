using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace p
{
	public class ArgumentProvider
	{
		public ArgumentProvider(params string[] args)
		{
			Arguments = args == null ? "" : string.Join(" ", args);
		}

		public string Arguments { get; }
	}
}
