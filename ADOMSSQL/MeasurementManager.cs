using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace ADOMSSQL
{
	public class MeasurementManager
	{
		public double milliseconds { get; set; }
		public long memory { get; set; }

		Stopwatch _watch;
		long _startMemory;


		public MeasurementManager(bool immediateStart = false)
		{
			_watch = new Stopwatch();
			if (immediateStart)
			{
				Start();
			}
		}

		public void Start()
		{			
			GC.Collect();
			GC.WaitForPendingFinalizers();
			GC.Collect();
			_startMemory = GC.GetTotalMemory(true);//Process.GetCurrentProcess().VirtualMemorySize64;
			_watch.Reset();
			_watch.Start();
		}

		public void Stop()
		{
			_watch.Stop();
			milliseconds = _watch.Elapsed.TotalMilliseconds;
			memory = GC.GetTotalMemory(false) - _startMemory; //Process.GetCurrentProcess().VirtualMemorySize64;// - _startMemory;			
		}
	}	
}