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
	[Register ("FamilyView")]
	partial class FamilyView
	{
		[Outlet]
		UIKit.UIView ContentView { get; set; }

		[Outlet]
		UIKit.UIScrollView ScrollViewObj { get; set; }

		[Action ("AddFamilyButtonTapped:")]
		partial void AddFamilyButtonTapped (Foundation.NSObject sender);

		[Action ("HelpButtonTaped:")]
		partial void HelpButtonTaped (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (ScrollViewObj != null) {
				ScrollViewObj.Dispose ();
				ScrollViewObj = null;
			}

			if (ContentView != null) {
				ContentView.Dispose ();
				ContentView = null;
			}
		}
	}
}
