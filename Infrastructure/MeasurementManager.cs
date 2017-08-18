using System;
using System.Diagnostics;

namespace Infrastructure
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
			_startMemory = GC.GetTotalMemory(true);
			_watch.Reset();
			_watch.Start();
		}

		public void Stop()
		{
			_watch.Stop();
			milliseconds = _watch.Elapsed.TotalMilliseconds;
			memory = GC.GetTotalMemory(false) - _startMemory;
		}
	}	
}