using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NHibernateMSSQL.Models
{
	public class Employee
	{
		public virtual int Id { get; set; }
		public virtual string LastName { get; set; }
		public virtual string FirstName { get; set; }
		public virtual string Title { get; set; }
		public virtual DateTime BirthDate { get; set; }
		public virtual DateTime HireDate { get; set; }
		public virtual string Address { get; set; }
		public virtual string City { get; set; }
		public virtual string State { get; set; }
		public virtual string Country { get; set; }
		public virtual string PostalCode { get; set; }
		public virtual string Phone { get; set; }
		public virtual string Fax { get; set; }
		public virtual string Email { get; set; }
		public virtual ISet<Employee> EmployeesReporting { get; set; }
		public virtual ISet<Customer> Customers { get; set; }

	}
}