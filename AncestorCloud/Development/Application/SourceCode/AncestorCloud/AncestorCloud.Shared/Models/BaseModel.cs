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
		[Ignore]
		public string LoggedinUserFAMOFGN{ get; set;}

		//For Get Family Service
		[Ignore]
		public string CHILD_OFGNS{ get; set;}
		[Ignore]
		public string FATHER_OFGN{ get; set;}
		[Ignore]
		public string MOTHER_OFGN{ get; set;}
		//======
	}
}

