using System;
using System.Collections.Generic;

namespace NHibernateMSSQL.Models
{
	public class Invoice
	{
		public virtual int Id { get; set; }
		public virtual DateTime InvoiceDate { get; set; }
		public virtual string BillingAddress { get; set; }
		public virtual string BillingCity { get; set; }
		public virtual string BillingState { get; set; }
		public virtual string BillingCountry { get; set; }
		public virtual string BillingPostalCode { get; set; }
		public virtual int Total { get; set; }
		public virtual ISet<InvoiceLine> InvoiceLines { get; set; }
	}
}