﻿using System;
using Foundation;
using UIKit;
using Cirrious.CrossCore;
using System.Linq;
using Cirrious.MvvmCross.Plugins.Messenger;
using AncestorCloud.Shared;
using AncestorCloud.Shared.ViewModels;
using System.Drawing;
using Cirrious.MvvmCross.Binding.BindingContext;

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

			this.Title = "Matcher";
			this.NavigationController.NavigationBar.TintColor=UIColor.FromRGB(255,255,255);

			SetTableView ();

			this.NavigationItem.TitleView = new MyMatchTitleView (this.Title,new RectangleF(0,0,150,20));
			this.NavigationController.NavigationBarHidden = false;

			UIImage image = UIImage.FromFile ("clock_icon.png");

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

		#region DATABINDING

		public void SetTableView()
		{


			var source = new RelationshipMatchTableSource (RelationshipMatchTable);
			//var source = new MvxSimpleTableViewSource(fbFamilyTableView, FbFamilyCell.Key, FbFamilyCell.Key);
			RelationshipMatchTable.Source = source;

			//this.NavigationItem.TitleView = new MyPastMatchTitleView (this.Title,new RectangleF(0,0,150,20));

			var set = this.CreateBindingSet<RelationshipMatchDetailView , RelationshipMatchDetailViewModel> ();
			set.Bind (source).To (vm => vm.RelationshipMatchDetailList);
			//set.Bind (NextButton).To (vm => vm.NextButtonCommand);
			set.Apply ();
			//this.NavigationController.NavigationBarHidden = true;

		}
		#endregion

	}
}

