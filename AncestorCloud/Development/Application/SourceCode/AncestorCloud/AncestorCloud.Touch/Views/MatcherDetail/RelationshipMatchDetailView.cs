using System;
using Foundation;
using UIKit;
using Cirrious.CrossCore;
using System.Linq;
using Cirrious.MvvmCross.Plugins.Messenger;
using AncestorCloud.Shared;
using AncestorCloud.Shared.ViewModels;
using System.Drawing;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Binding.Touch.Views;

namespace AncestorCloud.Touch
{
	public partial class RelationshipMatchDetailView : BaseViewController
	{

		String firstPersonImage = "",secondPersonImage = "";
		String firstPersonName="",secondPersonName="";


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
			SetRelationShipDetailView();
			BindData ();
			
			// Perform any additional setup after loading the view, typically from a nib.
		}

		public override void ViewWillDisappear (bool animated)
		{

			if (!NavigationController.ViewControllers.Contains (this)) {
				var messenger = Mvx.Resolve<IMvxMessenger> ();
				messenger.Publish (new NavigationBarHiddenMessage (this, true)); 

			}
			base.ViewWillDisappear (animated);
		}
		public override void ViewWillAppear (bool animated)
		{
			this.NavigationController.NavigationBarHidden = false;
		}


		#region Relationship table

		private void SetRelationShipDetailView()
		{
			this.Title = "Cousin Match";
			this.NavigationController.NavigationBar.TintColor=UIColor.FromRGB(255,255,255);


			_FirstMatchPic.Layer.CornerRadius = 40f;
			_FirstMatchPic.ClipsToBounds = true;
//			MvxImageViewLoader _imageViewLoader = new MvxImageViewLoader(() => this._FirstMatchPic);
//
//			firstPersonImage = ViewModel.FirstPersonURL;
//			_imageViewLoader.ImageUrl = firstPersonImage;
//
//
//			firstPersonName = ViewModel.FirstPersonNAME;




			_SecondMatchPic.Layer.CornerRadius = 40f;
			_SecondMatchPic.ClipsToBounds = true;
//
//			MvxImageViewLoader _secImageViewLoader = new MvxImageViewLoader(() => this._SecondMatchPic);
//
//			//secondPersonImage = ViewModel.SecondPersonURL;
//			_secImageViewLoader.ImageUrl = ViewModel.SecondPersonURL;
//
//			secondPersonName = ViewModel.SecondPersonNAME;
			float width = (float)UIScreen.MainScreen.ApplicationFrame.Size.Width;


			if (width <= 320f) 
			{
				this.NavigationItem.TitleView = new MyMatchTitleView (this.Title,new RectangleF(0,0,130,20));
			}else if (width >= 321f && width <=375) {
				this.NavigationItem.TitleView = new MyMatchTitleView (this.Title, new RectangleF (0, 0, 150, 20));
			} else {
				this.NavigationItem.TitleView = new MyMatchTitleView (this.Title, new RectangleF (0, 0, 180, 20));
			}

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

		#region DATABINDING
		void BindData()
		{
			var source = new RelationshipMatchTableSource (RelationshipMatchTable);
			//var source = new MvxSimpleTableViewSource(fbFamilyTableView, FbFamilyCell.Key, FbFamilyCell.Key);
			RelationshipMatchTable.Source = source;

			RelationshipMatchTable.Delegate = new RelationshipMatchTableDelegate ();

			MvxImageViewLoader _imageViewLoader = new MvxImageViewLoader(() => this._FirstMatchPic);
			MvxImageViewLoader _secImageViewLoader = new MvxImageViewLoader(() => this._SecondMatchPic);

			var set = this.CreateBindingSet<RelationshipMatchDetailView , RelationshipMatchDetailViewModel > ();
			set.Bind (source).To (vm => vm.MatchResultList);
			set.Bind (FirstPersonName).To (vm => vm.FirstPersonNAME);
			set.Bind (SecondPersonName).To (vm => vm.SecondPersonNAME);
			set.Bind (_imageViewLoader).To (vm => vm.FirstPersonURL);
			set.Bind (_secImageViewLoader).To (vm => vm.SecondPersonURL);
			set.Bind (DegreeLabel).To (vm => vm.MatchResult.Degrees);//.WithConversion(new DegreeConverter(),null);;
			//System.Diagnostics.Debug.WriteLine (ViewModel.MatchResultList);
			set.Apply ();

		}
		#endregion



	}
}

