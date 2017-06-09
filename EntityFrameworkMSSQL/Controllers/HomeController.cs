using System;
using System.Collections.Generic;
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
			using(ChinookContext context = new ChinookContext())
			{
				List<MediaType> types = context.MediaTypes.ToList();
				return View(types);
			}


			
        }
    }
}