using ADOMySQL.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ADOMySQL.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
			List<Track> results = new List<Track>();
			MySqlConnection conn = new MySqlConnection(@"Server=localhost;Database=chinook;User ID=root");

			MySqlCommand command = new MySqlCommand();
			command.CommandType = System.Data.CommandType.Text;
			command.Connection = conn;
			command.CommandText = "select top 1000 * from Track";

			conn.Open();

			DataSet ds = new DataSet();
			MySqlDataAdapter adapter = new MySqlDataAdapter(command.CommandText, conn);
			adapter.Fill(ds);
			
			conn.Close();
			return View();
		}
    }
}