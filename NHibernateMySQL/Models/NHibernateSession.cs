using NHibernate;
using NHibernate.Cfg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NHibernateMSSQL.Models
{
	public static class NHibernateSession
	{
		public static ISession OpenSession()
		{
			ISessionFactory sessionFactory = Configure().BuildSessionFactory();
			return sessionFactory.OpenSession();
		}

		public static IStatelessSession OpenStatelessSession()
		{
			ISessionFactory sessionFactory = Configure().BuildSessionFactory();
			return sessionFactory.OpenStatelessSession();
		}

		private static Configuration Configure()
		{
			var configuration = new Configuration();
			var configurationPath = HttpContext.Current.Server.MapPath(@"~\Models\NHibernate\hibernate.cfg.xml");
			configuration.Configure(configurationPath);

			var ArtistConfigurationFile = HttpContext.Current.Server.MapPath(@"~\Models\NHibernate\Artist.hbm.xml");
			configuration.AddFile(ArtistConfigurationFile);
			var AlbumConfigurationFile = HttpContext.Current.Server.MapPath(@"~\Models\NHibernate\Album.hbm.xml");
			configuration.AddFile(AlbumConfigurationFile);
			var CustomerConfigurationFile = HttpContext.Current.Server.MapPath(@"~\Models\NHibernate\Customer.hbm.xml");
			configuration.AddFile(CustomerConfigurationFile);
			var EmployeeConfigurationFile = HttpContext.Current.Server.MapPath(@"~\Models\NHibernate\Employee.hbm.xml");
			configuration.AddFile(EmployeeConfigurationFile);
			var GenreConfigurationFile = HttpContext.Current.Server.MapPath(@"~\Models\NHibernate\Genre.hbm.xml");
			configuration.AddFile(GenreConfigurationFile);
			var InvoiceConfigurationFile = HttpContext.Current.Server.MapPath(@"~\Models\NHibernate\Invoice.hbm.xml");
			configuration.AddFile(InvoiceConfigurationFile);
			var InvoiceLineConfigurationFile = HttpContext.Current.Server.MapPath(@"~\Models\NHibernate\InvoiceLine.hbm.xml");
			configuration.AddFile(InvoiceLineConfigurationFile);
			var MediaTypeConfigurationFile = HttpContext.Current.Server.MapPath(@"~\Models\NHibernate\MediaType.hbm.xml");
			configuration.AddFile(MediaTypeConfigurationFile);
			var PlaylistConfigurationFile = HttpContext.Current.Server.MapPath(@"~\Models\NHibernate\Playlist.hbm.xml");
			configuration.AddFile(PlaylistConfigurationFile);
			var TrackConfigurationFile = HttpContext.Current.Server.MapPath(@"~\Models\NHibernate\Track.hbm.xml");
			configuration.AddFile(TrackConfigurationFile);

			return configuration;
		}
	}
}