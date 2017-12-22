using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace p.Commands
{
	internal class NewCommand : Command
	{
		public override char Prefix => '+';

		public NewCommand(IProcessController processController, ArgumentProvider argumentProvider)
		{
			ProcessController = processController ?? throw new ArgumentNullException(nameof(processController));
			ArgumentProvider = argumentProvider ?? throw new ArgumentNullException(nameof(argumentProvider));
		}

		public override bool Execute(string processName)
		{
			return ProcessController.Start(processName, ArgumentProvider.Arguments);
		}

		public IProcessController ProcessController { get; }

		public ArgumentProvider ArgumentProvider { get; }
	}
}
