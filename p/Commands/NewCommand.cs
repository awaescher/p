using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace p.Commands
{
	internal class NewCommand : Command
	{
		public override char Prefix => '+';

		public NewCommand(IProcessController processController)
		{
			ProcessController = processController ?? throw new ArgumentNullException(nameof(processController));
		}

		public override bool Execute(string processName)
		{
			ProcessController.Start(processName);
			return true;
		}

		public IProcessController ProcessController { get; }
	}
}
