using System;
using Foundation;
using UIKit;

namespace AncestorCloud.Touch
{
	public partial class MyFamilyTableCell : UITableViewCell
	{
		public static readonly UINib Nib = UINib.FromName ("MyFamilyTableCell", NSBundle.MainBundle);

		public static readonly NSString Key = new NSString ("MyFamilyTableCell");

		readonly string[] tableItems;

		public MyFamilyTableCell (IntPtr handle) : base (handle)
		{
		}
		public MyFamilyTableCell (string[] items) 
		{
			tableItems = items;

		}

		public static MyFamilyTableCell Create ()
		{
			return (MyFamilyTableCell)Nib.Instantiate (null, null) [0];
		}

		partial void EditButtonTapped(NSObject sender)
		{
			System.Diagnostics.Debug.WriteLine ("Button Tapped");
		}
	}


}

