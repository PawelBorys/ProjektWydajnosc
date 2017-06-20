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

		public ActionResult GetSingleTable(int count)
		{
			List<Track> resultList = new List<Track>();

			if (count == 1)
			{
				mm.Start();
				Track result = _context.Tracks.Find(1);
				mm.Stop();			
					
				resultList.Add(result);
			}
			else
			{
				mm.Start();
				resultList = _context.Tracks.Take(count).ToList();
				mm.Stop();
			}

			ViewBag.time = mm.milliseconds;
			ViewBag.memory = mm.memory;
			return View(resultList);
		}

		public ActionResult GetRelatedTables(int count)
		{
			mm.Start();
			Track resultTrack = _context.Tracks.Find(1);
			Album resultAlbum = resultTrack.Album;
			Genre resultGenre = resultTrack.Genre;
			mm.Stop();

			TrackViewModel vm = new TrackViewModel()
			{
				Name = resultTrack.Name,
				Genre = resultGenre.Name,
				AlbumName = resultAlbum.Title
			};

			ViewBag.time = mm.milliseconds;
			ViewBag.memory = mm.memory;
			return View(vm);
		}

		public ActionResult InsertSingleTable(int count)
		{
			List<Track> tracks = new List<Track>();
			for (int i = 0; i < count; i++)
			{
				Track newTrack = _context.Tracks.Create();
				newTrack.AlbumId = 1;
				newTrack.Bytes = 123;
				newTrack.Composer = "TestComposer";
				newTrack.GenreId = 1;
				newTrack.MediaTypeId = 1;
				newTrack.Milliseconds = 123;
				newTrack.Name = "TestTrack " + i.ToString();
				newTrack.UnitPrice = 99;
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

		public ActionResult InsertRelatedTables(int count)
		{
			List<Album> albums = new List<Album>();
			List<Genre> genres = new List<Genre>();
			for (int i = 0; i < count; i++)
			{
				Album newAlbum = _context.Albums.Create();
				newAlbum.ArtistId = 1;
				newAlbum.Title = "Test Album " + i.ToString();
				albums.Add(newAlbum);

				Genre newGenre = _context.Genres.Create();
				newGenre.Name = "Test Genre " + i.ToString();
				genres.Add(newGenre);
			};


			List<Track> tracks = new List<Track>();
			for (int i = 0; i < count; i++)
			{
				Track newTrack = _context.Tracks.Create();
				newTrack.Album = albums[i];
				newTrack.Bytes = 123;
				newTrack.Composer = "TestComposer";
				newTrack.Genre = genres[i];
				newTrack.MediaTypeId = 1;
				newTrack.Milliseconds = 123;
				newTrack.Name = "TestTrack " + i.ToString();
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