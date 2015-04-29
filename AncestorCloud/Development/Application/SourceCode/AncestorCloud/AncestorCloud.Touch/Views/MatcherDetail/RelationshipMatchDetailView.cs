
using System;

using Foundation;
using UIKit;
using Cirrious.CrossCore;
using System.Linq;
using Cirrious.MvvmCross.Plugins.Messenger;
using AncestorCloud.Shared;
using AncestorCloud.Shared.ViewModels;

namespace AncestorCloud.Touch
{
	public partial class RelationshipMatchDetailView : BaseViewController
	{

		UITableView table;

		public RelationshipMatchDetailView () : base ("RelationshipMatchDetailView", null)
		{
		}


		public new RelationshipMatchDetailViewModel ViewModel
		{
			get { return base.ViewModel as RelationshipMatchDetailViewModel; }
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
			CreateRelationShipTable();
			
			// Perform any additional setup after loading the view, typically from a nib.
		}


		#region Relationship table

		private void CreateRelationShipTable()
		{
			this.NavigationController.NavigationBar.TintColor=UIColor.FromRGB(255,255,255);

			table = RelationshipMatchTable;//new UITableView(View.Bounds); // defaults to Plain style
			string[] tableItems = new string[] {"1972"+"   "+"Glenneth Girtrude Gates"+"  " +" 06","1956"+"   "+"Henry Wright Gates"+"          " +" 07","1925"+"   "+"Glenneth Girtrude Gates"+"  " +" 08","1907"+"   "+"Henry Wright Gates"+"          " +" 09","1925"+"   "+"Henry Wright Gates"+"          " +" 10","1907"+"   "+"Glenneth Girtrude Gates"+"  " +" 11","1925"+"   "+"Henry Wright Gates"+"          " +" 12","1921"+"   "+"Glenneth Girtrude Gates"+"  " +" 13","1972"+"   "+"Henry Wright Gates"+"          " +" 14","1956"+"   "+"Glenneth Girtrude Gates"+"  " +" 15"};
			table.Source = new RelationshipMatchTableSource(tableItems);
			this.NavigationItem.TitleView = new MyTitleView (this.Title);
			this.NavigationController.NavigationBarHidden = false;

			UIImage image = UIImage.FromFile ("cross.png");

			image = image.ImageWithRenderingMode (UIImageRenderingMode.AlwaysOriginal);

			this.NavigationItem.SetRightBarButtonItem(
				new UIBarButtonItem(image
					, UIBarButtonItemStyle.Plain
					, (sender,args) => {
						System.Diagnostics .Debug.WriteLine("PAST MATCHER");
						ViewModel.ShowPastMatches();
					})
				, true);
			//Add (table);
		}

		#endregion

		public override void ViewWillDisappear (bool animated)
		{

			if (!NavigationController.ViewControllers.Contains (this)) {
				var messenger = Mvx.Resolve<IMvxMessenger> ();
				messenger.Publish (new NavigationBarHiddenMessage (this, true)); 
			
			}
			base.ViewWillDisappear (animated);
		}

	}
}

