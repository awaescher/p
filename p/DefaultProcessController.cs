using p.Log;
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

		public DefaultProcessController(ILog log)
		{
			Log = log ?? throw new ArgumentNullException(nameof(log));
		}

		public Process[] GetByName(string processName)
		{
			var processes = Process.GetProcessesByName(processName);

			if (processes?.Length == 0)
			{
				Log.Log("No processes found.");
			}
			else
			{
				if (TargetMode == Target.FirstProcess)
					processes = new Process[] { processes[0] };

				if (processes.Length == 1)
					Log.Log($"Found process " + processes[0].Id.ToString());
				else
					Log.Log($"Found {processes.Length} processes: " + string.Join(", ", processes.Select(p => p.Id.ToString()).ToArray()));
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

		public Target TargetMode { get; set; } = Target.AllProcesses;
		public ILog Log { get; }

		public enum Target
		{
			FirstProcess,
			AllProcesses
		}
	}
}
