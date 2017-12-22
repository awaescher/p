using p.Commands;
using p.Log;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace p
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			if (args == null || args.Length == 0)
				return;

			var argument = args[0];

			if (argument?.Length < 2)
				return;

			var container = TinyIoC.TinyIoCContainer.Current;

			var multiProcessMode = argument[0] == argument[1];

			var targetMode = multiProcessMode
				? DefaultProcessController.Target.AllProcesses
				: DefaultProcessController.Target.FirstProcess;

			var processName = argument.Substring(multiProcessMode ? 2 : 1);
			var processArgs = args.Length > 1 ? args.Skip(1).ToArray() : new string[0];

			var aliasMapFile = Path.Combine(Directory.GetParent(Assembly.GetExecutingAssembly().Location).FullName, "alias.map");
			var aliasMapLines = File.Exists(aliasMapFile) ? File.ReadAllLines(aliasMapFile) : new string[0];

			var commandTypes = new Type[] {
				typeof(NewCommand),
				typeof(FocusCommand),
				typeof(FocusOrNewCommand),
				typeof(KillCommand)
			};
			container.RegisterMultiple<Command>(commandTypes).AsSingleton();
			container.Register<ILog, ConsoleLog>().AsSingleton();
			container.Register(new ArgumentProvider(processArgs));
			container.Register(AliasMap.Read(aliasMapLines));
			container.Register<IProcessController, DefaultProcessController>().AsSingleton();
			(container.Resolve<IProcessController>() as DefaultProcessController).TargetMode = targetMode;

			var commands = container.ResolveAll<Command>();
			var command = commands.FirstOrDefault(c => c.Prefix == argument[0]);

			command?.Execute(processName);
		}
	}
}