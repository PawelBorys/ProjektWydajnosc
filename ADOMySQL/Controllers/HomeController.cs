using ADOMySQL.Models;
using Infrastructure;
using Infrastructure.ViewModels;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;

namespace ADOMySQL.Controllers
{
	public class HomeController : Controller, IMyController
	{
		MySqlConnection conn = new MySqlConnection(@"Server=localhost;Database=chinook;User ID=root");
		MeasurementManager measurement = new MeasurementManager();

		// GET: Home
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult GetSingleTables(int count)
		{
			List<MeasurementViewModel> results = new List<MeasurementViewModel>();

			MySqlCommand command = new MySqlCommand();
			command.CommandType = CommandType.Text;
			command.Connection = conn;
			conn.Open();

			for (int i = 0; i < count; i++)
			{
				List<Artist> resultList = new List<Artist>();

				measurement.Start();
				for (int j = 1; j <= 1000; j++)
				{
					command.CommandText = "SELECT * FROM Artist a WHERE a.ArtistId = " + j.ToString();

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
				measurement.Stop();
				results.Add(new MeasurementViewModel() { memory = measurement.memory, milliseconds = measurement.milliseconds });
			}
			conn.Close();

			return View("MeasurementResults", results);
		}

		public ActionResult GetRelatedTables(int count)
		{
			List<MeasurementViewModel> results = new List<MeasurementViewModel>();

			MySqlCommand command = new MySqlCommand();
			command.CommandType = CommandType.Text;
			command.Connection = conn;
			conn.Open();

			for (int i = 0; i < count; i++)
			{
				List<Track> tracks = new List<Track>();
				List<Album> albums = new List<Album>();
				List<Genre> genres = new List<Genre>();
				List<MediaType> mediaTypes = new List<MediaType>();

				measurement.Start();
				for (int j = 1; j <= 1000; j++)
				{
					command.CommandText = "SELECT t.TrackId, t.Name, t.Composer, t.Milliseconds, t.Bytes, t.UnitPrice, al.AlbumId AS al_Id, al.Title AS al_Title, g.GenreId AS g_Id, g.Name AS g_Name, mt.MediaTypeId AS mt_Id, mt.Name AS mt_Name " +
						"FROM Track t " +
						"JOIN Album al ON al.AlbumId = t.AlbumId " +
						"JOIN Genre g ON g.GenreId = t.GenreId " +
						"JOIN MediaType mt ON mt.MediaTypeId = t.MediaTypeId " +
						"WHERE t.TrackId = " + j.ToString();

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
				measurement.Stop();
				results.Add(new MeasurementViewModel() { memory = measurement.memory, milliseconds = measurement.milliseconds });
			}

			return View("MeasurementResults", results);
		}

		public ActionResult GetSingleTablesConditional(int count)
		{
			List<MeasurementViewModel> results = new List<MeasurementViewModel>();

			MySqlCommand command = new MySqlCommand();
			command.CommandType = CommandType.Text;
			command.Connection = conn;
			conn.Open();

			for (int i = 0; i < count; i++)
			{
				List<Artist> resultList = new List<Artist>();

				measurement.Start();
				command.CommandText = "SELECT * FROM Artist a WHERE a.ArtistId <= 1000";
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
				measurement.Stop();

				results.Add(new MeasurementViewModel() { memory = measurement.memory, milliseconds = measurement.milliseconds });
			}
			conn.Close();

			return View("MeasurementResults", results);
		}

		public ActionResult GetRelatedTablesConditional(int count)
		{
			List<MeasurementViewModel> results = new List<MeasurementViewModel>();

			MySqlCommand command = new MySqlCommand();
			command.CommandType = CommandType.Text;
			command.Connection = conn;
			conn.Open();

			for (int i = 0; i < count; i++)
			{
				List<Track> tracks = new List<Track>();
				List<Album> albums = new List<Album>();
				List<Genre> genres = new List<Genre>();
				List<MediaType> mediaTypes = new List<MediaType>();

				measurement.Start();
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

				measurement.Stop();
				results.Add(new MeasurementViewModel() { memory = measurement.memory, milliseconds = measurement.milliseconds });
			}

			return View("MeasurementResults", results);
		}

		public ActionResult InsertSingleTables(int count)
		{
			List<MeasurementViewModel> results = new List<MeasurementViewModel>();

			MySqlCommand command = new MySqlCommand();
			command.CommandType = CommandType.Text;
			command.Connection = conn;
			conn.Open();

			for (int i = 0; i < count; i++)
			{
				List<Artist> artists = new List<Artist>();
				for (int j = 0; j < 100; j++)
				{
					Artist newArtist = new Artist();
					newArtist.Name = "Inserted Artist " + j.ToString();
				}

				command.CommandText = "INSERT INTO Artist(Name) VALUES (@name)";
				measurement.Start();
				for (int j = 0; j < 100; j++)
				{
					command.Parameters.Clear();
					command.Parameters.AddWithValue("@name", "Inserted Artist " + j.ToString());
					command.ExecuteNonQuery();
				}
				measurement.Stop();
				results.Add(new MeasurementViewModel() { memory = measurement.memory, milliseconds = measurement.milliseconds });
			}
			conn.Close();

			return View("MeasurementResults", results);
		}

		public ActionResult InsertRelatedTables(int count)
		{
			List<MeasurementViewModel> results = new List<MeasurementViewModel>();

			MySqlCommand command = new MySqlCommand();
			command.CommandType = CommandType.Text;
			command.Connection = conn;
			conn.Open();

			for (int i = 0; i < count; i++)
			{
				List<Album> albums = new List<Album>();
				List<Genre> genres = new List<Genre>();
				List<MediaType> mediaTypes = new List<MediaType>();
				for (int j = 0; j < 100; j++)
				{
					Album newAlbum = new Album();
					newAlbum.ArtistId = 1;
					newAlbum.Title = "Inserted Album " + j.ToString();
					albums.Add(newAlbum);

					Genre newGenre = new Genre();
					newGenre.Name = "Inserted Genre " + j.ToString();
					genres.Add(newGenre);

					MediaType newType = new MediaType();
					newType.Name = "Inserted Media Type";
					mediaTypes.Add(newType);
				};


				List<Track> tracks = new List<Track>();
				for (int j = 1; j <= 100; j++)
				{

					Track newTrack = new Track();
					newTrack.Bytes = 123;
					newTrack.Composer = "Inseted Composer";
					newTrack.Milliseconds = 123;
					newTrack.Name = "Inseted Track " + j.ToString();
					newTrack.UnitPrice = 99;

					tracks.Add(newTrack);
				}

				measurement.Start();
				for (int j = 0; j < 100; j++)
				{
					command.CommandText = "INSERT INTO Genre(Name) OUTPUT INSERTED.GenreId VALUES(@name)";
					command.Parameters.Clear();
					command.Parameters.AddWithValue("@name", genres[j].Name);
					int genreId = (int)command.ExecuteScalar();
					command.CommandText = "INSERT INTO Album(Title) OUTPUT INSERTED.AlbumId VALUES(@name)";
					command.Parameters.Clear();
					command.Parameters.AddWithValue("@name", albums[j].Title);
					int AlbumId = (int)command.ExecuteScalar();
					command.CommandText = "INSERT INTO MediaType(Name) OUTPUT INSERTED.MediaTypeId VALUES(@name)";
					command.Parameters.Clear();
					command.Parameters.AddWithValue("@name", mediaTypes[j].Name);
					int MediaTypeId = (int)command.ExecuteScalar();

					tracks[j].GenreId = genreId;
					tracks[j].AlbumId = AlbumId;
					tracks[j].MediaTypeId = MediaTypeId;

					command.CommandText = "INSERT INTO Track(Name, AlbumId, MediaTypeId, GenreId, Composer, Milliseconds, Bytes, UnitPrice) " +
						"VALUES(@name, @albumId, @mediaTypeId, @genreId, @composer, @milliseconds, @bytes, @unitPrice)";
					command.Parameters.Clear();
					command.Parameters.AddWithValue("@name", tracks[j].Name);
					command.Parameters.AddWithValue("@albumId", tracks[j].AlbumId);
					command.Parameters.AddWithValue("@mediaTypeId", tracks[j].MediaTypeId);
					command.Parameters.AddWithValue("@genreId", tracks[j].GenreId);
					command.Parameters.AddWithValue("@composer", tracks[j].Composer);
					command.Parameters.AddWithValue("@milliseconds", tracks[j].Milliseconds);
					command.Parameters.AddWithValue("@bytes", tracks[j].Bytes);
					command.Parameters.AddWithValue("@unitPrice", tracks[j].UnitPrice);

					command.ExecuteNonQuery();
				}
				measurement.Stop();
				results.Add(new MeasurementViewModel() { memory = measurement.memory, milliseconds = measurement.milliseconds });
			}

			return View("MeasurementResults", results);
		}

	}
}