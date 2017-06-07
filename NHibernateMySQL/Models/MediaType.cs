using System.Collections.Generic;

namespace NHibernateMySQL.Models
{
	public class MediaType
	{
		public virtual int Id { get; set; }
		public virtual string Name { get; set; }
		public virtual IList<Track> Tracks { get; set; }
	}
}