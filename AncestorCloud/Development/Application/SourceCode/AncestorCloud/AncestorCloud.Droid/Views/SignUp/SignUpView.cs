
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
using Xamarin.Auth;
using AncestorCloud.Shared;

namespace AncestorCloud.Droid
{
	[Activity (Label = "SignUpView")]			
	public class SignUpView : BaseActivity
	{
		ActionBar actionBar;
		TextView fbBtn,signUpBtn;

		public new SignUpViewModel ViewModel
		{
			get { return base.ViewModel as SignUpViewModel; }
			set { base.ViewModel = value; }
		}


		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.sign_up);
			// Create your application here

			InitViews ();
			ConfigureActionBar ();
			ApplyActions ();
		}


		private void InitViews()
		{
			actionBar = FindViewById<ActionBar> (Resource.Id.actionBar);
			fbBtn = FindViewById<TextView> (Resource.Id.fb_btn_reg);
			signUpBtn = FindViewById<TextView> (Resource.Id.sign_up_btn);
		}


		private void ConfigureActionBar()
		{
			actionBar.SetCenterText (Resources.GetString(Resource.String.sign_up));
			actionBar.SetLeftCornerImage (Resource.Drawable.close_icon);
			actionBar.FindViewById<RelativeLayout> (Resource.Id.action_bar_left_btn).Click += (object sender, EventArgs e) => {
				ViewModel.ShowHomeViewModel();
				ViewModel.Close();
			};
		}

		private void ApplyActions()
		{
			fbBtn.Click += (object sender, EventArgs e) => {
				UseFacebookToRegister();
			};	

			signUpBtn.Click += (object sender, EventArgs e) => {
				Utilities.RegisterCertificateForApiHit();
				ViewModel.DoSignUp();
			};

		}

		private void UseFacebookToRegister()
		{
			var facebook = new FacebookService { ClientId = AppConstant.FBAPIKEY,
												ClientSecret = AppConstant.FBAPISECRETKEY,
												Scope = AppConstant.FBSCOPE
												};


			var authIntent = facebook.GetAuthenticateUI (this, accounts => {

				Account account = accounts;

				System.Diagnostics.Debug.WriteLine ("accounts :" + account);

				if(account != null){
					var request = facebook.CreateRequest ("GET", new Uri ("https://graph.facebook.com/me"),account );//friends ///me/invitable_friends ///me/taggable_friends
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
							if(account != null){
								DoFbSignUp();
							}
						});

					});


				}

			});

			StartActivity (authIntent);
		}

		public void DoFbSignUp(){
			ViewModel.ShowFbFamilyViewModel ();
			ViewModel.Close ();
		}

	}
}

