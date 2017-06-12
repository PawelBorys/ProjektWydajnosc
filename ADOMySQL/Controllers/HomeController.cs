using ADOMySQL.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
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
			command.CommandText = "select * from Track";

			conn.Open();

			MySqlDataReader reader = command.ExecuteReader();

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