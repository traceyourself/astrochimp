using System;
using Cirrious.MvvmCross.Plugins.Sqlite;

namespace AncestorCloud.Shared
{
	public class LoginModel : BaseModel
	{
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }
		
		public string Message { get; set; }
		public string UserEmail { get; set; }
		public string Code { get; set; }
		public string Value { get; set; }
		public string IndiOGFN { get; set; }
		public string OGFN { get; set; }
		public string AvatarOGFN { get; set; }
		public string AvatarURL { get; set; }
		public string GroupOGFN { get; set; }
		public string FamOGFN { get; set; }
		public string Name { get; set; }

	}
}

