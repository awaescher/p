using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace p.Commands
{
	internal class FocusCommand : Command
	{
		public override char Prefix => '*';

		public FocusCommand(IProcessController processController)
		{
			ProcessController = processController ?? throw new ArgumentNullException(nameof(processController));
		}

		public override bool Execute(string processName)
		{
			var processes = ProcessController.GetByName(processName);

			foreach (var process in processes)
				ProcessController.BringToFront(process);

			return processes.Any();
		}

		public IProcessController ProcessController { get; }
	}
}
