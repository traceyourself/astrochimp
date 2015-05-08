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
	[Register ("MatchView")]
	partial class MatchView
	{
		[Outlet]
		UIKit.UIView ContentView { get; set; }

		[Outlet]
		UIKit.UIScrollView scrollViewObj { get; set; }

		[Action ("FirstImageButtonTapped:")]
		partial void FirstImageButtonTapped (Foundation.NSObject sender);

		[Action ("FirstImageTapped:")]
		partial void FirstImageTapped (Foundation.NSObject sender);

		[Action ("MatchTapped:")]
		partial void MatchTapped (Foundation.NSObject sender);

		[Action ("SecondButtonImageTapped:")]
		partial void SecondButtonImageTapped (Foundation.NSObject sender);

		[Action ("SecondImageTapped:")]
		partial void SecondImageTapped (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (ContentView != null) {
				ContentView.Dispose ();
				ContentView = null;
			}

			if (scrollViewObj != null) {
				scrollViewObj.Dispose ();
				scrollViewObj = null;
			}
		}
	}
}
