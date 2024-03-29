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
	[Register ("EditFamilyView")]
	partial class EditFamilyView
	{
		[Outlet]
		UIKit.UITextField BirthLocationField { get; set; }

		[Outlet]
		UIKit.UIView container { get; set; }

		[Outlet]
		UIKit.UILabel EditLabel { get; set; }

		[Outlet]
		UIKit.UITextField FirstNameTextField { get; set; }

		[Outlet]
		UIKit.UISegmentedControl GenderSegment { get; set; }

		[Outlet]
		UIKit.UITextField LastNameTextField { get; set; }

		[Outlet]
		UIKit.UITextField MiddleNameTextField { get; set; }

		[Outlet]
		UIKit.UIButton PickerButtonTapped { get; set; }

		[Action ("CrossButtonTapped:")]
		partial void CrossButtonTapped (Foundation.NSObject sender);

		[Action ("GenderSegmentChanged:")]
		partial void GenderSegmentChanged (Foundation.NSObject sender);

		[Action ("SaveButtonTapped:")]
		partial void SaveButtonTapped (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (container != null) {
				container.Dispose ();
				container = null;
			}

			if (BirthLocationField != null) {
				BirthLocationField.Dispose ();
				BirthLocationField = null;
			}

			if (EditLabel != null) {
				EditLabel.Dispose ();
				EditLabel = null;
			}

			if (FirstNameTextField != null) {
				FirstNameTextField.Dispose ();
				FirstNameTextField = null;
			}

			if (GenderSegment != null) {
				GenderSegment.Dispose ();
				GenderSegment = null;
			}

			if (LastNameTextField != null) {
				LastNameTextField.Dispose ();
				LastNameTextField = null;
			}

			if (MiddleNameTextField != null) {
				MiddleNameTextField.Dispose ();
				MiddleNameTextField = null;
			}

			if (PickerButtonTapped != null) {
				PickerButtonTapped.Dispose ();
				PickerButtonTapped = null;
			}
		}
	}
}
