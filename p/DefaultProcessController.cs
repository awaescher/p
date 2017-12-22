using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace p
{
	internal class DefaultProcessController : IProcessController
	{
		[DllImport("USER32.DLL")]
		private static extern bool SetForegroundWindow(IntPtr hWnd);

		public DefaultProcessController(TargetMode target)
		{
			Target = target;
		}

		public Process[] GetByName(string processName)
		{
			var processes = Process.GetProcessesByName(processName);

			if (processes?.Length == 0)
			{
				Console.WriteLine("No processes found.");
			}
			else
			{
				if (Target == TargetMode.FirstProcess)
					processes = new Process[] { processes[0] };

				if (processes.Length == 1)
					Console.WriteLine($"Found process " + processes[0].Id.ToString());
				else
					Console.WriteLine($"Found {processes.Length} processes: " + string.Join(", ", processes.Select(p => p.Id.ToString()).ToArray()));
			}

			return processes;
		}

		public void Start(string processName)
		{
			Process.Start(processName);
		}

		public void BringToFront(Process process)
		{
			if (process.MainWindowHandle != IntPtr.Zero)
				SetForegroundWindow(process.MainWindowHandle);
		}

		public TargetMode Target { get; }

		public enum TargetMode
		{
			FirstProcess,
			AllProcesses
		}
	}
}
