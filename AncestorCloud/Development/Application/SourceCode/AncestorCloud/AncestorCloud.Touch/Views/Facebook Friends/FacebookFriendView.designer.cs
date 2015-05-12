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
	[Register ("FacebookFriendView")]
	partial class FacebookFriendView
	{
		[Outlet]
		UIKit.UITableView FacebookFriendTableView { get; set; }

		[Outlet]
		UIKit.UIButton MeImage { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (MeImage != null) {
				MeImage.Dispose ();
				MeImage = null;
			}

			if (FacebookFriendTableView != null) {
				FacebookFriendTableView.Dispose ();
				FacebookFriendTableView = null;
			}
		}
	}
}
