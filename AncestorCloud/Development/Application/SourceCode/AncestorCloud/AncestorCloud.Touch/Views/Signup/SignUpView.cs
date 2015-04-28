using System;
using Foundation;
using UIKit;
using Cirrious.MvvmCross.Touch.Views;
using AncestorCloud.Shared.ViewModels;
using Cirrious.MvvmCross.Binding.BindingContext;
using Xamarin.Social.Services;
using AncestorCloud.Shared;
using Xamarin.Auth;


namespace AncestorCloud.Touch
{
	public partial class SignUpView : BaseViewController,IMvxModalTouchView
	{
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
			set.Bind(NameTextFeild).For(et => et.Text).To(ViewModel => ViewModel.Name);
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

			// Perform any additional setup after loading the view, typically from a nib.
		}


		#region NavigationBar Property

		public void  SetNavigationBar()
		{
			UINavigationBar.Appearance.SetTitleTextAttributes (new UITextAttributes ()
				{ TextColor = UIColor.FromRGB (255,255,255) });

			this.Title="Sign Up";
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
		partial void SignUpButttonTapped (NSObject sender)
		{
//			DoSignUp();
			//ViewModel.LoginCommand.Execute(null);

		}

		void DoSignUp()
		{
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

				if(account==null)
				{
					DismissViewController (true, null);

					return ;
				}

				//System.Diagnostics.Debug.WriteLine ("accounts :" + account);

				var request = facebook.CreateRequest ("GET", new Uri ("https://graph.facebook.com/me"),account );//friends/accounts ///me/invitable_friends ///me/taggable_friends //permissions
				//https://graph.facebook.com/me?access_token=xxxxxxxxxxxxxxxxx

				request.GetResponseAsync ().ContinueWith (response => {
					// parse the JSON in response.GetResponseText ()
					//System.Diagnostics.Debug.WriteLine (response.Result.GetResponseText().GetType());

					ViewModel.FbResponseText = response.Result.GetResponseText();

					//ViewModel.SaveUserFbData.Execute(null);

					//ViewModel.GetFbData();
					ViewModel.SaveFbFamilyData();


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

					DoSignUp();
				}

				DismissViewController (true, null);

			});

			PresentViewController (shareController, true, null);
		}

		#endregion
	}
}

