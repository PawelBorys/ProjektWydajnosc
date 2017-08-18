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
				using (ISession session = NHibernateSession.OpenSession())
				{
					List<Artist> artists = new List<Artist>();

					measurement.Start();
					for (int j = 1; j <= 1000; j++)
					{
						artists.Add(session.Get<Artist>(j));
					}
					measurement.Stop();
					results.Add(new MeasurementViewModel() { memory = measurement.memory, milliseconds = measurement.milliseconds });
					session.Clear();
				}
			}

			return View("MeasurementResults", results);
		}

		public ActionResult GetRelatedTables(int count)
		{
			List<MeasurementViewModel> results = new List<MeasurementViewModel>();

			for (int j = 0; j < count; j++)
			{
				List<Track> tracks = new List<Track>();
				List<Album> albums = new List<Album>();
				List<Genre> genres = new List<Genre>();
				List<MediaType> mediaTypes = new List<MediaType>();

				using (ISession session = NHibernateSession.OpenSession())
				{
					measurement.Start();
					for (int i = 0; i < 1000; i++)
					{
						tracks.Add(session.Get<Track>(i + 1));
						albums.Add(tracks[i].Album);
						genres.Add(tracks[i].Genre);
						mediaTypes.Add(tracks[i].MediaType);
					}
					measurement.Stop();
					results.Add(new MeasurementViewModel() { memory = measurement.memory, milliseconds = measurement.milliseconds });
					session.Clear();
				}
			}

			return View("MeasurementResults", results);
		}

		public ActionResult GetSingleTablesConditional(int count)
		{
			List<MeasurementViewModel> results = new List<MeasurementViewModel>();

			for (int i = 0; i < count; i++)
			{
				List<Artist> artists = new List<Artist>();

				using (ISession session = NHibernateSession.OpenSession())
				{
					measurement.Start();
					artists = session.Query<Artist>().Where(a => a.Id <= 1000).ToList();
					measurement.Stop();
					results.Add(new MeasurementViewModel() { memory = measurement.memory, milliseconds = measurement.milliseconds });
					session.Clear();
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

				using (ISession session = NHibernateSession.OpenSession())
				{
					measurement.Start();
					tracks = session.Query<Track>().Where(t => t.Genre.Name == "Latin").ToList();
					foreach (Track t in tracks)
					{
						albums.Add(t.Album);
						genres.Add(t.Genre);
						mediaTypes.Add(t.MediaType);
					}
					measurement.Stop();
					results.Add(new MeasurementViewModel() { memory = measurement.memory, milliseconds = measurement.milliseconds });
					session.Clear();
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
				for (int j = 0; j < 100; j++)
				{
					Artist newArtist = new Artist();
					newArtist.Name = "Inserted Artist " + j.ToString();
					artists.Add(newArtist);
				}

				using (IStatelessSession session = NHibernateSession.OpenStatelessSession())
				{
					measurement.Start();
					using (ITransaction transaction = session.BeginTransaction())
					{
						for (int j = 0; j < artists.Count; j++)
						{
							session.Insert(artists[j]);
						}
						transaction.Commit();
					}
					measurement.Stop();
					results.Add(new MeasurementViewModel() { memory = measurement.memory, milliseconds = measurement.milliseconds });
				}
			}

			return View("MeasurementResults", results);
		}


		public ActionResult InsertRelatedTables(int count)
		{
			List<MeasurementViewModel> results = new List<MeasurementViewModel>();

			using (ISession session = NHibernateSession.OpenSession())
			{
				for (int i = 0; i < count; i++)
				{
					List<Album> albums = new List<Album>();
					List<Genre> genres = new List<Genre>();
					List<MediaType> mediaTypes = new List<MediaType>();
					for (int j = 0; j < 100; j++)
					{
						Album newAlbum = new Album();
						newAlbum.Artist = session.Get<Artist>(1);
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
					for (int j = 0; j < 100; j++)
					{
						Track newTrack = new Track();
						newTrack.Album = albums[j];
						newTrack.Bytes = 123;
						newTrack.Composer = "Inseted Composer";
						newTrack.Genre = genres[j];
						newTrack.MediaType = mediaTypes[j];
						newTrack.Milliseconds = 123;
						newTrack.Name = "Inseted Track " + j.ToString();
						newTrack.UnitPrice = 99;

						tracks.Add(newTrack);
						albums[j].Tracks.Add(newTrack);
						genres[j].Tracks.Add(newTrack);
						mediaTypes[j].Tracks.Add(newTrack);
					}

					measurement.Start();
					using (ITransaction transaction = session.BeginTransaction())
					{
						for (int j = 0; j < tracks.Count; j++)
						{
							session.Save(tracks[j]);
						}
						transaction.Commit();
					}
					measurement.Stop();
				}
			}

			return View("MeasurementResults", results);
		}
	}
}
