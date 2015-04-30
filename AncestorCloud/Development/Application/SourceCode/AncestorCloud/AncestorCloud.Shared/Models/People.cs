using System;
using Cirrious.MvvmCross.Plugins.Sqlite;

namespace AncestorCloud.Shared
{
	public class People : User
	{
		public string Relation { get; set; }
		public bool IsSelected { get; set; }
		[Ignore]
		public string MiddleName { get; set; }
	}
}

