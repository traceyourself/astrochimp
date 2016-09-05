// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace AncestorCloud.Touch
{
    [Register ("FbFamilyView")]
    partial class FbFamilyView
    {
        [Outlet]
        UIKit.UILabel FamilyText { get; set; }


        [Outlet]
        UIKit.UITableView fbFamilyTableView { get; set; }


        [Outlet]
        UIKit.UIImageView FbIcon { get; set; }


        [Outlet]
        UIKit.UIButton HelpButton { get; set; }


        [Outlet]
        UIKit.UIButton NextButton { get; set; }


        [Outlet]
        UIKit.UILabel RelationText { get; set; }


        [Action ("HelpButtonTapped:")]
        partial void HelpButtonTapped (Foundation.NSObject sender);

        void ReleaseDesignerOutlets ()
        {
            if (FamilyText != null) {
                FamilyText.Dispose ();
                FamilyText = null;
            }

            if (fbFamilyTableView != null) {
                fbFamilyTableView.Dispose ();
                fbFamilyTableView = null;
            }

            if (FbIcon != null) {
                FbIcon.Dispose ();
                FbIcon = null;
            }

            if (HelpButton != null) {
                HelpButton.Dispose ();
                HelpButton = null;
            }

            if (NextButton != null) {
                NextButton.Dispose ();
                NextButton = null;
            }

            if (RelationText != null) {
                RelationText.Dispose ();
                RelationText = null;
            }
        }
    }
}