// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace AncestorCloud.Touch
{
	[Register ("ContactsView")]
	partial class ContactsView
	{
		[Outlet]
		UIKit.UITableView ContactsTableView { get; set; }

		[Outlet]
		UIKit.UIButton MeImage { get; set; }

		[Action ("AddButtonTapped:")]
		partial void AddButtonTapped (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (ContactsTableView != null) {
				ContactsTableView.Dispose ();
				ContactsTableView = null;
			}

			if (MeImage != null) {
				MeImage.Dispose ();
				MeImage = null;
			}
		}
	}
}
