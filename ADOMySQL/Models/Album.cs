using System.Collections.Generic;

namespace ADOMySQL.Models
{
	public class Album
	{
		public virtual int Id { get; set; }
		public virtual string Title { get; set; }
		public virtual int ArtistId { get; set; }
		public virtual ISet<Track> Tracks { get; set; }
	}
}