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
	[Register ("FacebookFriendCell")]
	partial class FacebookFriendCell
	{
		[Outlet]
		UIKit.UIButton fbFriendImage { get; set; }

		[Outlet]
		UIKit.UILabel NameLabel { get; set; }

		[Outlet]
		UIKit.UIImageView ProfilePic { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ProfilePic != null) {
				ProfilePic.Dispose ();
				ProfilePic = null;
			}

			if (fbFriendImage != null) {
				fbFriendImage.Dispose ();
				fbFriendImage = null;
			}

			if (NameLabel != null) {
				NameLabel.Dispose ();
				NameLabel = null;
			}
		}
	}
}
