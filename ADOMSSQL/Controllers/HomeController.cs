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
			List<Track> results = new List<Track>();
			SqlConnection conn = new SqlConnection(@"data source=KOPUTER_PAWLA\SQLEXPRESS;initial catalog=Chinook;integrated security=SSPI;");

			SqlCommand command = new SqlCommand();
			command.CommandType = CommandType.Text;
			command.Connection = conn;
			command.CommandText = "select * from Track";

			conn.Open();

			SqlDataReader reader = command.ExecuteReader();

			while (reader.Read())
			{
				if (int.Parse(reader["TrackId"].ToString()) != 1)
				{
					continue;
				}
				else
				{
					results.Add(new Track()
					{
						Name = reader["Name"].ToString(),
						Bytes = int.Parse(reader["Bytes"].ToString()),
						Composer = reader["Composer"].ToString(),
						Id = int.Parse(reader["TrackId"].ToString()),
						Milliseconds = int.Parse(reader["Milliseconds"].ToString()),
						UnitPrice = decimal.Parse(reader["UnitPrice"].ToString()),
						InvoiceLines = new HashSet<InvoiceLine>(),
						Playlists = new HashSet<Playlist>()
					});
					break;
				}
			}
			reader.NextResult();

			reader.Close();
			conn.Close();



			

			return View(results);
		}
    }
}