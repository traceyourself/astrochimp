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
	[Register ("RelationshipMatchDetailCell")]
	partial class RelationshipMatchDetailCell
	{
		[Outlet]
		UIKit.UILabel MatchDegreeLabel { get; set; }

		[Outlet]
		UIKit.UILabel NameLabel { get; set; }

		[Outlet]
		UIKit.UILabel YearLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (YearLabel != null) {
				YearLabel.Dispose ();
				YearLabel = null;
			}

			if (NameLabel != null) {
				NameLabel.Dispose ();
				NameLabel = null;
			}

			if (MatchDegreeLabel != null) {
				MatchDegreeLabel.Dispose ();
				MatchDegreeLabel = null;
			}
		}
	}
}
