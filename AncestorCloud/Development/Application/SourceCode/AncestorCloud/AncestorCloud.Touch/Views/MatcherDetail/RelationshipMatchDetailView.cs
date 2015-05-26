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
			this.Title = Utility.LocalisedBundle ().LocalizedString ("MatchText","");
			this.NavigationController.NavigationBar.TintColor=Themes.TitleTextColor();


			CenterImage.Layer.CornerRadius = 50f;
			CenterImage.ClipsToBounds = true;

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

			UIImage image = UIImage.FromFile (StringConstants.MATCHICON);

			image = image.ImageWithRenderingMode (UIImageRenderingMode.AlwaysOriginal);

			this.NavigationItem.SetRightBarButtonItem(
				new UIBarButtonItem(image
					, UIBarButtonItemStyle.Plain
					, (sender,args) => {
						ViewModel.ShowPastMatchesNoData();


					})
				, true);
			//Add (table);
		}

		#endregion

		#region DATABINDING
		void BindData()
		{
			var source = new RelationshipMatchTableSource (RelationshipMatchTable);

			AppDelegate appDelegate = (AppDelegate)UIApplication.SharedApplication.Delegate;
			//var source = new MvxSimpleTableViewSource(fbFamilyTableView, FbFamilyCell.Key, FbFamilyCell.Key);
			RelationshipMatchTable.Source = source;

			RelationshipMatchTable.Delegate = new RelationshipMatchTableDelegate ();

			MvxImageViewLoader _imageViewLoader = new MvxImageViewLoader(() => this._FirstMatchPic);
			MvxImageViewLoader _secImageViewLoader = new MvxImageViewLoader(() => this._SecondMatchPic);

			var set = this.CreateBindingSet<RelationshipMatchDetailView , RelationshipMatchDetailViewModel > ();
			set.Bind (source).To (vm => vm.MatchResultList);
			set.Bind (FirstPersonName).To (vm => vm.FirstPersonNAME);
			set.Bind (SecondPersonName).To (vm => vm.SecondPersonNAME);

			//TODO: image not uploaded apply condition:=

			if ((ViewModel.FirstPersonTag != null) &&( ViewModel.FirstPersonTag.Equals (AppConstant.METAGKEY))) {
				_FirstMatchPic.Image = appDelegate.UIImageProfilePic;
			} else {
				set.Bind (_imageViewLoader).To (vm => vm.FirstPersonURL);
			}
			if ((ViewModel.SecondPersonTag != null)  && (ViewModel.SecondPersonTag.Equals (AppConstant.METAGKEY))) {
				_SecondMatchPic.Image = appDelegate.UIImageProfilePic;
			} else {
				set.Bind (_secImageViewLoader).To (vm => vm.SecondPersonURL);
			}
				
			set.Bind (DegreeLabel).To (vm => vm.MatchResult.Degrees).WithConversion (new DegreeConverter (), null);


			set.Apply ();

			//TODO: Change this condition

			if (ViewModel.MatchResultList[0].CommonResult != null) {
				RelationshipMatchTable.BackgroundColor = Themes.MatchTableView();
				
			}

		}
		#endregion



	}
}

