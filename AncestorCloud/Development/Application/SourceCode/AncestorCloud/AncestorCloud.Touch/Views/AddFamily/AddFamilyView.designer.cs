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
	[Register ("AddFamilyView")]
	partial class AddFamilyView
	{
		[Outlet]
		UIKit.UIButton AddButton { get; set; }

		[Outlet]
		UIKit.UITextField BirthLocationTextField { get; set; }

		[Outlet]
		UIKit.UIButton BirthYearButton { get; set; }

		[Outlet]
		UIKit.UIView container { get; set; }

		[Outlet]
		UIKit.UITextField FirstNameTextField { get; set; }

		[Outlet]
		UIKit.UISegmentedControl GenderSegmentControl { get; set; }

		[Outlet]
		UIKit.UITextField LastNameTextField { get; set; }

		[Outlet]
		UIKit.UITextField MiddleNameTextFeild { get; set; }

		[Outlet]
		UIKit.UIButton PickerButtonTapped { get; set; }

		[Outlet]
		UIKit.UILabel PickerLabel { get; set; }

		[Outlet]
		UIKit.UILabel RefernceLabel { get; set; }

		[Outlet]
		UIKit.UISegmentedControl RefernceSegmentControl { get; set; }

		[Action ("_RefenceSegmentControl:")]
		partial void _RefenceSegmentControl (Foundation.NSObject sender);

		[Action ("AddButtonTapped:")]
		partial void AddButtonTapped (Foundation.NSObject sender);

		[Action ("Birthlabel:")]
		partial void Birthlabel (Foundation.NSObject sender);

		[Action ("GenderSegmentControlChanged:")]
		partial void GenderSegmentControlChanged (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (RefernceLabel != null) {
				RefernceLabel.Dispose ();
				RefernceLabel = null;
			}

			if (RefernceSegmentControl != null) {
				RefernceSegmentControl.Dispose ();
				RefernceSegmentControl = null;
			}

			if (AddButton != null) {
				AddButton.Dispose ();
				AddButton = null;
			}

			if (BirthLocationTextField != null) {
				BirthLocationTextField.Dispose ();
				BirthLocationTextField = null;
			}

			if (BirthYearButton != null) {
				BirthYearButton.Dispose ();
				BirthYearButton = null;
			}

			if (container != null) {
				container.Dispose ();
				container = null;
			}

			if (FirstNameTextField != null) {
				FirstNameTextField.Dispose ();
				FirstNameTextField = null;
			}

			if (GenderSegmentControl != null) {
				GenderSegmentControl.Dispose ();
				GenderSegmentControl = null;
			}

			if (LastNameTextField != null) {
				LastNameTextField.Dispose ();
				LastNameTextField = null;
			}

			if (MiddleNameTextFeild != null) {
				MiddleNameTextFeild.Dispose ();
				MiddleNameTextFeild = null;
			}

			if (PickerButtonTapped != null) {
				PickerButtonTapped.Dispose ();
				PickerButtonTapped = null;
			}

			if (PickerLabel != null) {
				PickerLabel.Dispose ();
				PickerLabel = null;
			}
		}
	}
}
