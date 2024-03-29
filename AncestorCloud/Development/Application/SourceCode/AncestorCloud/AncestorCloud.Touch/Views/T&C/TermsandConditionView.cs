﻿
using System;

using Foundation;
using UIKit;
using Cirrious.MvvmCross.Touch.Views;
using AncestorCloud.Shared.ViewModels;

namespace AncestorCloud.Touch
{
	public partial class TermsandConditionView : BaseViewController,IMvxModalTouchView
	{
		public TermsandConditionView () : base ("Terms_ConditionView", null)
		{
		}
		public new TermsandConditionViewModel ViewModel
		{
			get { return base.ViewModel as TermsandConditionViewModel; }
			set { base.ViewModel = value; }
		}
		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			setTerms_Condition ();

			this.Title = Utility.LocalisedBundle ().LocalizedString ("T&CText", "");
			this.NavigationController.NavigationBar.Hidden = false;
			this.NavigationController.NavigationBar.BarTintColor = Themes.NavBarTintColor();

			UIImage image = UIImage.FromFile (StringConstants.WHITECROSS);

			image = image.ImageWithRenderingMode (UIImageRenderingMode.AlwaysOriginal);

			this.NavigationItem.SetLeftBarButtonItem(
				new UIBarButtonItem(image
					, UIBarButtonItemStyle.Plain
					, (sender,args) => {
						this.NavigationController.DismissViewController(false,null);
						ViewModel.Close();
						ViewModel.ShowSignUpView();

					})
				, true);
			
			// Perform any additional setup after loading the view, typically from a nib.
		}

		public void setTerms_Condition()
		{
			//this.NavigationController.NavigationBarHidden= true;
		}

//		partial void CrossButtonTapped (NSObject sender)
//		{
//			//ViewModel.Close();
//			this.NavigationController.DismissViewController(false,null);
//			ViewModel.Close();
//			ViewModel.ShowSignUpView();
//
//		}
	}
}

