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
	[Register ("LoaderView")]
	partial class LoaderView
	{
		[Outlet]
		UIKit.UIActivityIndicatorView ActivityLoader { get; set; }

		[Outlet]
		UIKit.UIView BlackView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ActivityLoader != null) {
				ActivityLoader.Dispose ();
				ActivityLoader = null;
			}

			if (BlackView != null) {
				BlackView.Dispose ();
				BlackView = null;
			}
		}
	}
}
