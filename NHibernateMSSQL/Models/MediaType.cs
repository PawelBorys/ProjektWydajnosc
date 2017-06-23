using System.Collections.Generic;

namespace NHibernateMSSQL.Models
{
	public class MediaType
	{
		public MediaType()
		{
			Tracks = new HashSet<Track>();
		}
		public virtual int Id { get; set; }
		public virtual string Name { get; set; }
		public virtual ISet<Track> Tracks { get; set; }
	}
}