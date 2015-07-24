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

		private FamilyType _famType { get; set;}

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
	
		public MyFamilyTableSource (UITableView tableView, FamilyType type): base(tableView)
		{
			tableView.RegisterNibForCellReuse(UINib.FromName("MyFamilyTableCell", NSBundle.MainBundle),
				MyFamilyTableCell.Key);
			_famType = type;
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
				BackgroundColor=UIColor.FromRGB(239,239,239),
				Text="   "+ListItems[(int)section].SectionHeader.ToUpper(),
				Font= UIFont.FromName("Helvetica-Bold", 14f),
				TextColor=UIColor.DarkGray,
				TextAlignment=UITextAlignment.Left,

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
				BackgroundColor=UIColor.FromRGB(248,183,21),
				Frame= new CoreGraphics.CGRect(0,0,tableView.Frame.Size.Width+30,40),//UIColor.White,
			};
				
			btn.SetTitle( "ADD "+ListItems[(int)section].SectionFooter.ToUpper(),UIControlState.Normal);
			btn.SetTitleColor(UIColor.White,UIControlState.Normal);//UIColor.FromRGB(40,141,152)
			btn.HorizontalAlignment = UIControlContentHorizontalAlignment.Center;
			btn.Font = UIFont.FromName ("Helvetica-Bold", 14f);

			btn.TouchUpInside += (object sender, EventArgs e) => {

				if (FooterClickedDelegate != null) {
					FooterClickedDelegate (ListItems[(int)section]);
				}

			};

			UIImageView _imageView = new UIImageView(UIImage.FromBundle("add_icon.png"));
			_imageView.Frame = new CoreGraphics.CGRect (tableView.Frame.Size.Width/2 - 60, 8,25,25);
				

			UIView footerView = new UIView(new CoreGraphics.CGRect(0,0,tableView.Frame.Size.Width,40));
			footerView.BackgroundColor = UIColor.FromRGB (248, 183, 21);
			footerView.AddSubview (btn);
			footerView.AddSubview (_imageView);
				

			return footerView;

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


