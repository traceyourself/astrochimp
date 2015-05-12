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
		UIKit.UIButton FirstMatchPic { get; set; }

		[Outlet]
		UIKit.UITableView RelationshipMatchTable { get; set; }

		[Outlet]
		UIKit.UIButton SecondMatchPic { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (SecondMatchPic != null) {
				SecondMatchPic.Dispose ();
				SecondMatchPic = null;
			}

			if (FirstMatchPic != null) {
				FirstMatchPic.Dispose ();
				FirstMatchPic = null;
			}

			if (RelationshipMatchTable != null) {
				RelationshipMatchTable.Dispose ();
				RelationshipMatchTable = null;
			}
		}
	}
}
