using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EntityFrameworkMSSQL.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
			List<Track> tracks;

			GC.Collect();
			GC.WaitForPendingFinalizers();
			GC.Collect();
			var before = System.Diagnostics.Process.GetCurrentProcess().VirtualMemorySize64;

			Stopwatch watch = Stopwatch.StartNew();

			using (ChinookContext context = new ChinookContext())
			{
				tracks = context.Tracks.ToList();				
			}

			watch.Stop();
			ViewBag.time = watch.ElapsedMilliseconds;
			ViewBag.memory = System.Diagnostics.Process.GetCurrentProcess().VirtualMemorySize64 - before;

			return View(tracks);
		}
    }
}