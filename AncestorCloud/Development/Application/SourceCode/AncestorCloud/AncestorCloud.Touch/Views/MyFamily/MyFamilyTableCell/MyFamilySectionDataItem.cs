using System;

namespace AncestorCloud.Touch
{
	public class MyFamilySectionDataItem
	{
		public String PersonName{ get; set;}
		public String Year{ get; set;}

		public MyFamilySectionDataItem (String name,String year)
		{
			PersonName = name;
			Year = year;
		}

	}
}

