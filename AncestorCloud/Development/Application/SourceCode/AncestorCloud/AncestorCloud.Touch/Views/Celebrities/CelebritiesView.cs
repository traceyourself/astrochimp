using System;
using Foundation;
using UIKit;
using AncestorCloud.Shared;
using Cirrious.MvvmCross.Binding.BindingContext;
using System.Drawing;

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
			Search ();
			
			// Perform any additional setup after loading the view, typically from a nib.
		}


		public void SetTableView()
		{

			this.Title="Celebrities";
			var source = new CelebritiesTableSource (CelebritiesTableVIew);
			//var source = new MvxSimpleTableViewSource(fbFamilyTableView, FbFamilyCell.Key, FbFamilyCell.Key);
			CelebritiesTableVIew.Source = source;

			UINavigationBar.Appearance.SetTitleTextAttributes (new UITextAttributes ()
				{ TextColor = UIColor.FromRGB (255,255,255) });

			this.NavigationController.NavigationBar.BarTintColor = UIColor.FromRGB (178, 45, 116);

			//	this.NavigationItem.BackBarButtonItem.TintColor = UIColor.White;
			
			this.NavigationItem.TitleView = new MyMatchTitleView (this.Title,new RectangleF(0,0,150,20));

			var set = this.CreateBindingSet<CelebritiesView , CelebritiesViewModel> ();
			set.Bind (source).To (vm => vm.CelebritiesList);
			set.Bind (SearchViewController).To (vm => vm.SearchKey).TwoWay();
			//set.Bind (NextButton).To (vm => vm.NextButtonCommand);
			set.Apply ();
			//this.NavigationController.NavigationBarHidden = true;

		}


		public void Search()
		{
			
		}
	}
}

