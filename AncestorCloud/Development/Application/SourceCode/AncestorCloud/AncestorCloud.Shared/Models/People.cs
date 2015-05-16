using System;
using Cirrious.MvvmCross.Plugins.Sqlite;

namespace AncestorCloud.Shared
{
	public class People : User
	{
		public string Relation { get; set; }
		public bool IsSelected { get; set; }
		public string IndiOgfn { get; set; }
	
		public string MiddleName { get; set; }
		public string Contact { get; set; }
		public string LoginUserLinkID { get; set; }

	}
}

