using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace p
{
	internal class AliasMap
	{
		private Dictionary<string, string> _map = new Dictionary<string, string>();

		public static AliasMap Read(string[] mapLines)
		{
			var map = new AliasMap();

			foreach (var line in mapLines)
			{
				var parts = line.Split(':');

				if (parts.Length == 2)
					map.Define(parts[0], parts[1]);
			}

			return map;
		}

		public void Define(string alias, string process)
		{
			_map[alias] = process;
		}

		public string TryResolve(string aliasOrProcess)
		{
			if (_map.TryGetValue(aliasOrProcess, out string process))
				return process;

			return aliasOrProcess;
		}
	}
}
