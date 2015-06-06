using System;
using Foundation;
using UIKit;
using Cirrious.MvvmCross.Touch.Views;
using AncestorCloud.Shared.ViewModels;
using Cirrious.MvvmCross.Binding.BindingContext;
using Xamarin.Social.Services;
using AncestorCloud.Shared;
using Xamarin.Auth;
using CoreGraphics;


namespace AncestorCloud.Touch
{
	public partial class SignUpView : BaseViewController,IMvxModalTouchView
	{
		CGRect preFrame;

		public SignUpView () : base ("SignUpView", null)
		{
		}

		public new SignUpViewModel ViewModel
		{
			get { return base.ViewModel as SignUpViewModel; }
			set { base.ViewModel = value; }
		}

		public void BindViewModel ()
		{
			var set = this.CreateBindingSet<SignUpView, SignUpViewModel>();
			set.Bind (SignUpButton).To (v => v.SignUpCommand);
			set.Bind(NameTextFeild).For(et => et.Text).To(ViewModel => ViewModel.FirstName);
			set.Bind (LastNameTextField).For (et => et.Text).To (ViewModel => ViewModel.LastName);
			set.Bind(EmailTextField).For(et => et.Text).To(ViewModel => ViewModel.Email);
			set.Bind(PasswordTextField).For(et =>et.Text).To(ViewModel => ViewModel.Password);
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
			SetNavigationBar();
			BindViewModel ();

			base.OnKeyboardChanged += OnKeyboardChanged;

			AppDelegate _delegate = (AppDelegate)UIApplication.SharedApplication.Delegate;

			_delegate.UIImageProfilePic = null;
			// Perform any additional setup after loading the view, typically from a nib.
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			preFrame = _container.Frame;
		}


		#region NavigationBar Property

		public void  SetNavigationBar()
		{
			UINavigationBar.Appearance.SetTitleTextAttributes (new UITextAttributes ()
				{ TextColor = Themes.TitleTextColor() });

			this.Title = Utility.LocalisedBundle ().LocalizedString ("SignUpText", "");
			this.NavigationController.NavigationBar.BarTintColor = Themes.NavBarTintColor ();
			var navController = base.NavigationController;

			UIImage image = UIImage.FromFile (StringConstants.WHITECROSS);

			image = image.ImageWithRenderingMode (UIImageRenderingMode.AlwaysOriginal);
   		
			this.NavigationItem.SetLeftBarButtonItem(
				new UIBarButtonItem(image
					, UIBarButtonItemStyle.Plain
					, (sender,args) => {
						ViewModel.Close();
					})
				, true);


			NameTextFeild.ShouldReturn = _ => {
				NameTextFeild.BecomeFirstResponder ();
				return false;
			};



			LastNameTextField.ShouldReturn = _ => {
				LastNameTextField.BecomeFirstResponder ();
				return false;
			};

			EmailTextField.ShouldReturn = _ => {
				EmailTextField.BecomeFirstResponder ();
				return false;
			};

			PasswordTextField.ShouldReturn = _ => {
				PasswordTextField.BecomeFirstResponder ();
				return false;
			};

		}

		#endregion


		#region ButtonTapped Handlers
		partial void SignUpButttonTapped (NSObject sender)
		{
//			DoSignUp();
			//ViewModel.LoginCommand.Execute(null);

		}

		void DoSignUp()
		{
//			ViewModel.IsFbLogin = true;
//			ViewModel.CallFlyoutCommand.Execute(null);
//			ViewModel.CloseCommand.Execute(null);

			ViewModel.LinkFbUserData.Execute(null);
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

				if(account==null)
				{
					DismissViewController (true, null);

					return ;
				}

				//System.Diagnostics.Debug.WriteLine ("accounts :" + account);

				if(account==null)
				{
					DismissViewController (true, null);

					return ;
				}

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

					var friendRequest = facebook.CreateRequest ("GET", new Uri ("https://graph.facebook.com/me/friends"),account );//friends/accounts ///me/invitable_friends ///me/taggable_friends //permissions
					var friendResponse = friendRequest.GetResponseAsync();
					//System.Diagnostics.Debug.WriteLine ("friendresponse :"+friendResponse.Result.GetResponseText());
					ViewModel.FbFriendResponseText = friendResponse.Result.GetResponseText();
					ViewModel.SaveFbFriendsData();

					InvokeOnMainThread (delegate {  
						HideLoader();

						if(account!=null)
						{
							DoSignUp();
						}
					});
				});

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

		#region KeyBoard
		public virtual bool HandlesKeyboardNotifications
		{
			get { return true; }
		}

		public  void OnKeyboardChanged (object sender,OnKeyboardChangedArgs args)
		{

			UITextField current; //= NameTextFeild.IsFirstResponder ? NameTextFeild : LastNameTextField;

			if (NameTextFeild.IsFirstResponder) {
				current = NameTextFeild;
			} else if (LastNameTextField.IsFirstResponder) {
				current = LastNameTextField;
			} else if (EmailTextField.IsFirstResponder) {
				current = EmailTextField;
			} else {
				current = PasswordTextField;
			}

			var point = this.View.ConvertRectToView (current.Frame, this.View.Superview);

			var frame = _container.Frame;

			if (frame.Size.Height  - args.Frame.Size.Height > point.Y + 100)
				return;

			if (args.visible)
				frame.Y -= point.Y + 100 - (frame.Size.Height - args.Frame.Size.Height);
			else
				frame = preFrame;

			_container.Frame = frame;
		}

		#endregion
		partial void TCButtonTaped (NSObject sender)
		{
			//System.Diagnostics.Debug.WriteLine("T&C");
			this.NavigationController.DismissViewController(false,null);
			ViewModel.Close();
			ViewModel.ShowTermsandCondition();

		}

//		public override void TouchesBegan (NSSet touches, UIEvent evt)
//		{
//			//if (this.View.IsFirstResponder)
//				this.View.ResignFirstResponder ();
//
//			base.TouchesBegan (touches, evt);
//		}


	}
}

