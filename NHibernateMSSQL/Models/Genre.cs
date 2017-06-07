using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NHibernateMSSQL.Models
{
	public class Genre
	{
		public virtual int Id { get; set; }
		public virtual string Name { get; set; }
		public virtual IList<Track> Tracks { get; set; }
	}
}