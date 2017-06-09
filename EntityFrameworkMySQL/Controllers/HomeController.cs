using EntityFrameworkMySQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EntityFrameworkMySQL.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
			using (ChinookContext context = new ChinookContext())
			{
				List<playlist> playlists = context.playlists.ToList();
				return View(playlists);
			}
		}
    }
}