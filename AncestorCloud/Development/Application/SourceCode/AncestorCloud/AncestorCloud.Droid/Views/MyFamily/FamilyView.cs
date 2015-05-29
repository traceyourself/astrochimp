
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
using AncestorCloud.Shared;
using Android.Content.PM;
using Android.Graphics;

namespace AncestorCloud.Droid
{
	[Activity (Label = "MyFamilyView", ScreenOrientation = ScreenOrientation.Portrait)]			
	public class FamilyView : BaseActivity
	{
		FlyOutContainer menu;
		ActionBar actionBar;
		LinearLayout contentLayout;
		RelativeLayout menuLayout;
		TextView addFamilyBtn;
		ImageView helpIcon;
		TextView userNameMenu;
		ImageView userImageMenu;

		public new FamilyViewModel ViewModel
		{
			get { return base.ViewModel as FamilyViewModel; }
			set { base.ViewModel = value; }
		}

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.my_familiy_fly_out);

			initUI ();
			configureActionBar ();

			ApplyActions ();
		
			LoginModel modal = ViewModel.GetUserData();
			userNameMenu.Text = modal.UserEmail;

			if(Utilities.CurrentUserimage == null){
				string avatarUrl = ""+modal.AvatarURL;
				if(avatarUrl.Length > 0){
					new AvatarImageTask (this,avatarUrl).Execute();
				}
			}

		}


		protected override void OnResume ()
		{
			base.OnResume ();
			ApplyData ();

			if(Utilities.LoggedInUsingFb){
				Utilities.LoggedInUsingFb = false;
			}
		}

		private void initUI()
		{
			menu = FindViewById<FlyOutContainer> (Resource.Id.flyOutContainerLay);
			menuLayout = FindViewById<RelativeLayout> (Resource.Id.FlyOutMenu);
			contentLayout = FindViewById<LinearLayout> (Resource.Id.FlyOutContent);
			addFamilyBtn = contentLayout.FindViewById<TextView> (Resource.Id.add_family_btn);
			helpIcon = contentLayout.FindViewById<ImageView> (Resource.Id.help_icon);

			userNameMenu = menuLayout.FindViewById<TextView> (Resource.Id.user_name_menu);
			userImageMenu = menuLayout.FindViewById<ImageView> (Resource.Id.user_img_menu);
		}

		public void ApplyData(){
			//userNameMenu;
			if (Utilities.CurrentUserimage != null) {
				userImageMenu.SetImageBitmap (Utilities.CurrentUserimage);	
			}


		}

		private void ApplyActions(){
		
			addFamilyBtn.Click += (object sender, EventArgs e) => {
				ViewModel.ShowEditViewModel();
			};

			menuLayout.FindViewById<LinearLayout> (Resource.Id.my_family_menu_btn).Click += (object sender, EventArgs e) => {
				
				menu.AnimatedOpened = !menu.AnimatedOpened;
			};

			menuLayout.FindViewById<LinearLayout> (Resource.Id.matcher_menu_btn).Click += (object sender, EventArgs e) => {
				//menu.AnimatedOpened = !menu.AnimatedOpened;
				if(menu.AnimatedOpened){
					ViewModel.CallMatcher();
				}
			};

			menuLayout.FindViewById<LinearLayout> (Resource.Id.research_menu_btn).Click += (object sender, EventArgs e) => {
				//menu.AnimatedOpened = !menu.AnimatedOpened;
				if(menu.AnimatedOpened){
					ViewModel.ShowResearchHelpViewModel();
					ViewModel.Close();
				}
			};

			menuLayout.FindViewById<LinearLayout> (Resource.Id.logout_menu_btn).Click += (object sender, EventArgs e) => {
				//menu.AnimatedOpened = !menu.AnimatedOpened;
				if(menu.AnimatedOpened){
					Utilities.CurrentUserimage = null;
					ViewModel.Logout();
				}
			};

			menuLayout.FindViewById<LinearLayout> (Resource.Id.profile_menu_btn).Click += (object sender, EventArgs e) => {
				if(menu.AnimatedOpened){
					menu.AnimatedOpened = !menu.AnimatedOpened;
					ViewModel.ShowProfilePicModel();
				}
			};

			helpIcon.Click += (object sender, EventArgs e) => {
				new HelpDialog(this).ShowHelpDialog();
			};

		}

		#region Action Bar Configuration
		private void configureActionBar(){

			actionBar = FindViewById <ActionBar>(Resource.Id.actionBar);
			actionBar.SetLeftCornerMenuImage (Resource.Drawable.action_menu);

			actionBar.SetCenterImageText (Resource.Drawable.myfamily_title,Resources.GetString(Resource.String.my_family_menu));

			var menuButton = actionBar.FindViewById <RelativeLayout> (Resource.Id.action_bar_left_btn);

			menuButton.Click += (sender, e) => {
				menu.AnimatedOpened = !menu.AnimatedOpened;
			};
		}
		#endregion


		#region first image downloader
		public class AvatarImageTask : AsyncTask
		{
			Bitmap resultBMP = null;
			FamilyView myObj;
			string url;

			public AvatarImageTask(FamilyView myObj,string url)
			{
				this.myObj = myObj;
				this.url = url;
			}

			protected override void OnPreExecute()
			{
			}

			protected override Java.Lang.Object DoInBackground(params Java.Lang.Object[] @params)
			{
				resultBMP = Utilities.GetRoundedimage(myObj,url,0,200);
				return "";
			}

			protected override void OnPostExecute(Java.Lang.Object result)
			{
				Utilities.CurrentUserimage = resultBMP;
				myObj.userImageMenu.SetImageBitmap (Utilities.CurrentUserimage);	
			}
		}
		#endregion


	}
}

