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
    [Register ("LoaderView")]
    partial class LoaderView
    {
        [Outlet]
        UIKit.UIActivityIndicatorView ActivityLoader { get; set; }


        [Outlet]
        UIKit.UIView BlackView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ActivityLoader != null) {
                ActivityLoader.Dispose ();
                ActivityLoader = null;
            }

            if (BlackView != null) {
                BlackView.Dispose ();
                BlackView = null;
            }
        }
    }
}