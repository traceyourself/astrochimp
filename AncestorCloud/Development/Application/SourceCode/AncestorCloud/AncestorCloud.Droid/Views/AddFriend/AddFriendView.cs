
using System;
using Android.App;
using Android.OS;
using Android.Widget;
using AncestorCloud.Shared.ViewModels;
using Android.Content.PM;

namespace AncestorCloud.Droid
{
	[Activity (Label = "AddFriendView", ScreenOrientation = ScreenOrientation.Portrait)]			
	public class AddFriendView : BaseActivity
	{
		ActionBar actionBar;
	
		RelativeLayout fb_btn,contacts_btn,celeb_btn;


		public new AddFriendViewModel ViewModel
		{
			get { return base.ViewModel as AddFriendViewModel; }
			set { base.ViewModel = value; }
		}

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.add_friends_to_match);

			initUI ();
			configureActionBar ();

			ApplyActions ();

		}

		private void initUI()
		{
			fb_btn = FindViewById<RelativeLayout> (Resource.Id.fb_friend_btn);
			contacts_btn = FindViewById<RelativeLayout> (Resource.Id.contacts_friend_btn);
			celeb_btn = FindViewById<RelativeLayout> (Resource.Id.celeb_friend_btn);
		}

		private void ApplyActions(){
			fb_btn.Click += (object sender, EventArgs e) => {
				ViewModel.ShowFacebookFriend();
				ViewModel.Close();
			};

			celeb_btn.Click += (object sender, EventArgs e) => {
				ViewModel.ShowCelebrities();
				ViewModel.Close();
			};

			contacts_btn.Click += (object sender, EventArgs e) => {
				ViewModel.ShowContacts();
				ViewModel.Close();
			};

		}

		#region Action Bar Configuration
		private void configureActionBar(){

			actionBar = FindViewById <ActionBar>(Resource.Id.actionBar);
			actionBar.SetLeftCornerImage (Resource.Drawable.close_icon);

			actionBar.SetCenterText (Resources.GetString(Resource.String.select_someone_to_match));

			var crossButton = actionBar.FindViewById <RelativeLayout> (Resource.Id.action_bar_left_btn);

			crossButton.Click += (sender, e) => {
				ViewModel.Close();
			};
		}
		#endregion
	}
}

