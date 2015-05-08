using System;
using Foundation;
using UIKit;
using Cirrious.MvvmCross.Touch.Views;
using AncestorCloud.Shared.ViewModels;
using CrossUI.Touch.Dialog.Elements;
using System.Collections.Generic;
using Cirrious.MvvmCross.ViewModels;
using System.Drawing;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Plugins.Messenger;
using AncestorCloud.Shared;
using System.Linq;

namespace AncestorCloud.Touch
{
	public partial class MatchView : BaseViewController
	{

		String firstPersonImage = "",secondPersonImage = "";
		bool isFirstPersonSelected = false;
		bool isSecondPersonSelected = false;

		public MatchView () : base ("MatchView", null)
		{
		}

		public new MatchViewModel ViewModel
		{
			get { return base.ViewModel as MatchViewModel; }
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

//			scrollViewObj.Frame = this.View.Frame;
//			scrollViewObj.ContentSize = new SizeF(320, 550);

			float constant = 0.88f;

			float width = (float)UIScreen.MainScreen.ApplicationFrame.Size.Width;

			if (width <= 320f)
				constant = 1.0f;

			this.View.AddConstraint (NSLayoutConstraint.Create (this.ContentView, NSLayoutAttribute.Leading, 0, this.View, NSLayoutAttribute.Left, 1.0f, 0));

			this.View.AddConstraint (NSLayoutConstraint.Create (this.ContentView, NSLayoutAttribute.Trailing , 0, this.View, NSLayoutAttribute.Right, constant, 0));


			setNavigationBar ();

		}

		public void setNavigationBar()
		{


			UIImage image = UIImage.FromFile ("action_menu.png");

			image = image.ImageWithRenderingMode (UIImageRenderingMode.AlwaysOriginal);

			this.Title = "Matcher";
			UINavigationBar.Appearance.SetTitleTextAttributes (new UITextAttributes ()
				{ TextColor = UIColor.FromRGB (255,255,255) });
			this.NavigationController.NavigationBar.BarTintColor = UIColor.FromRGB (178, 45, 116);
			this.NavigationItem.SetHidesBackButton (true, false);
			this.NavigationItem.TitleView = new MyMatchTitleView (this.Title,new RectangleF(0,0,150,20));
			this.NavigationController.NavigationBarHidden = false;

			#region LeftSide Button

			this.NavigationItem.SetLeftBarButtonItem(
				new UIBarButtonItem(image
					, UIBarButtonItemStyle.Plain
					, (sender,args) => {
						
							//message to show the menu
							var messenger = Mvx.Resolve<IMvxMessenger>();
							messenger.Publish(new ToggleFlyoutMenuMessage(this));


					})
				, true);
			#endregion

			#region RightSide Button

			UIImage rightImage = UIImage.FromFile ("clock_icon.png");

			rightImage = rightImage.ImageWithRenderingMode (UIImageRenderingMode.AlwaysOriginal);

			this.NavigationItem.SetRightBarButtonItem(
				new UIBarButtonItem(rightImage
					, UIBarButtonItemStyle.Plain
					, (sender,args) => {
						ViewModel.ShowPastMatches();
						System.Diagnostics.Debug.WriteLine("PAST MATCHER");
					})
				, true);

			#endregion
		
		}


		#region MatchTapped
		partial void MatchTapped (NSObject sender)
		{

			ViewModel.ShowRelationshipMatchDetailViewModel();
		}

		#endregion

		#region firstImage Tapped
	     partial void FirstImageButtonTapped(NSObject sender)
		{
				ViewModel.WhichImageClicked = 1;

			ViewModel.ShowFriendList();
			//ViewModel.Close();


		}
		#endregion

		partial void SecondButtonImageTapped (NSObject sender)
		{
			
				ViewModel.WhichImageClicked = 2;

			ViewModel.ShowFriendList();
			//ViewModel.Close();

		}


		#region GetData

		public void GetData()
		{
			if (ViewModel.WhichImageClicked == 1) {
				if (ViewModel.FirstPersonCeleb != null) {
					ViewModel.WhichImageClicked = 0;
					//Mvx.Trace("celeb name in match view for first image: "+ViewModel.FirstPersonCeleb.GivenNames);

					firstPersonImage = ViewModel.FirstPersonCeleb.Img;
					isFirstPersonSelected = true;
					HandleFirstPersonSelected ();

				}else if(ViewModel.FirstPersonPeople != null){
					ViewModel.WhichImageClicked = 0;
					isSecondPersonSelected = true;

					firstPersonImage = ViewModel.FirstPersonPeople.ProfilePicURL;
					isFirstPersonSelected = true;
					HandleFirstPersonSelected ();
				}
			} else if (ViewModel.WhichImageClicked == 2){
				if (ViewModel.SecondPersonCeleb != null) {
					ViewModel.WhichImageClicked = 0;
					Mvx.Trace("celeb name in match view for sec image: "+ViewModel.SecondPersonCeleb.GivenNames);

					secondPersonImage = ViewModel.SecondPersonCeleb.Img;
					isSecondPersonSelected = true;
					HandleSecondPersonSelected ();

				}else if(ViewModel.SecondPersonPeople != null){
					ViewModel.WhichImageClicked = 0;
					Mvx.Trace("People name in match view for Sec image: "+ViewModel.SecondPersonPeople.Name);

					secondPersonImage = ViewModel.SecondPersonPeople.ProfilePicURL;
					isSecondPersonSelected = true;
					HandleSecondPersonSelected ();
				}
			}
		}

		#endregion

		public void HandleFirstPersonSelected()
		{

			if (isFirstPersonSelected) {

			
				//first_img.SetImageResource(Resource.Drawable.user_no_img);

				//firstCrossContainer.Visibility = ViewStates.Visible;
			} else {
//				first_img.SetImageResource (Resource.Drawable.empty_matcher_img);
//				firstCrossContainer.Visibility = ViewStates.Gone;
			}
			
		}

		public void HandleSecondPersonSelected()
		{
			
		}
	}
}

