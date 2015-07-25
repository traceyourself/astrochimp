using System;
using Foundation;
using UIKit;
using Cirrious.MvvmCross.Touch.Views;
using AncestorCloud.Shared.ViewModels;
using System.Collections.Generic;
using FlyoutNavigation;
using CrossUI.Touch.Dialog.Elements;
using Cirrious.MvvmCross.Plugins.Messenger;
using AncestorCloud.Shared;
using Cirrious.CrossCore;
using System.Linq;
using CoreGraphics;

namespace AncestorCloud.Touch
{
     public partial class HomePage : BaseViewController

	{
		
	
//		private MvxSubscriptionToken navigationMenuToggleToken;
//		private MvxSubscriptionToken navigationBarHiddenToken;
		UICollectionView collection;
		List<ICollectionView> collectionView;
		static readonly NSString collectionCellId = new NSString ("CollectionCell");

		private MvxSubscriptionToken navigationBarHiddenToken;

		public HomePage () : base ("HomePage", null)
		{
		}

		public new HomePageViewModel ViewModel
		{
			get { return base.ViewModel as HomePageViewModel; }
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
			CreateCollectionView ();
			collection.RegisterClassForCell (typeof(AncestorCloud.Touch.HomePageCollectionSource.CollectionCell), collectionCellId);
			// Perform any additional setup after loading the view, typically from a nib.
//			var messenger = Mvx.Resolve<IMvxMessenger>();
//			navigationBarHiddenToken = messenger.SubscribeOnMainThread<NavigationBarHiddenMessage>(message => NavigationController.NavigationBarHidden = message.NavigationBarHidden);

			AdjustFrames ();
		}

	   partial void loginClicked (NSObject sender)
		{
			
			ViewModel.ShowLoginViewModel();
			ViewModel.Close();
		}

		partial void signUpClicked (NSObject sender)
		{
				ViewModel.ShowSignViewModel();
				ViewModel.Close();

		}

		#region CollectionView

		private void CreateCollectionView()
		{
			collection = collectionViewObj;
			List<ICollectionView> list = GetData ();
			collection.Source = new HomePageCollectionSource(list);
			collection.Delegate = new HomePageCollectionViewDelegate (pageObj,list);
			this.NavigationController.NavigationBarHidden = false;
//			this.NavigationController.NavigationBar.BarTintColor = UIColor.FromRGB (255, 255, 255);
//			this.NavigationItem.SetHidesBackButton (true, false);
		}

		public string[] Images()
		{
			string[] array = new string[4];
			array [0] = StringConstants.HOMEPAGEFIRSTIMAGE;
			array [1] = StringConstants.HOMEPAGESECIMAGE;
			array [2] = StringConstants.HOMEPAGETHIRDTIMAGE;
			array [3] = StringConstants.HOMEPAGEFOURTHIMAGE;

			return array;
		}

		public List<ICollectionView> GetData ()
		{
			List<ICollectionView> collectionViewList = new List<ICollectionView> ();
			for (int i = 0; i < 4; i++) {
				collectionViewList.Add (new PictureLibrary (Images()[i]));
			}



			return collectionViewList;
		}

		#endregion
		public override void ViewWillAppear (bool animated)
		{
			this.NavigationController.NavigationBarHidden = true;

			var messenge = Mvx.Resolve<IMvxMessenger>();
			messenge.Publish(new FlyOutCloseMessage(this));


//			AppDelegate _delagate = (AppDelegate) UIApplication.SharedApplication.Delegate;
//			_delagate.UIImageProfilePic = null;
		}

		void AdjustViews()
		{
			float xOffset = 20;

			if(UIScreen.MainScreen.ApplicationFrame.Width <=320){
				xOffset = 0;
			}

			collectionViewObj.Center = new CGPoint(View.Frame.Width/2,collectionViewObj.Center.Y); 
			//collectionViewObj.BackgroundColor = UIColor.Green;

			CGRect lblFrame = swipeToLearnLabel.Frame;
			lblFrame.Y = collectionViewObj.Frame.Y + collectionViewObj.Frame.Height;
			lblFrame.X -= xOffset;
			swipeToLearnLabel.Frame = lblFrame; 

			CGRect pageRect = pageObj.Frame;
			pageRect.Y = swipeToLearnLabel.Frame.Y + swipeToLearnLabel.Frame.Height + 3;
			pageRect.X -= xOffset+ xOffset/2;
			pageObj.Frame = pageRect; 

			CGRect signUpRect = signUpButton.Frame;
			signUpRect.Y = pageObj.Frame.Y + pageObj.Frame.Height + 40;
			signUpRect.X -= xOffset;
			signUpButton.Frame = signUpRect;

			CGRect loginRect = loginButton.Frame;
			loginRect.Y = signUpButton.Frame.Y + signUpButton.Frame.Height + 5;
			loginRect.X -= xOffset;
			loginButton.Frame = loginRect;

		}

		void AdjustFrames()
		{
			if (UIScreen.MainScreen.ApplicationFrame.Width <= 320) {
				CGRect lblFrame = collectionViewObj.Frame;
				lblFrame.X -= 26;
				collectionViewObj.Frame = lblFrame; 
			}

			if (UIScreen.MainScreen.ApplicationFrame.Width > 375) {
				CGRect lblFrame = collectionViewObj.Frame;
				lblFrame.X += 20;
				collectionViewObj.Frame = lblFrame; 
			}

			if(App.IsAutoLogin)
				AdjustViews ();
		}
	}
}


