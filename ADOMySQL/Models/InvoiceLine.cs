namespace ADOMySQL.Models
{
	public class InvoiceLine
	{
		public virtual int Id { get; set; }
		public virtual int UnitPrice { get; set; }
		public virtual int Quantity { get; set; }
	}
}