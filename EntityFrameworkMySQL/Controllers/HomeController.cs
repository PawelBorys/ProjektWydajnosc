using Infrastructure;
using Infrastructure.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace EntityFrameworkMySQL.Controllers
{
	public class HomeController : Controller, IMyController
    {
		MeasurementManager measurement = new MeasurementManager();

		// GET: Home
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult GetSingleTables(int count)
		{
			List<MeasurementViewModel> results = new List<MeasurementViewModel>();
			for (int i = 0; i < count; i++)
			{
				List<artist> resultList = new List<artist>();

				using (ChinookContext context = new ChinookContext())
				{
					context.artists.ToList();
					measurement.Start();
					for (int j = 1; j <= 1000; j++)
					{
						resultList.Add(context.artists.AsNoTracking().Where(a => a.ArtistId == j).Single());
					}
					measurement.Stop();
					results.Add(new MeasurementViewModel() { memory = measurement.memory, milliseconds = measurement.milliseconds });
				}
			}

			return View("MeasurementResults", results);
		}


		public ActionResult GetRelatedTables(int count)
		{
			List<MeasurementViewModel> results = new List<MeasurementViewModel>();

			using (ChinookContext context = new ChinookContext())
			{
				for (int i = 0; i < count; i++)
				{
					List<track> tracks = new List<track>();
					List<album> albums = new List<album>();
					List<genre> genres = new List<genre>();
					List<mediatype> mediaTypes = new List<mediatype>();
					List<TrackViewModel> vmList = new List<TrackViewModel>();

					measurement.Start();
					for (int j = 0; j < 1000; j++)
					{
						tracks.Add(context.tracks.Where(t => t.TrackId == j + 1).Single());
						albums.Add(tracks[j].album);
						genres.Add(tracks[j].genre);
						mediaTypes.Add(tracks[j].mediatype);
					}
					measurement.Stop();
					results.Add(new MeasurementViewModel() { memory = measurement.memory, milliseconds = measurement.milliseconds });
				}
			}

			return View("MeasurementResults", results);
		}

		public ActionResult GetSingleTablesConditional(int count)
		{
			List<MeasurementViewModel> results = new List<MeasurementViewModel>();

			for (int i = 0; i < count; i++)
			{
				using (ChinookContext context = new ChinookContext())
				{
					context.artists.ToList();

					List<artist> artists = new List<artist>();
					measurement.Start();
					artists = context.artists.Where(a => a.ArtistId <= 1000).ToList();
					measurement.Stop();
					results.Add(new MeasurementViewModel() { memory = measurement.memory, milliseconds = measurement.milliseconds });
				}
			}

			return View("MeasurementResults", results);
		}

		public ActionResult GetRelatedTablesConditional(int count)
		{
			List<MeasurementViewModel> results = new List<MeasurementViewModel>();

			for (int i = 0; i < count; i++)
			{
				List<track> tracks = new List<track>();
				List<album> albums = new List<album>();
				List<genre> genres = new List<genre>();
				List<mediatype> mediaTypes = new List<mediatype>();

				using (ChinookContext context = new ChinookContext())
				{
					measurement.Start();
					tracks = context.tracks.Where(t => t.genre.Name == "Latin").ToList();
					foreach (track t in tracks)
					{
						albums.Add(t.album);
						genres.Add(t.genre);
						mediaTypes.Add(t.mediatype);
					}
					measurement.Stop();
					results.Add(new MeasurementViewModel() { memory = measurement.memory, milliseconds = measurement.milliseconds });
				}
			}

			return View("MeasurementResults", results);
		}

		public ActionResult InsertSingleTables(int count)
		{
			List<MeasurementViewModel> results = new List<MeasurementViewModel>();

			for (int i = 0; i < count; i++)
			{
				List<artist> artists = new List<artist>();

				using (ChinookContext context = new ChinookContext())
				{
					for (int j = 0; j < 100; j++)
					{
						artist newArtist = context.artists.Create();
						newArtist.Name = "Inserted Artist " + j.ToString();
					}

					measurement.Start();
					context.artists.AddRange(artists);
					context.SaveChanges();
					measurement.Stop();
					results.Add(new MeasurementViewModel() { memory = measurement.memory, milliseconds = measurement.milliseconds });
				}
			}

			return View("MeasurementResults", results);
		}

		public ActionResult InsertRelatedTables(int count)
		{
			List<MeasurementViewModel> results = new List<MeasurementViewModel>();

			for (int i = 0; i < count; i++)
			{
				List<album> albums = new List<album>();
				List<genre> genres = new List<genre>();
				List<mediatype> mediaTypes = new List<mediatype>();

				using (ChinookContext context = new ChinookContext())
				{
					for (int j = 0; j < 100; j++)
					{
						album newAlbum = context.albums.Create();
						newAlbum.ArtistId = 1;
						newAlbum.Title = "Inserted Album " + j.ToString();
						albums.Add(newAlbum);

						genre newGenre = context.genres.Create();
						newGenre.Name = "Inserted Genre " + j.ToString();
						genres.Add(newGenre);

						mediatype newType = context.mediatypes.Create();
						newType.Name = "Inserted Media Type";
						mediaTypes.Add(newType);
					};


					List<track> tracks = new List<track>();
					for (int j = 0; j < 100; j++)
					{
						track newTrack = context.tracks.Create();
						newTrack.album = albums[j];
						newTrack.Bytes = 123;
						newTrack.Composer = "Inseted Composer";
						newTrack.genre = genres[j];
						newTrack.mediatype = mediaTypes[j];
						newTrack.Milliseconds = 123;
						newTrack.Name = "Inseted Track " + j.ToString();
						newTrack.UnitPrice = 99;

						tracks.Add(newTrack);
					}

					measurement.Start();
					context.tracks.AddRange(tracks);
					context.SaveChanges();
					measurement.Stop();
					results.Add(new MeasurementViewModel() { memory = measurement.memory, milliseconds = measurement.milliseconds });
				}
			}

			return View("MeasurementResults", results);
		}
	}
}