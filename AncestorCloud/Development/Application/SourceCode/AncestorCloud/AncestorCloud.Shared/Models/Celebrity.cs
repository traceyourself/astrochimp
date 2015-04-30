using System;
using Cirrious.MvvmCross.Plugins.Sqlite;

namespace AncestorCloud.Shared
{
	public class Celebrity
	{
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }

		public string GivenNames { get; set; }
		public string LastName { get; set; }
		public string Img { get; set; }
		public string OGFN { get; set; }
	}
}

