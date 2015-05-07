
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
		ImageView first_img,sec_img;
		RelativeLayout firstCrossContainer,secCrossContainer;
		ImageView firstCrossImg,secCrossImg;

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
			first_img = contentLayout.FindViewById<ImageView> (Resource.Id.first_img);
			sec_img = contentLayout.FindViewById<ImageView> (Resource.Id.sec_img);

			firstCrossContainer = contentLayout.FindViewById<RelativeLayout> (Resource.Id.first_cross_container);
			secCrossContainer = contentLayout.FindViewById<RelativeLayout> (Resource.Id.sec_cross_container);
			firstCrossImg = contentLayout.FindViewById<ImageView> (Resource.Id.first_cross_img);
			secCrossImg = contentLayout.FindViewById<ImageView> (Resource.Id.sec_cross_img);

		}
		#endregion

		#region dynamic changing of width height of cross btn
		public override void OnWindowFocusChanged (bool hasFocus)
		{
			base.OnWindowFocusChanged (hasFocus);

			int firstHeight = first_img.Height;

			if(firstHeight > 0){
				int containerDimen = firstHeight / 2;
				ChangeDimensionsAccordingly (containerDimen);
			}
		}

		private void ChangeDimensionsAccordingly(int containerDimen)
		{
			ViewGroup.LayoutParams layParams = firstCrossContainer.LayoutParameters;
			layParams.Height = containerDimen;
			layParams.Width = containerDimen;
			firstCrossContainer.LayoutParameters = layParams;
			firstCrossContainer.Invalidate ();

			secCrossContainer.LayoutParameters = layParams;
			secCrossContainer.Invalidate ();


			layParams = firstCrossImg.LayoutParameters;
			layParams.Height = ((containerDimen/2)-25);
			layParams.Width = ((containerDimen/2)-25);
			firstCrossImg.LayoutParameters = layParams;
			firstCrossImg.Invalidate ();

			secCrossImg.LayoutParameters = layParams;
			secCrossImg.Invalidate ();

			//firstCrossContainer.Visibility = ViewStates.Visible;
			//secCrossContainer.Visibility = ViewStates.Visible;
		}
		#endregion

		#region Action Bar Configuration
		private void configureActionBar(){

			actionBar = FindViewById <ActionBar>(Resource.Id.actionBar);
			actionBar.SetLeftCornerMenuImage (Resource.Drawable.action_menu);

			actionBar.SetCenterImageText (Resource.Drawable.match_icon,Resources.GetString(Resource.String.matcher_menu));

			actionBar.SetRightBigImage (Resource.Drawable.clock_icon);

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
					ViewModel.Logout();
				}
			};

			matchBtn.Click += (object sender, EventArgs e) => {
				ViewModel.ShowRelationshipMatchDetailViewModel();
			};


			first_img.Click += (object sender, EventArgs e) => {
				ViewModel.ShowFriendList();
			};

			sec_img.Click += (object sender, EventArgs e) => {
				ViewModel.ShowFriendList();
			};

		}
		#endregion

	}
}