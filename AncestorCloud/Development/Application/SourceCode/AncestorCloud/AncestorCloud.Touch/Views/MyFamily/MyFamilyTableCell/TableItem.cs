using System;

using UIKit;
using System.Collections.Generic;

namespace AncestorCloud.Touch {
	public class TableItem {


		public String SectionHeader{ get; set;}
		public String SectionFooter{ get; set;}
		public List<MyFamilySectionDataItem> DataItems{ get; set;}

		protected UITableViewCellStyle cellStyle = UITableViewCellStyle.Default;

		public UITableViewCellStyle CellStyle
		{
			get { return cellStyle; }
			set { cellStyle = value; }
		}

		public UITableViewCellAccessory CellAccessory
		{
			get { return cellAccessory; }
			set { cellAccessory = value; }
		}
		protected UITableViewCellAccessory cellAccessory = UITableViewCellAccessory.None;


		public TableItem (String heading,String footer,List<MyFamilySectionDataItem> items)
		{ 
			SectionHeader = heading; 
			SectionFooter = footer;
			DataItems = items;
		}
	}
}
