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
			var configuration = new Configuration();
			var configurationPath = HttpContext.Current.Server.MapPath(@"~\Models\NHibernate\hibernate.cfg.xml");
			configuration.Configure(configurationPath);

			var ArtistConfigurationFile = HttpContext.Current.Server.MapPath(@"~\Models\NHibernate\Artist.hbm.xml");
			configuration.AddFile(ArtistConfigurationFile);
			var AlbumConfigurationFile = HttpContext.Current.Server.MapPath(@"~\Models\NHibernate\Album.hbm.xml");
			configuration.AddFile(AlbumConfigurationFile);


			ISessionFactory sessionFactory = configuration.BuildSessionFactory();
			return sessionFactory.OpenSession();
		}
	}
}