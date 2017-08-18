using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Infrastructure
{
	public interface IMyController
	{
		ActionResult GetSingleTables(int count);
		ActionResult GetRelatedTables(int count);
		ActionResult GetSingleTablesConditional(int count);
		ActionResult GetRelatedTablesConditional(int count);
		ActionResult InsertSingleTables(int count);
		ActionResult InsertRelatedTables(int count);

	}
}
