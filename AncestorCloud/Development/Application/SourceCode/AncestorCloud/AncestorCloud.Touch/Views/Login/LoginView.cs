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
using CoreGraphics;
using Facebook.LoginKit;
using Facebook.CoreKit;
using Newtonsoft.Json;


namespace AncestorCloud.Touch
{
	
	public partial class LoginView : BaseViewController, IMvxModalTouchView
	{

//		FlyoutNavigationController navigation;
//		List<LoginViewModel> MenuItems;

		CGRect preFrame;
		LoginButton loginView;
		List<string> readPermissions = new List<string> { "public_profile","user_friends","user_relationships","email" };
		Boolean hideLogin = true;

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

			AppDelegate _delegate = (AppDelegate)UIApplication.SharedApplication.Delegate;

			_delegate.UIImageProfilePic = null;

			base.OnKeyboardChanged += OnKeyboardChanged;

			var buttonFrame = Facebookbutton.Frame;
			loginView = new LoginButton (buttonFrame) {
				LoginBehavior = LoginBehavior.Web,
				ReadPermissions = readPermissions.ToArray ()
			};
			container.AddSubview (loginView);
			loginView.Hidden = true;

			loginView.Completed += (sender, e) => {

				if (e.Error != null)
				{
					// Handle if there was an error
					HideLoader();
				}
				else if (e.Result.IsCancelled)
				{
					// Handle if the user cancelled the login request
					HideLoader();
				}
				else {

					AccessToken.RefreshCurrentAccessToken((e0, e1, e2) =>
					{

					});
					var token = AccessToken.CurrentAccessToken;

					// Handle your successful login
					if (e.Result != null && e.Result.Token != null)
					{
						Dictionary<String, String> props = new Dictionary<string, string>();
						props["state"] = "yrzxowmyldardwbx";
						props["access_token"] = token.TokenString;
						props["expires_in"] = "5105456";
						account = new Account(token.UserID, props);
						CompleteFacebookLogin();
					}
				}
			};


			//			EmailTextFeild.BecomeFirstResponder ();
			//			PasswordTextFeild.BecomeFirstResponder ();
			// Perform any additional setup after loading the view, typically from a nib.

			if (hideLogin){
				this.imageEmail.Hidden = true;
				this.imagePassword.Hidden = true;
				this.imageSeparatorLeft.Hidden = true;
				this.imageSeparatorRight.Hidden = true;
				this.labelOr.Hidden = true;
				this.labelLoginWithEmail.Hidden = true;
				this.EmailTextFeild.Hidden = true;
				this.PasswordTextFeild.Hidden = true;
				this.LoginButton.Hidden = true;
				this.labelContinueWith.Frame = new CGRect(new CGPoint(this.labelContinueWith.Frame.Location.X,
																	  this.labelContinueWith.Frame.Location.Y + 50),
														  this.labelContinueWith.Frame.Size);
				this.Facebookbutton.Frame = new CGRect(new CGPoint(this.Facebookbutton.Frame.Location.X,
																  this.Facebookbutton.Frame.Location.Y + 50),
													   this.Facebookbutton.Frame.Size);
			}
		}
		#region NavigationBar Property

		public void  SetNavigationBar()
		{
			UINavigationBar.Appearance.SetTitleTextAttributes (new UITextAttributes ()
				{ TextColor =Themes.TitleTextColor()});
			
			this.Title = Utility.LocalisedBundle().LocalizedString("LoginText", "");
			this.NavigationController.NavigationBar.BarTintColor = Themes.NavBarTintColor();
			var navController = base.NavigationController;

			#region TESTING


//			EmailTextFeild.Frame = new CGRect(20,260,285,60);
//			PasswordTextFeild.Frame = new CGRect(20,330,285,60);

			#endregion


			UIImage image = UIImage.FromFile (StringConstants.WHITECROSS);

			image = image.ImageWithRenderingMode (UIImageRenderingMode.AlwaysOriginal);


			this.NavigationItem.SetLeftBarButtonItem(
				new UIBarButtonItem(image
					, UIBarButtonItemStyle.Plain
					, (sender,args) => {
						ViewModel.Close();

					})
				, true);

			EmailTextFeild.ShouldReturn = _ => {
				EmailTextFeild.BecomeFirstResponder ();
				return false;
			};



			PasswordTextFeild.ShouldReturn = _ => {
				PasswordTextFeild.BecomeFirstResponder ();
				return false;
			};

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

			ViewModel.LinkFbUserData.Execute (null);

//			ViewModel.IsFbLogin = true;
//			ViewModel.CallFlyoutCommand.Execute(null);
//			ViewModel.CloseCommand.Execute (null);
		}

		partial void FacebookButtonTapped (NSObject sender)
		{
			DoFbLogin();
		}

		private Object GetValue(Object val){
			Object result = val;
			if (val is NSDictionary) {
				result = ConvertDictionary ((NSDictionary)val);
			} else if (val is NSArray) {
				var nsarray = (NSArray)val;
				result = new Object [nsarray.Count];
				for (int i = 0; i < (int)nsarray.Count; i++) {
					var ob = nsarray.ValueAt ((nuint)i);
					((Object[])result) [i] = GetValue (ob);
				}
			} else {
				Console.WriteLine ("HERE :" + val.GetType ());
			}
			return result;
		}

		private Dictionary<String,Object> ConvertDictionary(NSDictionary dataDic){
			Dictionary<String,Object> result = new Dictionary<string, object> ();

			foreach(var key in dataDic.Keys){
				var value = dataDic[key];
				result [key.ToString ()] = GetValue (value);
			}
			return result;



		}

		private void CompleteFacebookLogin(){

			var facebook = new FacebookService {
				ClientId = AppConstant.FBAPIKEY,
				ClientSecret = AppConstant.FBAPISECRETKEY,
				Scope = AppConstant.FBSCOPE 
			};


			var request = facebook.CreateRequest ("GET", new Uri ("https://graph.facebook.com/me"),account );//friends/accounts ///me/invitable_friends ///me/taggable_friends //permissions
			//https://graph.facebook.com/me?access_token=xxxxxxxxxxxxxxxxx

			ShowLoader();

			request.GetResponseAsync ().ContinueWith (response => {
				// parse the JSON in response.GetResponseText ()
				//System.Diagnostics.Debug.WriteLine (response.Result.GetResponseText());

				ViewModel.FbResponseText = response.Result.GetResponseText();
				ViewModel.SaveFbData();

				var familyRequest = facebook.CreateRequest ("GET", new Uri ("https://graph.facebook.com/me/family"),account );//friends/accounts ///me/invitable_friends ///me/taggable_friends //permissions
				var famResponse = familyRequest.GetResponseAsync();
				//System.Diagnostics.Debug.WriteLine ("famresponse :"+famResponse.Result.GetResponseText());
				ViewModel.FbFamilyResponseText = famResponse.Result.GetResponseText();
				ViewModel.SaveFbFamilyData();
				//
				var friendRequest = facebook.CreateRequest ("GET", new Uri ("https://graph.facebook.com/me/friends"),account );//friends/accounts ///me/invitable_friends ///me/taggable_friends //permissions
				var friendResponse = friendRequest.GetResponseAsync();
				//System.Diagnostics.Debug.WriteLine ("friendresponse :"+friendResponse.Result.GetResponseText());
				ViewModel.FbFriendResponseText = friendResponse.Result.GetResponseText();
				ViewModel.SaveFbFriendsData();

				InvokeOnMainThread (delegate {  
					HideLoader();

					if(account!=null)
					{
						DoLogin();
					}
				});
			});
		
		}

		public virtual bool HandlesKeyboardNotifications
		{
			get { return true; }
		}


		#endregion

		public  void OnKeyboardChanged (object sender,OnKeyboardChangedArgs args)
		{

			UITextField current = EmailTextFeild.IsFirstResponder ? EmailTextFeild: PasswordTextFeild;

			var point = this.View.ConvertRectToView (current.Frame, this.View.Superview);

			var frame = container.Frame;

			//preFrame = frame;

			if (frame.Size.Height  - args.Frame.Size.Height > point.Y + 50 && args.visible)
				return;
			
			if (args.visible)
				frame.Y -= point.Y + 50 - (frame.Size.Height - args.Frame.Size.Height);
			else
				frame.Y = 20 + 44;
//				frame = preFrame;//.Y += point.Y + 50 - (frame.Size.Height  - args.Frame.Size.Height);

			container.Frame = frame;
		}

	
		#region Facebook services

		Account account = null;

		public void DoFbLogin()
		{
			var token = Facebook.CoreKit.AccessToken.CurrentAccessToken;
			if (token != null) {
				Dictionary<String,String> props = new Dictionary<string, string> ();
				props ["state"] = "yrzxowmyldardwbx";
				props ["access_token"] = token.TokenString;
				props ["expires_in"] = "5105456";
				account = new Account (token.UserID, props);
				CompleteFacebookLogin ();
			} else {
				ShowLoader();
				LoginManager loginManager = new LoginManager();
				loginManager.LogOut();
				loginView.SendActionForControlEvents (UIControlEvent.TouchUpInside);
			}
		}

		public void DoFbLoginSocialSDK()
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

				CompleteFacebookLogin();

				DismissViewController (true, null);
			});

			PresentViewController (shareController, true, null);
		}

		#endregion


		#region Helper methods

		private void ShowLoader()
		{
			AppDelegate _delegate = (AppDelegate)UIApplication.SharedApplication.Delegate;
			_delegate.ShowActivityLoader ();
		}

		private void HideLoader()
		{
			AppDelegate _delegate = (AppDelegate)UIApplication.SharedApplication.Delegate;
			_delegate.HideActivityLoader ();
		}

		#endregion

		#region Comment Section
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


		//					var familyRequest = facebook.CreateRequest ("GET", new Uri ("https://graph.facebook.com/me/family"),account );//friends/accounts ///me/invitable_friends ///me/taggable_friends //permissions
		//					familyRequest.GetResponseAsync ().ContinueWith (famResponse => {
		//
		//						System.Diagnostics.Debug.WriteLine (famResponse.Result.GetResponseText());
		//						ViewModel.FbFamilyResponseText = famResponse.Result.GetResponseText();
		//
		//						ViewModel.SaveFbFamilyData();
		//
		//					});container
		 * 
		 * */

		#endregion

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			preFrame = container.Frame;
		}



	}
}

