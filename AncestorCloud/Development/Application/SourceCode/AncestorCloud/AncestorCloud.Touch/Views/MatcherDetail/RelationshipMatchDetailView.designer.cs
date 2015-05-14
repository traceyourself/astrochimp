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
	[Register ("RelationshipMatchDetailView")]
	partial class RelationshipMatchDetailView
	{
		[Outlet]
		UIKit.UIImageView _FirstMatchPic { get; set; }

		[Outlet]
		UIKit.UIImageView _SecondMatchPic { get; set; }

		[Outlet]
		UIKit.UILabel DegreeLabel { get; set; }

		[Outlet]
		UIKit.UILabel FirstPersonName { get; set; }

		[Outlet]
		UIKit.UITableView RelationshipMatchTable { get; set; }

		[Outlet]
		UIKit.UILabel SecondPersonName { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (DegreeLabel != null) {
				DegreeLabel.Dispose ();
				DegreeLabel = null;
			}

			if (_FirstMatchPic != null) {
				_FirstMatchPic.Dispose ();
				_FirstMatchPic = null;
			}

			if (_SecondMatchPic != null) {
				_SecondMatchPic.Dispose ();
				_SecondMatchPic = null;
			}

			if (FirstPersonName != null) {
				FirstPersonName.Dispose ();
				FirstPersonName = null;
			}

			if (RelationshipMatchTable != null) {
				RelationshipMatchTable.Dispose ();
				RelationshipMatchTable = null;
			}

			if (SecondPersonName != null) {
				SecondPersonName.Dispose ();
				SecondPersonName = null;
			}
		}
	}
}
