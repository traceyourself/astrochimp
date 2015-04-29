﻿using System;
using Foundation;
using UIKit;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Cirrious.MvvmCross.Binding.BindingContext;
using AncestorCloud.Shared.ViewModels;

namespace AncestorCloud.Touch
{
	public partial class FbFamilyView : BaseViewController
	{
		public FbFamilyView () : base ("FbFamilyView", null)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public new FbFamilyViewModel ViewModel
		{
			get { return base.ViewModel as FbFamilyViewModel; }
			set { base.ViewModel = value; }
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			SetTableView ();
			
			// Perform any additional setup after loading the view, typically from a nib.
		}


		public void SetTableView()
		{
			var source = new FbFamilyTableViewSource (fbFamilyTableView);
			//var source = new MvxSimpleTableViewSource(fbFamilyTableView, FbFamilyCell.Key, FbFamilyCell.Key);
			fbFamilyTableView.Source = source;

			var set = this.CreateBindingSet<FbFamilyView , FbFamilyViewModel> ();
			set.Bind (source).To (vm => vm.FamilyList);
			set.Bind (NextButton).To (vm => vm.NextButtonCommand);
			set.Apply ();
			this.NavigationController.NavigationBarHidden = true;

		}

	}
}

