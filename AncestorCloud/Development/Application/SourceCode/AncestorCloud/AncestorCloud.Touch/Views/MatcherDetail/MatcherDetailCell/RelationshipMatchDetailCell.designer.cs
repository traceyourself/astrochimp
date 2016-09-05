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
            if (MatchDegreeLabel != null) {
                MatchDegreeLabel.Dispose ();
                MatchDegreeLabel = null;
            }

            if (NameLabel != null) {
                NameLabel.Dispose ();
                NameLabel = null;
            }

            if (YearLabel != null) {
                YearLabel.Dispose ();
                YearLabel = null;
            }
        }
    }
}