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
	[Register ("ProfilePicView")]
	partial class ProfilePicView
	{
		[Outlet]
		UIKit.UIButton ProfilePic { get; set; }

		[Action ("SkipButtonTapped:")]
		partial void SkipButtonTapped (Foundation.NSObject sender);

		[Action ("UploadButtonTapped:")]
		partial void UploadButtonTapped (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (ProfilePic != null) {
				ProfilePic.Dispose ();
				ProfilePic = null;
			}
		}
	}
}
