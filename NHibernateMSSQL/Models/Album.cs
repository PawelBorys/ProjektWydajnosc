using System.Collections.Generic;

namespace NHibernateMSSQL.Models
{
	public class Album
	{
		public Album()
		{
			Tracks = new HashSet<Track>();
		}
		public virtual int Id { get; set; }
		public virtual string Title { get; set; }
		public virtual Artist Artist { get; set; }
		public virtual ISet<Track> Tracks { get; set; }
	}
}