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
    [Register ("RelationshipMatchDetailView")]
    partial class RelationshipMatchDetailView
    {
        [Outlet]
        UIKit.UIImageView _FirstMatchPic { get; set; }


        [Outlet]
        UIKit.UIImageView _SecondMatchPic { get; set; }


        [Outlet]
        UIKit.UIImageView CenterImage { get; set; }


        [Outlet]
        UIKit.UILabel DegreeLabel { get; set; }


        [Outlet]
        UIKit.UILabel FargeeLabel { get; set; }


        [Outlet]
        UIKit.UILabel FirstPersonName { get; set; }


        [Outlet]
        UIKit.UITableView RelationshipMatchTable { get; set; }


        [Outlet]
        UIKit.UILabel SecondPersonName { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (_FirstMatchPic != null) {
                _FirstMatchPic.Dispose ();
                _FirstMatchPic = null;
            }

            if (_SecondMatchPic != null) {
                _SecondMatchPic.Dispose ();
                _SecondMatchPic = null;
            }

            if (CenterImage != null) {
                CenterImage.Dispose ();
                CenterImage = null;
            }

            if (DegreeLabel != null) {
                DegreeLabel.Dispose ();
                DegreeLabel = null;
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