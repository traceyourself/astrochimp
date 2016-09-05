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
    [Register ("PastMatchesView")]
    partial class PastMatchesView
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