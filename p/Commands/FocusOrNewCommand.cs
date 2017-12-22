using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace p.Commands
{
	internal class FocusOrNewCommand : Command
	{
		public override char Prefix => '~';

		public FocusOrNewCommand(FocusCommand focusCommand, NewCommand newCommand)
		{
			FocusCommand = focusCommand ?? throw new ArgumentNullException(nameof(focusCommand));
			NewCommand = newCommand ?? throw new ArgumentNullException(nameof(newCommand));
		}

		public override bool Execute(string processName)
		{
			var focused = FocusCommand.Execute(processName);

			if (focused)
				return true;

			return NewCommand.Execute(processName);
		}

		public FocusCommand FocusCommand { get; }

		public NewCommand NewCommand { get; }
	}
}
