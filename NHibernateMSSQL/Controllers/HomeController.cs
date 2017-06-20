using NHibernate;
using NHibernate.Linq;
using NHibernateMSSQL.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NHibernateMSSQL.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index(int scenario = 1)
        {
			using (ISession session = NHibernateSession.OpenSession())
			{
				Stopwatch watch1 = Stopwatch.StartNew();
				List<Track> test = session.Query<Track>().ToList();
				watch1.Stop();
				switch (scenario)
				{
					case 1:
						Stopwatch watch2 = Stopwatch.StartNew();
						List<Track> tracks = session.Query<Track>().Take(1000).ToList();
						watch2.Stop();
						return View(tracks);

					default:
						return View();
				}				
			}
        }
    }
}