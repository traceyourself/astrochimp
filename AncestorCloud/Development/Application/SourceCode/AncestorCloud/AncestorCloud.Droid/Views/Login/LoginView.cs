
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AncestorCloud.Shared.ViewModels;
using Xamarin.Social.Services;
using Xamarin.Social;
using Xamarin.Auth;
using Android.Content.PM;
using System.Net.Http;
using System.Net;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using Javax.Net.Ssl;
using Cirrious.CrossCore;


namespace AncestorCloud.Droid
{
	
	[Activity (Label = "LoginView",ScreenOrientation=ScreenOrientation.Portrait)]			
	public class LoginView : BaseActivity
	{

		ActionBar actionBar;
		TextView FbBtn,loginBtn;

		public new LoginViewModel ViewModel
		{
			get { return base.ViewModel as LoginViewModel; }
			set { base.ViewModel = value; }
		}

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.login);
			// Create your application here

			InitViews ();
			ConfigureActionBar ();
			ApplyActions ();
		}


		private void InitViews()
		{
			actionBar = FindViewById<ActionBar> (Resource.Id.actionBar);
			FbBtn = FindViewById<TextView> (Resource.Id.fb_btn_login);
			loginBtn = FindViewById<TextView> (Resource.Id.login_btn);
		}

		private void ConfigureActionBar()
		{
			actionBar.SetCenterText (Resources.GetString(Resource.String.log_in));
			actionBar.SetLeftCornerImage (Resource.Drawable.cross_btn);
			actionBar.FindViewById<RelativeLayout> (Resource.Id.action_bar_left_btn).Click += (object sender, EventArgs e) => {
				ViewModel.ShowHomeViewModel();
				ViewModel.Close();
			};
		}

		private void ApplyActions()
		{
			FbBtn.Click+= (object sender, EventArgs e) => {
				UseFacebookToLogin();
			};

			loginBtn.Click += (object sender, EventArgs e) => {
				Utilities.RegisterCertificateForApiHit();
				ViewModel.DoLogin();
			};
		}

		private void UseFacebookToLogin()
		{
			var facebook = new FacebookService { ClientId = "591314537670509",
												 ClientSecret = "659bacca4a45654358bb632f5607eeb0",
												 Scope = "publish_stream,email,user_friends,publish_actions"
											   };

			var authIntent = facebook.GetAuthenticateUI (this, accounts => {

			    Account account = accounts;

				System.Diagnostics.Debug.WriteLine ("accounts :" + account);

				if(account != null){
					var request = facebook.CreateRequest ("GET", new Uri ("https://graph.facebook.com/me/taggable_friends"),account );//friends ///me/invitable_friends ///me/taggable_friends
					request.GetResponseAsync ().ContinueWith (response => {
						// parse the JSON in response.GetResponseText ()
						//System.Diagnostics.Debug.WriteLine ("accounts :" + response.Result.GetResponseText());

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
						DoFBLogin();
					}

				}
			});

			StartActivity (authIntent);
		}
		 

		public void DoFBLogin(){
			ViewModel.ShowFbFamilyViewModel ();
			ViewModel.Close ();
		}

	}
}

