using System.Collections.Generic;

namespace ADOMSSQL.Models
{
	public class MediaType
	{
		public virtual int Id { get; set; }
		public virtual string Name { get; set; }
		public virtual ISet<Track> Tracks { get; set; }
	}
}