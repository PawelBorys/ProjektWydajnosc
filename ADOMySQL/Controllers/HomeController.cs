using ADOMySQL.Models;
using ADOMySQL.ViewModels;
using Infrastructure;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ADOMySQL.Controllers
{
    /*public class HomeController : Controller
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
    }*/

	public class HomeController : Controller, IMyController
	{
		MySqlConnection conn = new MySqlConnection(@"Server=localhost;Database=chinook;User ID=root");
		MeasurementManager mm = new MeasurementManager();

		// GET: Home
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult GetSingleTables(int count)
		{
			MySqlCommand command = new MySqlCommand();
			command.CommandType = CommandType.Text;
			command.Connection = conn;
			conn.Open();

			List<Artist> resultList = new List<Artist>();


			mm.Start();
			for (int i = 1; i <= count; i++)
			{
				command.CommandText = "SELECT * FROM Artist a WHERE a.ArtistId = " + i.ToString();

				MySqlDataReader reader = command.ExecuteReader();

				while (reader.Read())
				{
					Artist artist = new Artist()
					{
						Id = (int)reader["ArtistId"],
						Name = reader["Name"].ToString()
					};
					resultList.Add(artist);
				}
				reader.Close();

			}
			mm.Stop();

			conn.Close();

			ViewBag.time = mm.milliseconds;
			ViewBag.memory = mm.memory;
			return View(resultList);
		}

		public ActionResult GetRelatedTables(int count)
		{
			List<Track> tracks = new List<Track>();
			List<Album> albums = new List<Album>();
			List<Genre> genres = new List<Genre>();
			List<MediaType> mediaTypes = new List<MediaType>();
			List<TrackViewModel> vmList = new List<TrackViewModel>();

			MySqlCommand command = new MySqlCommand();
			command.CommandType = CommandType.Text;
			command.Connection = conn;
			conn.Open();

			mm.Start();
			for (int i = 1; i <= count; i++)
			{
				command.CommandText = "SELECT t.TrackId, t.Name, t.Composer, t.Milliseconds, t.Bytes, t.UnitPrice, al.AlbumId AS al_Id, al.Title AS al_Title, g.GenreId AS g_Id, g.Name AS g_Name, mt.MediaTypeId AS mt_Id, mt.Name AS mt_Name " +
					"FROM Track t " +
					"JOIN Album al ON al.AlbumId = t.AlbumId " +
					"JOIN Genre g ON g.GenreId = t.GenreId " +
					"JOIN MediaType mt ON mt.MediaTypeId = t.MediaTypeId " +
					"WHERE t.TrackId = " + i.ToString();

				MySqlDataReader reader = command.ExecuteReader();

				while (reader.Read())
				{
					Track track = new Track()
					{
						Id = (int)reader["TrackId"],
						Name = reader["Name"].ToString(),
						Bytes = (int)reader["Bytes"],
						Composer = reader["Composer"].ToString(),
						Milliseconds = (int)reader["Milliseconds"],
						UnitPrice = (decimal)reader["UnitPrice"]
					};
					tracks.Add(track);

					Album album = new Album()
					{
						Id = (int)reader["al_Id"],
						Title = reader["al_Title"].ToString()
					};
					albums.Add(album);

					Genre genre = new Genre()
					{
						Id = (int)reader["g_Id"],
						Name = reader["g_Name"].ToString()
					};
					genres.Add(genre);

					MediaType mediaType = new MediaType()
					{
						Id = (int)reader["mt_Id"],
						Name = reader["mt_Name"].ToString()
					};
					mediaTypes.Add(mediaType);
				}
				reader.Close();
			}
			mm.Stop();

			for (int i = 0; i < tracks.Count; i++)
			{
				TrackViewModel vm = new TrackViewModel()
				{
					Name = tracks[i].Name,
					Genre = genres[i].Name,
					AlbumName = albums[i].Title,
					MediaType = mediaTypes[i].Name
				};
				vmList.Add(vm);
			}

			ViewBag.time = mm.milliseconds;
			ViewBag.memory = mm.memory;
			return View(vmList);
		}

		public ActionResult GetSingleTablesConditional()
		{
			MySqlCommand command = new MySqlCommand();
			command.CommandType = CommandType.Text;
			command.Connection = conn;
			conn.Open();

			List<Artist> resultList = new List<Artist>();


			mm.Start();
			command.CommandText = "SELECT * FROM Artist a WHERE a.ArtistId < 500";
			MySqlDataReader reader = command.ExecuteReader();
			while (reader.Read())
			{
				Artist artist = new Artist()
				{
					Id = (int)reader["ArtistId"],
					Name = reader["Name"].ToString()
				};
				resultList.Add(artist);
			}
			reader.Close();
			mm.Stop();


			conn.Close();
			ViewBag.time = mm.milliseconds;
			ViewBag.memory = mm.memory;
			return View(resultList);
		}

		public ActionResult GetRelatedTablesConditional()
		{
			List<Track> tracks = new List<Track>();
			List<Album> albums = new List<Album>();
			List<Genre> genres = new List<Genre>();
			List<MediaType> mediaTypes = new List<MediaType>();
			List<TrackViewModel> vmList = new List<TrackViewModel>();

			MySqlCommand command = new MySqlCommand();
			command.CommandType = CommandType.Text;
			command.Connection = conn;
			conn.Open();

			mm.Start();

			command.CommandText = "SELECT t.TrackId, t.Name, t.Composer, t.Milliseconds, t.Bytes, t.UnitPrice, al.AlbumId AS al_Id, al.Title AS al_Title, g.GenreId AS g_Id, g.Name AS g_Name, mt.MediaTypeId AS mt_Id, mt.Name AS mt_Name " +
				"FROM Track t " +
				"JOIN Album al ON al.AlbumId = t.AlbumId " +
				"JOIN Genre g ON g.GenreId = t.GenreId " +
				"JOIN MediaType mt ON mt.MediaTypeId = t.MediaTypeId " +
				"WHERE g.Name = 'Latin'";

			MySqlDataReader reader = command.ExecuteReader();

			while (reader.Read())
			{
				Track track = new Track()
				{
					Id = (int)reader["TrackId"],
					Name = reader["Name"].ToString(),
					Bytes = (int)reader["Bytes"],
					Composer = reader["Composer"].ToString(),
					Milliseconds = (int)reader["Milliseconds"],
					UnitPrice = (decimal)reader["UnitPrice"]
				};
				tracks.Add(track);

				Album album = new Album()
				{
					Id = (int)reader["al_Id"],
					Title = reader["al_Title"].ToString()
				};
				albums.Add(album);

				Genre genre = new Genre()
				{
					Id = (int)reader["g_Id"],
					Name = reader["g_Name"].ToString()
				};
				genres.Add(genre);

				MediaType mediaType = new MediaType()
				{
					Id = (int)reader["mt_Id"],
					Name = reader["mt_Name"].ToString()
				};
				mediaTypes.Add(mediaType);
			}
			reader.Close();

			mm.Stop();

			for (int i = 0; i < tracks.Count; i++)
			{
				TrackViewModel vm = new TrackViewModel()
				{
					Name = tracks[i].Name,
					Genre = genres[i].Name,
					AlbumName = albums[i].Title,
					MediaType = mediaTypes[i].Name
				};
				vmList.Add(vm);
			}

			ViewBag.time = mm.milliseconds;
			ViewBag.memory = mm.memory;
			return View(vmList);
		}

		public ActionResult InsertSingleTables(int count)
		{
			MySqlCommand command = new MySqlCommand();
			command.CommandType = CommandType.Text;
			command.Connection = conn;
			conn.Open();

			List<Artist> artists = new List<Artist>();
			for (int i = 1; i <= count; i++)
			{
				Artist newArtist = new Artist();
				newArtist.Name = "Inserted Artist " + i.ToString();
			}

			command.CommandText = "INSERT INTO Artist(Name) VALUES (@name)";
			mm.Start();
			for (int i = 0; i < count; i++)
			{
				command.Parameters.Clear();
				command.Parameters.AddWithValue("@name", "Inserted Artist " + i.ToString());
				command.ExecuteNonQuery();
			}
			mm.Stop();

			conn.Close();
			ViewBag.time = mm.milliseconds;
			ViewBag.memory = mm.memory;
			return View("POSTResult");
		}

		public ActionResult InsertRelatedTables(int count)
		{
			MySqlCommand command = new MySqlCommand();
			command.CommandType = CommandType.Text;
			command.Connection = conn;
			conn.Open();

			List<Album> albums = new List<Album>();
			List<Genre> genres = new List<Genre>();
			List<MediaType> mediaTypes = new List<MediaType>();
			for (int i = 0; i < count; i++)
			{
				Album newAlbum = new Album();
				newAlbum.ArtistId = 1;
				newAlbum.Title = "Inserted Album " + i.ToString();
				albums.Add(newAlbum);

				Genre newGenre = new Genre();
				newGenre.Name = "Inserted Genre " + i.ToString();
				genres.Add(newGenre);

				MediaType newType = new MediaType();
				newType.Name = "Inserted Media Type";
				mediaTypes.Add(newType);
			};


			List<Track> tracks = new List<Track>();
			for (int i = 1; i <= count; i++)
			{

				Track newTrack = new Track();
				//newTrack.AlbumId = 1;
				newTrack.Bytes = 123;
				newTrack.Composer = "Inseted Composer";
				//newTrack.GenreId = 1;
				//newTrack.MediaTypeId = 1;
				newTrack.Milliseconds = 123;
				newTrack.Name = "Inseted Track " + i.ToString();
				newTrack.UnitPrice = 99;

				tracks.Add(newTrack);
			}

			mm.Start();
			for (int i = 0; i < count; i++)
			{
				command.CommandText = "INSERT INTO Genre(Name) VALUES(@name)";
				command.Parameters.Clear();
				command.Parameters.AddWithValue("@name", genres[i].Name);
				command.ExecuteNonQuery();
				int genreId = (int)command.LastInsertedId;
				command.CommandText = "INSERT INTO Album(Title, ArtistId) VALUES(@name, @artistId)";
				command.Parameters.Clear();
				command.Parameters.AddWithValue("@name", albums[i].Title);
				command.Parameters.AddWithValue("@artistId", albums[i].ArtistId);
				command.ExecuteNonQuery();
				int AlbumId = (int)command.LastInsertedId;
				command.CommandText = "INSERT INTO mediatype(Name) VALUES(@name)";
				command.Parameters.Clear();
				command.Parameters.AddWithValue("@name", mediaTypes[i].Name);
				command.ExecuteNonQuery();
				int MediaTypeId = (int)command.LastInsertedId;

				tracks[i].GenreId = genreId;
				tracks[i].AlbumId = AlbumId;
				tracks[i].MediaTypeId = MediaTypeId;

				command.CommandText = "INSERT INTO Track(Name, AlbumId, MediaTypeId, GenreId, Composer, Milliseconds, Bytes, UnitPrice) " +
					"VALUES(@name, @albumId, @mediaTypeId, @genreId, @composer, @milliseconds, @bytes, @unitPrice)";
				command.Parameters.Clear();
				command.Parameters.AddWithValue("@name", tracks[i].Name);
				command.Parameters.AddWithValue("@albumId", tracks[i].AlbumId);
				command.Parameters.AddWithValue("@mediaTypeId", tracks[i].MediaTypeId);
				command.Parameters.AddWithValue("@genreId", tracks[i].GenreId);
				command.Parameters.AddWithValue("@composer", tracks[i].Composer);
				command.Parameters.AddWithValue("@milliseconds", tracks[i].Milliseconds);
				command.Parameters.AddWithValue("@bytes", tracks[i].Bytes);
				command.Parameters.AddWithValue("@unitPrice", tracks[i].UnitPrice);

				command.ExecuteNonQuery();
			}

			mm.Stop();

			ViewBag.time = mm.milliseconds;
			ViewBag.memory = mm.memory;
			return View("POSTResult");
		}

	}
}