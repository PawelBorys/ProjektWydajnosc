using Infrastructure;
using Infrastructure.ViewModels;
using NHibernate;
using NHibernate.Linq;
using NHibernateMSSQL.Models;
using NHibernateMySQL.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NHibernateMySQL.Controllers
{
    public class HomeController : Controller, IMyController
	{
		MeasurementManager mm = new MeasurementManager();

		// GET: Home
		public ActionResult Index(int scenario = 1)
		{
			using (ISession session = NHibernateSession.OpenSession())
			{
				Stopwatch watch1 = Stopwatch.StartNew();
				List<Track> test = session.Query<Track>().ToList();
				watch1.Stop();
				switch (scenario)
				{
					case 1:
						Stopwatch watch2 = Stopwatch.StartNew();
						List<Track> tracks = session.Query<Track>().Take(1000).ToList();
						watch2.Stop();
						return View(tracks);

					default:
						return View();
				}


			}

		}

		public ActionResult GetRelatedTables(int count)
		{
			List<Track> tracks = new List<Track>();
			List<Album> albums = new List<Album>();
			List<Genre> genres = new List<Genre>();
			List<MediaType> mediaTypes = new List<MediaType>();
			List<TrackViewModel> vmList = new List<TrackViewModel>();

			using (ISession session = NHibernateSession.OpenSession())
			{
				mm.Start();
				for (int i = 0; i < count; i++)
				{
					tracks.Add(session.Get<Track>(i + 1));
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
					vmList.Add(vm);

				}
			}

			ViewBag.time = mm.milliseconds;
			ViewBag.memory = mm.memory;
			return View(vmList);
		}

		public ActionResult GetRelatedTablesConditional()
		{
			List<Track> tracks = new List<Track>();
			List<Album> albums = new List<Album>();
			List<Genre> genres = new List<Genre>();
			List<MediaType> mediaTypes = new List<MediaType>();
			List<TrackViewModel> vmList = new List<TrackViewModel>();

			using (ISession session = NHibernateSession.OpenSession())
			{
				mm.Start();				
				tracks = session.Query<Track>().Where(t => t.Genre.Name == "Latin").ToList();
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
					vmList.Add(vm);
				}
			}

			ViewBag.time = mm.milliseconds;
			ViewBag.memory = mm.memory;
			return View("GetRelatedTables", vmList);
		}

		public ActionResult GetSingleTables(int count)
		{
			List<Artist> resultList = new List<Artist>();
			using (ISession session = NHibernateSession.OpenSession())
			{
				mm.Start();
				for (int i = 1; i <= count; i++)
				{
					resultList.Add(session.Get<Artist>(i));
				}
				mm.Stop();
			}

			ViewBag.time = mm.milliseconds;
			ViewBag.memory = mm.memory;
			return View(resultList);
		}

		public ActionResult GetSingleTablesConditional()
		{
			List<Artist> resultList = new List<Artist>();

			using (ISession session = NHibernateSession.OpenSession())
			{
				mm.Start();
				resultList = session.Query<Artist>().Where(a => a.Id < 500).ToList();
				mm.Stop();
			}

			ViewBag.time = mm.milliseconds;
			ViewBag.memory = mm.memory;
			return View("GetSingleTables", resultList);
		}


		public ActionResult InsertRelatedTables(int count)
		{
			using (ISession session = NHibernateSession.OpenSession())
			{
				List<Album> albums = new List<Album>();
				List<Genre> genres = new List<Genre>();
				List<MediaType> mediaTypes = new List<MediaType>();
				for (int i = 0; i < count; i++)
				{
					Album newAlbum = new Album();
					newAlbum.Artist = session.Get<Artist>(1);
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
				for (int i = 0; i < count; i++)
				{
					Track newTrack = new Track();
					newTrack.Album = albums[i];
					newTrack.Bytes = 123;
					newTrack.Composer = "Inseted Composer";
					newTrack.Genre = genres[i];
					newTrack.MediaType = mediaTypes[i];
					newTrack.Milliseconds = 123;
					newTrack.Name = "Inseted Track " + i.ToString();
					newTrack.UnitPrice = 99;

					tracks.Add(newTrack);
					albums[i].Tracks.Add(newTrack);
					genres[i].Tracks.Add(newTrack);
					mediaTypes[i].Tracks.Add(newTrack);
				}


				mm.Start();
				using (ITransaction transaction = session.BeginTransaction())
				{
					for (int i = 0; i < tracks.Count; i++)
					{
						session.Save(tracks[i]);
					}
					transaction.Commit();
				}
				mm.Stop();
			}

			ViewBag.time = mm.milliseconds;
			ViewBag.memory = mm.memory;
			return View("POSTResult");
		}

		public ActionResult InsertSingleTables(int count)
		{
			List<Artist> artists = new List<Artist>();
			for (int i = 1; i <= count; i++)
			{
				Artist newArtist = new Artist();
				newArtist.Name = "Inserted Artist " + i.ToString();
			}


			using (IStatelessSession session = NHibernateSession.OpenStatelessSession())
			{
				mm.Start();
				using (ITransaction transaction = session.BeginTransaction())
				{

					for (int i = 0; i < artists.Count; i++)
					{
						session.Insert(artists[i]);
					}
					transaction.Commit();
				}
				mm.Stop();

			}

			ViewBag.time = mm.milliseconds;
			ViewBag.memory = mm.memory;
			return View("POSTResult");
		}
	}
}