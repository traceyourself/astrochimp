﻿
using System;

using Foundation;
using UIKit;
using AncestorCloud.Shared;
using Cirrious.MvvmCross.Binding.BindingContext;
using AncestorCloud.Shared.ViewModels;

namespace AncestorCloud.Touch
{
	public partial class PastMatchesView : BaseViewController
	{
		public PastMatchesView () : base ("PastMatchesView", null)
		{
		}

		public new PastMatchesViewModel ViewModel
		{
			get { return base.ViewModel as PastMatchesViewModel; }
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
			SetTableView ();
			
			// Perform any additional setup after loading the view, typically from a nib.
		}

		public void SetTableView()
		{
			var source = new CelbsTableSoure (PastMatchesTableVIew);
			//var source = new MvxSimpleTableViewSource(fbFamilyTableView, FbFamilyCell.Key, FbFamilyCell.Key);
			PastMatchesTableVIew.Source = source;

			var set = this.CreateBindingSet<PastMatchesView , PastMatchesViewModel> ();
			set.Bind (source).To (vm => vm.PastMatchesList);
			//set.Bind (NextButton).To (vm => vm.NextButtonCommand);
			set.Apply ();
			//this.NavigationController.NavigationBarHidden = true;

		}
	}
}

