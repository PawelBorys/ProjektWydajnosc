using EntityFrameworkMSSQL.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EntityFrameworkMSSQL.Controllers
{
    public class HomeController : Controller
    {
		ChinookContext _context = new ChinookContext();
		MeasurementManager mm = new MeasurementManager();

		// GET: Home
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult GetSingleTables(int count)
		{
			List<Artist> resultList = new List<Artist>();


			mm.Start();
			for (int i = 1; i <= count; i++)
			{
				resultList.Add(_context.Artists.Find(i));
			}
			mm.Stop();


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

			mm.Start();
			for (int i = 0; i < count; i++)
			{
				tracks.Add(_context.Tracks.Find(i+1));
				albums.Add(tracks[i].Album);
				genres.Add(tracks[i].Genre);
				mediaTypes.Add(tracks[i].MediaType);
			}
			mm.Stop();

			foreach (Track t in tracks)
			{
				TrackViewModel vm = new TrackViewModel()
				{
					Name = t.Name,
					Genre = t.Genre.Name,
					AlbumName = t.Album.Title,
					MediaType = t.MediaType.Name
				};

			}

			ViewBag.time = mm.milliseconds;
			ViewBag.memory = mm.memory;
			return View(vmList);
		}

		public ActionResult GetSingleTablesConditional()
		{
			List<Artist> resultList = new List<Artist>();


			mm.Start();			
			resultList = _context.Artists.Where(a => a.ArtistId < 500).ToList();			
			mm.Stop();


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

			mm.Start();
			tracks = _context.Tracks.Where(t => t.Genre.Name == "Latin").ToList();
			foreach (Track t in tracks)
			{
				albums.Add(t.Album);
				genres.Add(t.Genre);
				mediaTypes.Add(t.MediaType);
			}			
			mm.Stop();

			foreach (Track t in tracks)
			{
				TrackViewModel vm = new TrackViewModel()
				{
					Name = t.Name,
					Genre = t.Genre.Name,
					AlbumName = t.Album.Title,
					MediaType = t.MediaType.Name
				};

			}

			ViewBag.time = mm.milliseconds;
			ViewBag.memory = mm.memory;
			return View(vmList);
		}

		public ActionResult InsertSinglesTable(int count)
		{
			List<Artist> artists = new List<Artist>();
			for (int i = 1; i <= count; i++)
			{
				Artist newArtist = _context.Artists.Create();
				newArtist.Name = "Inserted Artist " + i.ToString();
			}

			if (count == 1)
			{
				mm.Start();
				_context.Artists.Add(artists[0]);				
			}
			else
			{
				mm.Start();
				_context.Artists.AddRange(artists);				
			}
			_context.SaveChanges();
			mm.Stop();

			ViewBag.time = mm.milliseconds;
			ViewBag.memory = mm.memory;
			return View("POSTResult");
		}

		public ActionResult InsertRelatedTables(int count)
		{
			List<Album> albums = new List<Album>();
			List<Genre> genres = new List<Genre>();
			List<MediaType> mediaTypes = new List<MediaType>();
			for (int i = 0; i < count; i++)
			{
				Album newAlbum = _context.Albums.Create();
				newAlbum.ArtistId = 1;
				newAlbum.Title = "Inserted Album " + i.ToString();
				albums.Add(newAlbum);

				Genre newGenre = _context.Genres.Create();
				newGenre.Name = "Inserted Genre " + i.ToString();
				genres.Add(newGenre);

				MediaType newType = _context.MediaTypes.Create();
				newType.Name = "Inserted Media Type";
				mediaTypes.Add(newType); 
			};


			List<Track> tracks = new List<Track>();
			for (int i = 0; i < count; i++)
			{
				Track newTrack = _context.Tracks.Create();
				newTrack.Album = albums[i];
				newTrack.Bytes = 123;
				newTrack.Composer = "Inseted Composer";
				newTrack.Genre = genres[i];
				newTrack.MediaType = mediaTypes[i];
				newTrack.Milliseconds = 123;
				newTrack.Name = "Inseted Track " + i.ToString();
				newTrack.UnitPrice = 99;

				tracks.Add(newTrack);
			}

			if (count == 1)
			{
				mm.Start();
				_context.Tracks.Add(tracks[0]);
			}
			else
			{
				mm.Start();
				_context.Tracks.AddRange(tracks);
			}
			_context.SaveChanges();
			mm.Stop();

			ViewBag.time = mm.milliseconds;
			ViewBag.memory = mm.memory;
			return View("POSTResult");
		}
    }
}