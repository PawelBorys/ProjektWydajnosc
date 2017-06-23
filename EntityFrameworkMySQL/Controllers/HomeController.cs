using EntityFrameworkMySQL;
using EntityFrameworkMySQL.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EntityFrameworkMySQL.Controllers
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
			List<artist> resultList = new List<artist>();


			mm.Start();
			for (int i = 1; i <= count; i++)
			{
				resultList.Add(_context.artists.Find(i));
			}
			mm.Stop();


			ViewBag.time = mm.milliseconds;
			ViewBag.memory = mm.memory;
			return View(resultList);
		}


		public ActionResult GetRelatedTables(int count)
		{
			List<track> tracks = new List<track>();
			List<album> albums = new List<album>();
			List<genre> genres = new List<genre>();
			List<mediatype> mediaTypes = new List<mediatype>();
			List<TrackViewModel> vmList = new List<TrackViewModel>();

			mm.Start();
			for (int i = 0; i < count; i++)
			{
				tracks.Add(_context.tracks.Find(i + 1));
				albums.Add(tracks[i].album);
				genres.Add(tracks[i].genre);
				mediaTypes.Add(tracks[i].mediatype);
			}
			mm.Stop();

			foreach (track t in tracks)
			{
				TrackViewModel vm = new TrackViewModel()
				{
					Name = t.Name,
					Genre = t.genre.Name,
					AlbumName = t.album.Title,
					MediaType = t.mediatype.Name
				};

			}

			ViewBag.time = mm.milliseconds;
			ViewBag.memory = mm.memory;
			return View(vmList);
		}

		public ActionResult GetSingleTablesConditional()
		{
			List<artist> resultList = new List<artist>();


			mm.Start();
			resultList = _context.artists.Where(a => a.ArtistId < 500).ToList();
			mm.Stop();


			ViewBag.time = mm.milliseconds;
			ViewBag.memory = mm.memory;
			return View(resultList);
		}


		public ActionResult GetRelatedTablesConditional()
		{
			List<track> tracks = new List<track>();
			List<album> albums = new List<album>();
			List<genre> genres = new List<genre>();
			List<mediatype> mediaTypes = new List<mediatype>();
			List<TrackViewModel> vmList = new List<TrackViewModel>();

			mm.Start();
			tracks = _context.tracks.Where(t => t.genre.Name == "Latin").ToList();
			foreach (track t in tracks)
			{
				albums.Add(t.album);
				genres.Add(t.genre);
				mediaTypes.Add(t.mediatype);
			}
			mm.Stop();

			foreach (track t in tracks)
			{
				TrackViewModel vm = new TrackViewModel()
				{
					Name = t.Name,
					Genre = t.genre.Name,
					AlbumName = t.album.Title,
					MediaType = t.mediatype.Name
				};

			}

			ViewBag.time = mm.milliseconds;
			ViewBag.memory = mm.memory;
			return View(vmList);
		}

		public ActionResult InsertSinglesTable(int count)
		{
			List<artist> artists = new List<artist>();
			for (int i = 1; i <= count; i++)
			{
				artist newArtist = _context.artists.Create();
				newArtist.Name = "Inserted Artist " + i.ToString();
			}

			if (count == 1)
			{
				mm.Start();
				_context.artists.Add(artists[0]);
			}
			else
			{
				mm.Start();
				_context.artists.AddRange(artists);
			}
			_context.SaveChanges();
			mm.Stop();

			ViewBag.time = mm.milliseconds;
			ViewBag.memory = mm.memory;
			return View("POSTResult");
		}

		public ActionResult InsertRelatedTables(int count)
		{
			List<album> albums = new List<album>();
			List<genre> genres = new List<genre>();
			List<mediatype> mediaTypes = new List<mediatype>();
			for (int i = 0; i < count; i++)
			{
				album newAlbum = _context.albums.Create();
				newAlbum.ArtistId = 1;
				newAlbum.Title = "Inserted Album " + i.ToString();
				albums.Add(newAlbum);

				genre newGenre = _context.genres.Create();
				newGenre.Name = "Inserted Genre " + i.ToString();
				genres.Add(newGenre);

				mediatype newType = _context.mediatypes.Create();
				newType.Name = "Inserted Media Type";
				mediaTypes.Add(newType);
			};


			List<track> tracks = new List<track>();
			for (int i = 0; i < count; i++)
			{
				track newTrack = _context.tracks.Create();
				newTrack.album = albums[i];
				newTrack.Bytes = 123;
				newTrack.Composer = "Inseted Composer";
				newTrack.genre = genres[i];
				newTrack.mediatype = mediaTypes[i];
				newTrack.Milliseconds = 123;
				newTrack.Name = "Inseted Track " + i.ToString();
				newTrack.UnitPrice = 99;

				tracks.Add(newTrack);
			}

			if (count == 1)
			{
				mm.Start();
				_context.tracks.Add(tracks[0]);
			}
			else
			{
				mm.Start();
				_context.tracks.AddRange(tracks);
			}
			_context.SaveChanges();
			mm.Stop();

			ViewBag.time = mm.milliseconds;
			ViewBag.memory = mm.memory;
			return View("POSTResult");
		}
	}
}