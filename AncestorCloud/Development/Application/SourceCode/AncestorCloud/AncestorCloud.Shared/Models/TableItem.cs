using System;
using System.Collections.Generic;

namespace AncestorCloud.Shared
{
	public class TableItem
	{
			public String SectionHeader{ get; set;}
			public String SectionFooter{ get; set;}
			public String ShowSectionFooter{ get; set;}
			public String ShowSectionHeader{ get; set;}
			public List<People> DataItems{ get; set;}
			public bool isData{ get; set;}

	}

}

