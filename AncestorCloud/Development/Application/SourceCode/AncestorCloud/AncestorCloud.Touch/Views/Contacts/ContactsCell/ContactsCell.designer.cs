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
	[Register ("ContactsCell")]
	partial class ContactsCell
	{
		[Outlet]
		UIKit.UILabel NameLabel { get; set; }

		[Action ("PlusbuttonTapped:")]
		partial void PlusbuttonTapped (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (NameLabel != null) {
				NameLabel.Dispose ();
				NameLabel = null;
			}
		}
	}
}
