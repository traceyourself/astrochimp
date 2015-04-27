using System;
using UIKit;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AncestorCloud.Touch
{
	public class MyFamilyTableSource :  UITableViewDataSource {

		readonly string cellIdentifier = "MyFamilyTableCell";
		Dictionary<string, List<TableItem>> indexedTableItems;
		string[] keys;
		List<TableItem> ListItems;


	
		public MyFamilyTableSource (List<TableItem> items) 
		{
			ListItems = items;
			indexedTableItems = new Dictionary<string, List<TableItem>>();
			foreach (var t in items) {
				if (indexedTableItems.ContainsKey (t.SectionHeader)) {
					indexedTableItems[t.SectionHeader].Add(t);
				
				} else {
					indexedTableItems.Add (t.SectionHeader, new List<TableItem>() {t});

				}
			}
			keys = indexedTableItems.Keys.ToArray ();

		}
		public override nint NumberOfSections (UITableView tableView)
        {
			return keys.Length;
		}
			
		public override nint RowsInSection (UITableView tableView, nint section)
		{
			
			return ListItems[(int)section].DataItems.Count;
		}

//		public override string TitleForHeader (UITableView tableView, nint section)
//		{
//			return keys[section];
//		}

//		public override string TitleForFooter (UITableView tableView, nint section)
//		{
//			return keys [section];
//		}

		public override UITableViewCell GetCell (UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			MyFamilyTableCell cell = (MyFamilyTableCell)tableView.DequeueReusableCell (cellIdentifier);
			// if there are no cells to reuse, create a new one
			if (cell == null)
				cell =  MyFamilyTableCell.Create() ;


			/*TableItem item = indexedTableItems [keys [indexPath.Section]];

			MyFamilySectionDataItem childItem = item.DataItems[indexPath.Row];*/


			TableItem item = ListItems [indexPath.Section];

			MyFamilySectionDataItem childItem = item.DataItems[indexPath.Row];

		
			if (cell == null)
			{
				cell = (MyFamilyTableCell)new UITableViewCell (item.CellStyle, cellIdentifier); 
			}


			System.Diagnostics.Debug.WriteLine ("Section : "+item.SectionHeader);
			System.Diagnostics.Debug.WriteLine ("Data : "+childItem.PersonName);
			System.Diagnostics.Debug.WriteLine ("Section Footer:" + item.SectionFooter);

			tableView.BackgroundColor = UIColor.FromRGB (178,45,116);






			/*
			//---- set the item text
			cell.TextLabel.Text = item.Heading;

			//---- if the item has a valid image, and it's not the contact style (doesn't support images)
			if(!string.IsNullOrEmpty(item.ImageName) && item.CellStyle != UITableViewCellStyle.Value2)
			{
				if(File.Exists(item.ImageName))
				{ cell.ImageView.Image = UIImage.FromBundle(item.ImageName); }
			}

			//---- set the accessory
			cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
			*/

			return cell;

		}


	}
	#region Delegate

	public class MyFamilyTableDelegate :UITableViewDelegate
	{

		List<TableItem> data;

		string[] keys;

		Dictionary<string, List<TableItem>> indexedTableItems;

		public MyFamilyTableDelegate (List<TableItem> items) 
		{

			data = items;

			indexedTableItems = new Dictionary<string, List<TableItem>>();
			foreach (var t in items) {
				if (indexedTableItems.ContainsKey (t.SectionHeader)) {
					indexedTableItems[t.SectionHeader].Add(t);

				} else {
					indexedTableItems.Add (t.SectionHeader, new List<TableItem>() {t});

				}
			}
			keys = indexedTableItems.Keys.ToArray ();

		}

		public override UIView GetViewForHeader (UITableView tableView, nint section)
		{
			UILabel view = new UILabel {
				BackgroundColor=UIColor.FromRGB(178,45,116),
				Text=keys[section],
				Font= UIFont.FromName("Helvetica", 16f),
				TextColor=UIColor.White,
				TextAlignment=UITextAlignment.Center
     	};
				
			return view;
		}

		public override UIView GetViewForFooter (UITableView tableView, nint section)
		{

			UILabel view = new UILabel {
				BackgroundColor=UIColor.FromRGB(255,255,255),
				Text= "   +Add"+keys[section],
				Font= UIFont.FromName("Helvetica", 14f),
			};
			return view;
			
		}


		public override nfloat GetHeightForHeader (UITableView tableView, nint section)
		{
			return 25;
		}
		public override nfloat GetHeightForFooter (UITableView tableView, nint section)
		{
			return 30;
		}

		#endregion


		
	}

}


