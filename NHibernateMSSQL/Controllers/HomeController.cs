﻿using NHibernate;
using NHibernate.Linq;
using NHibernateMSSQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NHibernateMSSQL.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
			using (ISession session = NHibernateSession.OpenSession())
			{
				var albums = session.Query<Artist>().FirstOrDefault().Albums.ToList();
				ViewBag.employee = session.Query<Employee>().Where(e => e.EmployeesReporting.Count > 0).FirstOrDefault();
				ViewBag.number = ((Employee)ViewBag.employee).EmployeesReporting.Count;
				return View(albums);
			}
        }
    }
}