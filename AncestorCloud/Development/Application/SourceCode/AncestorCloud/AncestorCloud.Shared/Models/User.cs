using System;
using System.Collections.Generic;
using Cirrious.MvvmCross.Plugins.Sqlite;

namespace AncestorCloud.Shared
{
	public class User : BaseModel
	{
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }

		public string UserID { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string DateOfBirth { get; set; }
		public string Gender { get; set; }
		public string BirthLocation { get; set; }
		public string CurrentLocation { get; set; }
		public string RelationshipInterest { get; set; }
		public string FbLink { get; set; }
		public string UpdatedTime { get; set;}
		public string ProfilePic { get; set;}

		[Ignore] 
		public List<People> FriendList { get; set; }

	}
}

