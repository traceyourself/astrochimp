using System;
using Cirrious.MvvmCross.Plugins.Sqlite;

namespace AncestorCloud.Shared
{
	public class BaseModel
	{
		[Ignore]
		public ResponseStatus Status{ get; set;}
		[Ignore]
		public string SessionId{ get; set;}
		[Ignore]
		public string LoggedinUserOFGN{ get; set;}
		[Ignore]
		public string LoggedinUserINDIOFGN{ get; set;}



	}
}

