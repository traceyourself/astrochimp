
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
using Cirrious.CrossCore;

namespace AncestorCloud.Droid
{
	[Activity (Label = "MatcherView")]			
	public class MatchView : BaseActivity
	{
		FlyOutContainer menu;
		ActionBar actionBar;
		LinearLayout menuLayout,contentLayout;
		TextView matchBtn;


		public new MatchViewModel ViewModel
		{
			get { return base.ViewModel as MatchViewModel; }
			set { base.ViewModel = value; }
		}

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			// Create your application here
			SetContentView(Resource.Layout.matcher_fly_out);
			initUI ();
			configureActionBar ();

			ApplyActions ();

		}

		#region init ui
		private void initUI()
		{
			menu = FindViewById<FlyOutContainer> (Resource.Id.flyOutContainerLay);
			menuLayout = FindViewById<LinearLayout> (Resource.Id.FlyOutMenu);
			contentLayout = FindViewById<LinearLayout> (Resource.Id.FlyOutContent);
			matchBtn = contentLayout.FindViewById<TextView> (Resource.Id.match_btn);
		}
		#endregion

		#region Action Bar Configuration
		private void configureActionBar(){

			actionBar = FindViewById <ActionBar>(Resource.Id.actionBar);
			actionBar.SetLeftCornerMenuImage (Resource.Drawable.action_menu);

			actionBar.SetCenterImageText (Resource.Drawable.action_menu,Resources.GetString(Resource.String.matcher_menu));

			actionBar.SetRightImage (Resource.Drawable.action_menu);

			var menuButton = actionBar.FindViewById <RelativeLayout> (Resource.Id.action_bar_left_btn);
			menuButton.Click += (sender, e) => {
				//Mvx.Trace("menu btn clicked");
				menu.AnimatedOpened = !menu.AnimatedOpened;
			};

			var pastButton = actionBar.FindViewById <RelativeLayout> (Resource.Id.action_bar_right_btn);
			pastButton.Click += (sender, e) => {
				ViewModel.ShowPastMatchesViewModel();
			};
		}
		#endregion


		#region Apply Actions
		private void ApplyActions(){
			menuLayout.FindViewById<LinearLayout> (Resource.Id.my_family_menu_btn).Click += (object sender, EventArgs e) => {
				//menu.AnimatedOpened = !menu.AnimatedOpened;
				if(menu.AnimatedOpened){
					ViewModel.ShowFamilyView();
				}
			};

			menuLayout.FindViewById<LinearLayout> (Resource.Id.matcher_menu_btn).Click += (object sender, EventArgs e) => {
				menu.AnimatedOpened = !menu.AnimatedOpened;
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
					ViewModel.Close();
				}
			};

			matchBtn.Click += (object sender, EventArgs e) => {
				ViewModel.ShowRelationshipMatchDetailViewModel();
			};
		}
		#endregion

	}
}