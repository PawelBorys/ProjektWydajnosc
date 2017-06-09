using NHibernate;
using NHibernate.Linq;
using NHibernateMSSQL.Models;
using NHibernateMySQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NHibernateMySQL.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
			using (ISession session = NHibernateSession.OpenSession())
			{
				var genres = session.Query<Genre>().ToList();
				return View(genres);
			}

		}
    }
}