 
using System;

using Foundation;
using UIKit;
using Cirrious.MvvmCross.Touch.Views;
using AncestorCloud.Shared.ViewModels;
using FlyoutNavigation;
using CrossUI.Touch.Dialog.Elements;
using System.Collections.Generic;
using Cirrious.MvvmCross.ViewModels;
using Xamarin.Social.Services;
using Xamarin.Auth;
using Cirrious.MvvmCross.Binding.BindingContext;
using AncestorCloud.Shared;


namespace AncestorCloud.Touch
{
	public partial class LoginView : BaseViewController, IMvxModalTouchView
	{



//		FlyoutNavigationController navigation;
//		List<LoginViewModel> MenuItems;


		public LoginView () : base ("LoginView", null)
		{
		}

		public new LoginViewModel ViewModel
		{
			get { return base.ViewModel as LoginViewModel; }
			set { base.ViewModel = value; }
		}

		public void BindViewModel ()
		{
			var set = this.CreateBindingSet<LoginView, LoginViewModel>();
		    set.Bind (LoginButton).To (v => v.LoginCommand);
			set.Bind(EmailTextFeild).For(et => et.Text).To(ViewModel => ViewModel.Email);
			set.Bind(PasswordTextFeild).For(et => et.Text).To(ViewModel => ViewModel.Password);
			set.Apply();
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
			SetNavigationBar ();
			BindViewModel ();
			
			// Perform any additional setup after loading the view, typically from a nib.
		}
		#region NavigationBar Property

		public void  SetNavigationBar()
		{
			UINavigationBar.Appearance.SetTitleTextAttributes (new UITextAttributes ()
				{ TextColor = UIColor.FromRGB (255,255,255) });
			
			this.Title="Log In";
			this.NavigationController.NavigationBar.BarTintColor = UIColor.FromRGB (178, 45, 116);
			var navController = base.NavigationController;

			UIImage image = UIImage.FromFile ("cross_white.png");

			image = image.ImageWithRenderingMode (UIImageRenderingMode.AlwaysOriginal);


			this.NavigationItem.SetLeftBarButtonItem(
				new UIBarButtonItem(image
					, UIBarButtonItemStyle.Plain
					, (sender,args) => {
						ViewModel.Close();

					})
				, true);

		}
		#endregion


		#region ButtonTapped Handlers
		partial void LoginButtonTapped (NSObject sender)
		{
			 //DoLogin();
			//DoLogin();

			//ViewModel.LoginCommand.Execute(null);

		}

		void DoLogin()
		{
//			ViewModel.ShowFbFamilyViewModel ();
//			ViewModel.Close ();
			ViewModel.IsFbLogin = true;
			ViewModel.CallFlyoutCommand.Execute(null);
			ViewModel.CloseCommand.Execute(null);
		}

		partial void FacebookButtonTapped (NSObject sender)
		{
			DoFbLogin();
		}

		#endregion

	
		#region Facebook services

		Account account = null;

		public void DoFbLogin()
		{

			var facebook = new FacebookService {
				ClientId = AppConstant.FBAPIKEY,
				ClientSecret = AppConstant.FBAPISECRETKEY,
				Scope = AppConstant.FBSCOPE 
			};

			var shareController = facebook.GetAuthenticateUI ( accounts => {

				account = accounts;

				//System.Diagnostics.Debug.WriteLine ("accounts :" + account);

				if(account==null)
				{
					DismissViewController (true, null);
					
					return ;
				}

				var request = facebook.CreateRequest ("GET", new Uri ("https://graph.facebook.com/me"),account );//friends/accounts ///me/invitable_friends ///me/taggable_friends //permissions
				//https://graph.facebook.com/me?access_token=xxxxxxxxxxxxxxxxx

				request.GetResponseAsync ().ContinueWith (response => {
					// parse the JSON in response.GetResponseText ()
					//System.Diagnostics.Debug.WriteLine (response.Result.GetResponseText());

					ViewModel.FbResponseText = response.Result.GetResponseText();

					ViewModel.SaveFbData();

					var familyRequest = facebook.CreateRequest ("GET", new Uri ("https://graph.facebook.com/me/family"),account );//friends/accounts ///me/invitable_friends ///me/taggable_friends //permissions
					familyRequest.GetResponseAsync ().ContinueWith (famResponse => {

						//System.Diagnostics.Debug.WriteLine (famResponse.Result.GetResponseText());
						ViewModel.FbFamilyResponseText = famResponse.Result.GetResponseText();

						ViewModel.SaveFbFamilyData();

						//ViewModel.GetFbData();

					});
	
				});
					
				if(account!=null)
				{
					
					DoLogin();
				}

				DismissViewController (true, null);

			});

			PresentViewController (shareController, true, null);
		}


		/*
		 * 
		//			var item = new Item { Text = "Xamarin.Social is the bomb.com." };
		//			item.Links.Add (new Uri ("http://github.com/xamarin/xamarin.social"));


		//			var shareController = facebook.GetShareUI (item, result => {
		//				// result lets you know if the user shared the item or canceled
		//				DismissViewController (true, null);
		//			});

		//			facebook.GetAccountsAsync ().ContinueWith (accounts => {
		//				// accounts is an IEnumerable<Account> of saved accounts
		//			});
		 * 
		 * */
		#endregion



	}
}

