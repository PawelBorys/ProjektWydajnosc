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

		// GET: Home
		public ActionResult Index(string scenario = "1a")
        {
			GC.Collect();
			GC.WaitForPendingFinalizers();
			GC.Collect();
			var before = System.Diagnostics.Process.GetCurrentProcess().VirtualMemorySize64;

			Stopwatch watch = Stopwatch.StartNew();

			// do stuff...

			watch.Stop();
			ViewBag.time = watch.ElapsedMilliseconds;
			ViewBag.memory = System.Diagnostics.Process.GetCurrentProcess().VirtualMemorySize64 - before;

			return View();
		}

		public ActionResult GetSingleTable(int count)
		{
			List<Track> resultList = new List<Track>();

			if (count == 1)
			{
				Track result = _context.Tracks.Find(1);
								
				resultList.Add(result);
			}
			else
			{
				resultList = _context.Tracks.Take(count).ToList();
			}


			return View(resultList);
		}

		public ActionResult GetRelatedTables(int count)
		{
			Track resultTrack = _context.Tracks.Find(1);
			Album resultAlbum = resultTrack.Album;
			Genre resultGenre = resultTrack.Genre;

			TrackViewModel vm = new TrackViewModel()
			{
				Name = resultTrack.Name,
				Genre = resultGenre.Name,
				AlbumName = resultAlbum.Title
			};

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
				_context.Tracks.Add(tracks[0]);
			}
			else
			{
				_context.Tracks.AddRange(tracks);
			}

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


			//TODO:  potwierdzić, że nie trzeba ręczenie wstawiać encji dla genre i album jeśli track je zawiera
			//if (count == 1)
			//{
			//	_context.Albums.Add(albums[0]);
			//	_context.Genres.Add(genres[0]);
			//}
			//else
			//{
			//	_context.Albums.AddRange(albums);
			//	_context.Genres.AddRange(genres);
			//}

			//_context.SaveChanges();


			List<Track> tracks = new List<Track>();
			for (int i = 0; i < count; i++)
			{
				Track newTrack = _context.Tracks.Create();
				//newTrack.AlbumId = albums[i].AlbumId;
				newTrack.Album = albums[i];
				newTrack.Bytes = 123;
				newTrack.Composer = "TestComposer";
				//newTrack.GenreId = genres[i].GenreId;
				newTrack.Genre = genres[i];
				newTrack.MediaTypeId = 1;
				newTrack.Milliseconds = 123;
				newTrack.Name = "TestTrack " + i.ToString();
				newTrack.UnitPrice = 99;
			}

			if (count == 1)
			{
				_context.Tracks.Add(tracks[0]);
			}
			else
			{
				_context.Tracks.AddRange(tracks);
			}

			return View("POSTResult");
		}

    }
}