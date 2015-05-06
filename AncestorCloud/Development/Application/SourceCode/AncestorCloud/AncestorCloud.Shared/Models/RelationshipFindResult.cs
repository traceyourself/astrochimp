using System;
using System.Collections.Generic;

namespace AncestorCloud.Shared
{
	public class RelationshipFindResult 
	{
		public int CommonIndiOgfn { get; set;}
		public int Degrees { get; set;}
		public string Description { get; set;}
		public string FirstFindDate { get; set;}
		public bool Found { get; set;}
		public int GroupOgfn { get; set;}
		public int Indi1Ogfn { get; set;}
		public int Indi2Ogfn { get; set;}
		public List<CommonMember> IndiList1 { get; set;}
		public List<CommonMember> IndiList2 { get; set;}
		public string LastFindDate { get; set;}
		public string NotFoundReason { get; set;}
		public string Type { get; set;}
		public int UserOgfn { get; set;}
	}

	public class CommonMember
	{
		public string BirthDate { get; set;}
		public string BirthPlace { get; set;}
		public string DeathDate { get; set;}
		public string DeathPlace { get; set;}
		public int FamousId { get; set;}
		public int Gender { get; set;}
		public int IndiOgfn { get; set;}
		public bool IsFamous { get; set;}
		public string Name { get; set;}
		public int RelationShipToNext { get; set;}
		public int RelationShipToPrevious { get; set;}
	}
}

