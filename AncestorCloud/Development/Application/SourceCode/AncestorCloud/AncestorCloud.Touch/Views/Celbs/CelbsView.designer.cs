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
	[Register ("PastMatchesView")]
	partial class CelbsView
	{
		[Outlet]
		UIKit.UITableView PastMatchesTableVIew { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (PastMatchesTableVIew != null) {
				PastMatchesTableVIew.Dispose ();
				PastMatchesTableVIew = null;
			}
		}
	}
}
