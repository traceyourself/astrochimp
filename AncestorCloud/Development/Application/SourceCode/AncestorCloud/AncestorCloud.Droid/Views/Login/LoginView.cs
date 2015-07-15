
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
using AncestorCloud.Shared;


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
			actionBar.SetLeftCornerImage (Resource.Drawable.close_icon);
			actionBar.FindViewById<RelativeLayout> (Resource.Id.action_bar_left_btn).Click += (object sender, EventArgs e) => {
				ViewModel.ShowHomeViewModel();
				ViewModel.Close();
			};
		}

		private void ApplyActions()
		{
			FbBtn.Click+= (object sender, EventArgs e) => {
				Utilities.RegisterCertificateForApiHit();
				UseFacebookToLogin();
			};

			loginBtn.Click += (object sender, EventArgs e) => {
				Utilities.LoggedInUsingFb = false;
				Utilities.RegisterCertificateForApiHit();
				ViewModel.DoLogin();
			};
		}

		private void UseFacebookToLogin()
		{
			var facebook = new FacebookService { ClientId = AppConstant.FBAPIKEY,
												 ClientSecret = AppConstant.FBAPISECRETKEY,
												 Scope = AppConstant.FBSCOPE 
											   };

			var authIntent = facebook.GetAuthenticateUI (this, accounts => {

				Account account = accounts;

				//System.Diagnostics.Debug.WriteLine ("accounts :" + account);

				if(account != null){
					var request = facebook.CreateRequest (StringConstants.GET_METHOD_TYPE, new Uri (StringConstants.FB_GRAPH_ME_URL),account );//friends ///me/invitable_friends ///me/taggable_friends

					ShowLoader();

					request.GetResponseAsync ().ContinueWith (response => {
						// parse the JSON in response.GetResponseText ()
						//System.Diagnostics.Debug.WriteLine ("accounts :" + response.Result.GetResponseText());

						ViewModel.FbResponseText = response.Result.GetResponseText();

						ViewModel.SaveFbData();

						//Mvx.Trace("saved result of me ");

						var familyRequest = facebook.CreateRequest (StringConstants.GET_METHOD_TYPE, new Uri (StringConstants.FB_GRAPH_FAMILY_URL),account );//friends/accounts ///me/invitable_friends ///me/taggable_friends //permissions
						familyRequest.GetResponseAsync ().ContinueWith (famResponse => {

							//System.Diagnostics.Debug.WriteLine (famResponse.Result.GetResponseText());
							ViewModel.FbFamilyResponseText = famResponse.Result.GetResponseText();

							ViewModel.SaveFbFamilyData();

							//Mvx.Trace("saved result of family ");

							var friendRequest = facebook.CreateRequest (StringConstants.GET_METHOD_TYPE, new Uri (StringConstants.FB_GRAPH_TAGGABLE_FRIENDS_URL),account );//friends/accounts ///me/invitable_friends ///me/taggable_friends //permissions

							friendRequest.GetResponseAsync().ContinueWith(friendResponse => {
								//System.Diagnostics.Debug.WriteLine ("friendresponse :"+friendResponse.Result.GetResponseText());
								ViewModel.FbFriendResponseText = friendResponse.Result.GetResponseText();
								ViewModel.SaveFbFriendsData();  	

								//Mvx.Trace("saved result of taggable friends ");

								HideLoader();

								//ViewModel.GetFbData();
								if(account != null){
									DoFBLogin();
								}

							});

						});

					});

				}
			});

			StartActivity (authIntent);
		}

		public void DoFBLogin(){
			/*Utilities.LoggedInUsingFb = true;
			ViewModel.ShowFbFamilyViewModel ();
			ViewModel.Close ();*/
			ViewModel.LinkFbUserData.Execute (null);
		}


		#region Helper methods
		ProgressDialog pd;
		private void ShowLoader()
		{
			if(pd != null){
				if(pd.IsShowing){
					pd.Dismiss ();
				}
			}
			pd = ProgressDialog.Show (this,"",Resources.GetString(Resource.String.loading));
		}

		private void HideLoader()
		{
			if(pd != null){
				pd.Dismiss ();
			}
		}
		#endregion


	}
}

