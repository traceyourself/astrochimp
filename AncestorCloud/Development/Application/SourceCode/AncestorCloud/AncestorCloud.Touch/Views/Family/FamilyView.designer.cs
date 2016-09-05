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
    [Register ("FamilyView")]
    partial class FamilyView
    {
        [Outlet]
        UIKit.UIView ContentView { get; set; }


        [Outlet]
        UIKit.UIScrollView ScrollViewObj { get; set; }


        [Action ("AddFamilyButtonTapped:")]
        partial void AddFamilyButtonTapped (Foundation.NSObject sender);


        [Action ("HelpButtonTaped:")]
        partial void HelpButtonTaped (Foundation.NSObject sender);

        void ReleaseDesignerOutlets ()
        {
            if (ContentView != null) {
                ContentView.Dispose ();
                ContentView = null;
            }

            if (ScrollViewObj != null) {
                ScrollViewObj.Dispose ();
                ScrollViewObj = null;
            }
        }
    }
}