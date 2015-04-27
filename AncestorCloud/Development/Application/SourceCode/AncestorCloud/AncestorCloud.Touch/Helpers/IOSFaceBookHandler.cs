using System;
using Xamarin.Social.Services;
using Xamarin.Auth;
using AncestorCloud.Shared;

namespace AncestorCloud.Touch
{
	public class IOSFaceBookHandler
	{
		public IOSFaceBookHandler ()
		{
		}

		public BaseViewController parent {
			get;
			set;
		}

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

				var request = facebook.CreateRequest ("GET", new Uri ("https://graph.facebook.com/me/taggable_friends"),account );//friends/accounts ///me/invitable_friends ///me/taggable_friends //permissions
				//https://graph.facebook.com/me?access_token=xxxxxxxxxxxxxxxxx

				request.GetResponseAsync ().ContinueWith (response => {
					// parse the JSON in response.GetResponseText ()
					System.Diagnostics.Debug.WriteLine ("GetResponseText :" + response.Result.GetResponseText());
				});

				if(account!=null)
				{
					var view = parent as LoginView;
					view.DoLogin();
				}

				parent.DismissViewController (true, null);


			});



			parent.PresentViewController (shareController, true, null);
		}
	}
}

