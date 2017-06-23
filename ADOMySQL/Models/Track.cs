using System;
using System.Collections.Generic;

namespace ADOMySQL.Models
{
	public class Track
	{
		public virtual int Id { get; set; }
		public virtual string Name { get; set; }
		public virtual string Composer { get; set; }
		public virtual int Milliseconds { get; set; }
		public virtual int Bytes { get; set; }
		public virtual Decimal UnitPrice { get; set; }
		public virtual int AlbumId { get; set; }
		public virtual int GenreId { get; set; }
		public virtual int MediaTypeId { get; set; }
		public virtual ISet<InvoiceLine> InvoiceLines { get; set; }
		public virtual ISet<Playlist> Playlists { get; set; }
	}
}