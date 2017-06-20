using ADOMSSQL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ADOMSSQL.Controllers
{
    public class HomeController : Controller
    {
		// GET: Home
		public ActionResult Index()
		{
			//List<Track> results = new List<Track>();
			SqlConnection conn = new SqlConnection(@"data source=KOPUTER_PAWLA\SQLEXPRESS;initial catalog=Chinook;integrated security=SSPI;");
			
			SqlCommand command = new SqlCommand();
			command.CommandType = CommandType.Text;
			command.Connection = conn;
			command.CommandText = "select top 1000 * from Track";

			conn.Open();
			

			DataSet ds = new DataSet();
			SqlDataAdapter adapter = new SqlDataAdapter(command.CommandText, conn);
			adapter.Fill(ds);


			conn.Close();
			return View();
		}
    }
}