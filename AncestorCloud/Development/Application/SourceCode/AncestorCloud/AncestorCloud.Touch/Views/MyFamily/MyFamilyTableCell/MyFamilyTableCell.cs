using System;
using Foundation;
using UIKit;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Plugins.Messenger;

namespace AncestorCloud.Touch
{
	public partial class MyFamilyTableCell : UITableViewCell
	{
		public static readonly UINib Nib = UINib.FromName ("MyFamilyTableCell", NSBundle.MainBundle);

		public static readonly NSString Key = new NSString ("MyFamilyTableCell");

		readonly string[] tableItems;

		public Action<object> EditButtonClicked
		{ get; set; }

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

//			if(EditButtonClicked!=null)
//			{
//				EditButtonClicked(sender);
//			}

			var messenger = Mvx.Resolve<IMvxMessenger> ();
			messenger.Publish (new MyTableCellTappedMessage (this));
		}
	}


}

