
using System;

using Foundation;
using UIKit;
using AncestorCloud.Shared;
using Cirrious.MvvmCross.Binding.BindingContext;

namespace AncestorCloud.Touch
{
	public partial class CelebritiesView : BaseViewController
	{
		public CelebritiesView () : base ("CelebritiesView", null)
		{
		}

		public new CelebritiesViewModel ViewModel
		{
			get { return base.ViewModel as CelebritiesViewModel; }
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
			var source = new CelebritiesTableSource (CelebritiesTableVIew);
			//var source = new MvxSimpleTableViewSource(fbFamilyTableView, FbFamilyCell.Key, FbFamilyCell.Key);
			CelebritiesTableVIew.Source = source;

			var set = this.CreateBindingSet<CelebritiesView , CelebritiesViewModel> ();
			set.Bind (source).To (vm => vm.CelebritiesList);
			//set.Bind (NextButton).To (vm => vm.NextButtonCommand);
			set.Apply ();
			//this.NavigationController.NavigationBarHidden = true;

		}
	}
}

