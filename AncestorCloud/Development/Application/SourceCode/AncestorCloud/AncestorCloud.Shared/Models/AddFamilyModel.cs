using System;
using Cirrious.MvvmCross.Plugins.Sqlite;

namespace AncestorCloud.Shared
{
	public class AddFamilyModel : BaseModel
	{
		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		public string LastName { get; set; }
		public string BirthYear { get; set; }
		public string Gender { get; set; }
		public string BirthLocation { get; set; }

	}
}

