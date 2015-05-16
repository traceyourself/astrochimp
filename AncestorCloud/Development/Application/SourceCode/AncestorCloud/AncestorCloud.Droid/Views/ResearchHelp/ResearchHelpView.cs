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

namespace AncestorCloud.Droid
{
	[Activity (Label = "ResearchHelpView")]			
	public class ResearchHelpView : BaseActivity
	{
		FlyOutContainer menu;
		ActionBar actionBar;
		LinearLayout contentLayout;
		RelativeLayout menuLayout;
		TextView research_help_txt;
		TextView userNameMenu;
		ImageView userImageMenu;

		public new ResearchHelpViewModel ViewModel
		{
			get { return base.ViewModel as ResearchHelpViewModel; }
			set { base.ViewModel = value; }
		}

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.research_help_fly_out);

			initUI ();
			configureActionBar ();
			ApplyActions ();
		}

		protected override void OnResume ()
		{
			base.OnResume ();
			ApplyData ();
		}


		private void initUI()
		{
			menu = FindViewById<FlyOutContainer> (Resource.Id.flyOutContainerLay);
			menuLayout = FindViewById<RelativeLayout> (Resource.Id.FlyOutMenu);
			contentLayout = FindViewById<LinearLayout> (Resource.Id.FlyOutContent);
			research_help_txt = contentLayout.FindViewById<TextView> (Resource.Id.help_txt);

			userNameMenu = menuLayout.FindViewById<TextView> (Resource.Id.user_name_menu);
			userImageMenu = menuLayout.FindViewById<ImageView> (Resource.Id.user_img_menu);
		}

		public void ApplyData(){
			//userNameMenu;
			if (Utilities.CurrentUserimage != null) {
				userImageMenu.SetImageBitmap (Utilities.CurrentUserimage);	
			}

			LoginModel modal = ViewModel.GetUserData();
			userNameMenu.Text = modal.UserEmail;
		}


		private void ApplyActions(){

			menuLayout.FindViewById<LinearLayout> (Resource.Id.my_family_menu_btn).Click += (object sender, EventArgs e) => {
				//menu.AnimatedOpened = !menu.AnimatedOpened;
				//if(menu.AnimatedOpened){
					ViewModel.ShowFamilyViewModel();
					ViewModel.Close();
				//}
			};

			menuLayout.FindViewById<LinearLayout> (Resource.Id.matcher_menu_btn).Click += (object sender, EventArgs e) => {
				//menu.AnimatedOpened = !menu.AnimatedOpened;
				//if(menu.AnimatedOpened){
					ViewModel.ShowMatcherViewModel();
					ViewModel.Close();
				//}
			};

			menuLayout.FindViewById<LinearLayout> (Resource.Id.research_menu_btn).Click += (object sender, EventArgs e) => {
				//if(menu.AnimatedOpened){
					menu.AnimatedOpened = !menu.AnimatedOpened;
				//}
			};

			menuLayout.FindViewById<LinearLayout> (Resource.Id.logout_menu_btn).Click += (object sender, EventArgs e) => {
				//menu.AnimatedOpened = !menu.AnimatedOpened;
				//if(menu.AnimatedOpened){
					Utilities.CurrentUserimage = null;
					ViewModel.Logout();
				//}
			};

			menuLayout.FindViewById<LinearLayout> (Resource.Id.profile_menu_btn).Click += (object sender, EventArgs e) => {
				menu.AnimatedOpened = !menu.AnimatedOpened;
				ViewModel.ShowProfilePicModel();
			};

		}

		#region Action Bar Configuration
		private void configureActionBar(){

			actionBar = FindViewById <ActionBar>(Resource.Id.actionBar);
			actionBar.SetLeftCornerMenuImage (Resource.Drawable.action_menu);

			actionBar.SetCenterImageText (Resource.Drawable.search_icon,Resources.GetString(Resource.String.research_menu));

			var menuButton = actionBar.FindViewById <RelativeLayout> (Resource.Id.action_bar_left_btn);

			menuButton.Click += (sender, e) => {
				menu.AnimatedOpened = !menu.AnimatedOpened;
			};
		}
		#endregion

	}
}

