using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADOMSSQL.Models
{
	public class Artist
	{
		public virtual int Id { get; set; }
		public virtual string Name { get; set; }
		public virtual ISet<Album> Albums { get; set; }
	}
}