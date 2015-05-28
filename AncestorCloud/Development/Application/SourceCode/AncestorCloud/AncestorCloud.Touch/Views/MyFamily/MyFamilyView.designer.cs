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
	[Register ("MyFamilyView")]
	partial class MyFamilyView
	{
		[Outlet]
		UIKit.UITableView myFamilyTable { get; set; }

		[Outlet]
		UIKit.UILabel PercentageLabel { get; set; }

		[Action ("CrossButtonTaped:")]
		partial void CrossButtonTaped (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (PercentageLabel != null) {
				PercentageLabel.Dispose ();
				PercentageLabel = null;
			}

			if (myFamilyTable != null) {
				myFamilyTable.Dispose ();
				myFamilyTable = null;
			}
		}
	}
}
