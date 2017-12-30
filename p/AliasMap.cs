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
				var keyEnd = line.IndexOf(':');
				if (keyEnd < 0 || keyEnd >= line.Length)
					continue;

				var key = line.Substring(0, keyEnd);
				var value = line.Substring(keyEnd + 1);

				map.Define(key, value);
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
