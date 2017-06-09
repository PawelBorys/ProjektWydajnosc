using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NHibernateMySQL.Models
{
	public class Album
	{
		public virtual int Id { get; set; }
		public virtual string Title { get; set; }
		public virtual ISet<Track> Tracks { get; set; }
	}
}