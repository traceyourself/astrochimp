using System;
using UIKit;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AncestorCloud.Shared;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Foundation;

namespace AncestorCloud.Touch
{
	public class MyFamilyTableSource :  MvxTableViewSource  
	{

		readonly string cellIdentifier = "MyFamilyTableCell";
//		Dictionary<string, List<TableItem>> indexedTableItems;
//		string[] keys;
		private List<TableItem> ListItems;

		public Action<object> FooterClickedDelegate
		{ get; set; }

		public new List<TableItem> ItemsSource
		{
			get
			{
				return ListItems;
			}
			set
			{
				ListItems = value;
				ReloadTableData();
			}
		}
	
		public MyFamilyTableSource (UITableView tableView): base(tableView)
		{
			tableView.RegisterNibForCellReuse(UINib.FromName("MyFamilyTableCell", NSBundle.MainBundle),
				MyFamilyTableCell.Key);

		}

		public override nint NumberOfSections(UITableView tableView)
		{
			if (ListItems == null)
				return 0;

			return ListItems.Count;
		}
			
		 
		public override nint RowsInSection (UITableView tableView, nint section)
		{
			
			return ListItems[(int)section].DataItems.Count;
		}
			
		protected override object GetItemAt(NSIndexPath indexPath)
		{
			if (ListItems == null)
				return null;

			return ListItems[indexPath.Section].DataItems[indexPath.Row];
		}

		protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
		{
			MyFamilyTableCell cell = (MyFamilyTableCell)tableView.DequeueReusableCell (cellIdentifier);
			// if there are no cells to reuse, create a new one
			if (cell == null)
				cell =  MyFamilyTableCell.Create() ;
        
			TableItem tableItem = ListItems [indexPath.Section];

			People childItem = tableItem.DataItems[indexPath.Row];

			cell.familyMember = childItem;
            
			if (cell == null)
			{
				cell = (MyFamilyTableCell)new UITableViewCell (UITableViewCellStyle.Default , cellIdentifier); 
			}


			cell.SetName ();
//			System.Diagnostics.Debug.WriteLine ("Section : "+tableItem.SectionHeader);
//			System.Diagnostics.Debug.WriteLine ("Data : "+childItem.Name);
//			System.Diagnostics.Debug.WriteLine ("Section Footer:" + tableItem.SectionFooter);

			//tableView.BackgroundColor = UIColor.FromRGB (178,45,116);

			return cell;

		}

		public override UIView GetViewForHeader (UITableView tableView, nint section)
		{
			UILabel view = new UILabel {
				BackgroundColor=UIColor.FromRGB(248,183,21),
				Text=ListItems[(int)section].SectionHeader,
				Font= UIFont.FromName("Helvetica", 16f),
				TextColor=UIColor.White,
				TextAlignment=UITextAlignment.Center
			};

			return view;
		}



		public override nfloat GetHeightForHeader (UITableView tableView, nint section)
		{
			return 35;
		}

		public override nfloat GetHeightForFooter (UITableView tableView, nint section)
		{
			return 40;
		}

		public override UIView GetViewForFooter (UITableView tableView, nint section)
		{
			UIButton btn = new UIButton
			{
				BackgroundColor=UIColor.White,
			};

			btn.SetTitle( "   + Add "+ListItems[(int)section].SectionFooter,UIControlState.Normal);
			btn.SetTitleColor(UIColor.FromRGB(178,45,116),UIControlState.Normal);
			btn.HorizontalAlignment = UIControlContentHorizontalAlignment.Left;
			btn.Font = UIFont.FromName ("Helvetica", 14f);

			btn.TouchUpInside += (object sender, EventArgs e) => {
				System.Diagnostics.Debug.WriteLine("Add Button Tapped");

				if (FooterClickedDelegate != null) {
					FooterClickedDelegate (ListItems[(int)section]);
				}

			};
			return btn;

		}

	}



	public class MyTableViewDelegate : UITableViewDelegate
	{

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			//throw new System.NotImplementedException ();
		}

	}
}


