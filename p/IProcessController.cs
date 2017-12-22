using System.Diagnostics;

namespace p
{
	internal interface IProcessController
	{
		void BringToFront(Process process);

		Process[] GetByName(string processName);

		void Start(string processName);
	}
}