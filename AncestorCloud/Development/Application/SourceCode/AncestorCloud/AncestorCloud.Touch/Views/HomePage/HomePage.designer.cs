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
	[Register ("HomePage")]
	partial class HomePage
	{
		[Outlet]
		UIKit.UICollectionView collectionViewObj { get; set; }

		[Outlet]
		UIKit.UIPageControl pageObj { get; set; }

		[Action ("loginClicked:")]
		partial void loginClicked (Foundation.NSObject sender);

		[Action ("signUpClicked:")]
		partial void signUpClicked (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (collectionViewObj != null) {
				collectionViewObj.Dispose ();
				collectionViewObj = null;
			}

			if (pageObj != null) {
				pageObj.Dispose ();
				pageObj = null;
			}
		}
	}
}
