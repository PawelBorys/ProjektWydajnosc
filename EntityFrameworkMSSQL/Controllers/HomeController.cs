using Infrastructure;
using Infrastructure.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace EntityFrameworkMSSQL.Controllers
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
				List<Artist> resultList = new List<Artist>();

				using (ChinookContext context = new ChinookContext())
				{
					context.Artists.ToList();
					measurement.Start();
					for (int j = 1; j <= 1000; j++)
					{
						resultList.Add(context.Artists.AsNoTracking().Where(a => a.ArtistId == j).Single());
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
					List<Track> tracks = new List<Track>();
					List<Album> albums = new List<Album>();
					List<Genre> genres = new List<Genre>();
					List<MediaType> mediaTypes = new List<MediaType>();
					List<TrackViewModel> vmList = new List<TrackViewModel>();

					measurement.Start();
					for (int j = 0; j < 1000; j++)
					{
						tracks.Add(context.Tracks.Where(t => t.TrackId == j + 1).Single());
						albums.Add(tracks[j].Album);
						genres.Add(tracks[j].Genre);
						mediaTypes.Add(tracks[j].MediaType);
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
					context.Artists.ToList();

					List<Artist> artists = new List<Artist>();
					measurement.Start();
					artists = context.Artists.Where(a => a.ArtistId <= 1000).ToList();
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
				List<Track> tracks = new List<Track>();
				List<Album> albums = new List<Album>();
				List<Genre> genres = new List<Genre>();
				List<MediaType> mediaTypes = new List<MediaType>();

				using (ChinookContext context = new ChinookContext())
				{
					measurement.Start();
					tracks = context.Tracks.Where(t => t.Genre.Name == "Latin").ToList();
					foreach (Track t in tracks)
					{
						albums.Add(t.Album);
						genres.Add(t.Genre);
						mediaTypes.Add(t.MediaType);
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
				List<Artist> artists = new List<Artist>();

				using (ChinookContext context = new ChinookContext())
				{
					for (int j = 0; j < 100; j++)
					{
						Artist newArtist = context.Artists.Create();
						newArtist.Name = "Inserted Artist " + j.ToString();
					}

					measurement.Start();
					context.Artists.AddRange(artists);
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
				List<Album> albums = new List<Album>();
				List<Genre> genres = new List<Genre>();
				List<MediaType> mediaTypes = new List<MediaType>();

				using (ChinookContext context = new ChinookContext())
				{
					for (int j = 0; j < 100; j++)
					{
						Album newAlbum = context.Albums.Create();
						newAlbum.ArtistId = 1;
						newAlbum.Title = "Inserted Album " + j.ToString();
						albums.Add(newAlbum);

						Genre newGenre = context.Genres.Create();
						newGenre.Name = "Inserted Genre " + j.ToString();
						genres.Add(newGenre);

						MediaType newType = context.MediaTypes.Create();
						newType.Name = "Inserted Media Type";
						mediaTypes.Add(newType);
					};


					List<Track> tracks = new List<Track>();
					for (int j = 0; j < 100; j++)
					{
						Track newTrack = context.Tracks.Create();
						newTrack.Album = albums[j];
						newTrack.Bytes = 123;
						newTrack.Composer = "Inseted Composer";
						newTrack.Genre = genres[j];
						newTrack.MediaType = mediaTypes[j];
						newTrack.Milliseconds = 123;
						newTrack.Name = "Inseted Track " + j.ToString();
						newTrack.UnitPrice = 99;

						tracks.Add(newTrack);
					}

					measurement.Start();
					context.Tracks.AddRange(tracks);
					context.SaveChanges();
					measurement.Stop();
					results.Add(new MeasurementViewModel() { memory = measurement.memory, milliseconds = measurement.milliseconds });
				}
			}

			return View("MeasurementResults", results);
		}
    }
}