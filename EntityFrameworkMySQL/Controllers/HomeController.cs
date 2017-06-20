using EntityFrameworkMySQL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EntityFrameworkMySQL.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index(int scenario = 1)
        {
			List<track> tracks;

			GC.Collect();
			GC.WaitForPendingFinalizers();
			GC.Collect();
			var before = System.Diagnostics.Process.GetCurrentProcess().VirtualMemorySize64;

			Stopwatch watch = Stopwatch.StartNew();

			using (ChinookContext context = new ChinookContext())
			{
				tracks = context.tracks.Take(1000).ToList();        // podobno tworzy zapytanie "select top 1000"				
			}

			watch.Stop();
			ViewBag.time = watch.ElapsedMilliseconds;
			ViewBag.memory = System.Diagnostics.Process.GetCurrentProcess().VirtualMemorySize64 - before;

			return View(tracks);
		}
    }
}