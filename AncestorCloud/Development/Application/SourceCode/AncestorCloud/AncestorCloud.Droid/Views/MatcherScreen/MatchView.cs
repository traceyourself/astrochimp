
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
using Android.Webkit;
using Android.Graphics.Drawables.Shapes;
using Android.Graphics.Drawables;
using Android.Graphics;

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
		ImageView first_img_cover,sec_img_cover;
		RelativeLayout firstCrossContainer,secCrossContainer;
		ImageView firstCrossImg,secCrossImg;

		bool isFirstPersonSelected = false;
		bool isSecondPersonSelected = false;
		String firstPersonImage = "",secondPersonImage = "";


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

			first_img_cover = contentLayout.FindViewById<ImageView> (Resource.Id.first_img_circular_cover);
			sec_img_cover = contentLayout.FindViewById<ImageView> (Resource.Id.sec_img_circular_cover);
		}
		#endregion

		#region dynamic changing of width height of cross btn
		public override void OnWindowFocusChanged (bool hasFocus)
		{
			base.OnWindowFocusChanged (hasFocus);

			int firstHeight = first_img.Height;
			int secHeight = sec_img.Height;

			if(firstHeight > 0 && secHeight > 0){
				int containerDimenfirst = firstHeight / 2;
				int containerDimensec = secHeight / 2;
				ChangeDimensionsAccordingly (containerDimenfirst,containerDimensec);
			}
		}

		private void ChangeDimensionsAccordingly(int containerDimenFirst,int containerDimenSec)
		{
			ViewGroup.LayoutParams layParamsFirst = first_img.LayoutParameters;
			layParamsFirst.Height = containerDimenFirst+containerDimenFirst;
			layParamsFirst.Width = containerDimenFirst+containerDimenFirst;
			first_img.LayoutParameters = layParamsFirst;
			first_img.Invalidate ();

			sec_img.LayoutParameters = layParamsFirst;

			first_img_cover.LayoutParameters = layParamsFirst;
			sec_img_cover.LayoutParameters = layParamsFirst;


			ViewGroup.LayoutParams layParams = firstCrossContainer.LayoutParameters;
			layParams.Height = containerDimenFirst;
			layParams.Width = containerDimenFirst;
			firstCrossContainer.LayoutParameters = layParams;
			firstCrossContainer.Invalidate ();

			layParams = firstCrossImg.LayoutParameters;
			layParams.Height = ((containerDimenFirst/2)-25);
			layParams.Width = ((containerDimenFirst/2)-25);
			firstCrossImg.LayoutParameters = layParams;
			firstCrossImg.Invalidate ();


			ViewGroup.LayoutParams layParams1 = secCrossContainer.LayoutParameters;
			layParams1.Height = containerDimenSec;
			layParams1.Width = containerDimenSec;
			secCrossContainer.LayoutParameters = layParams1;
			secCrossContainer.Invalidate ();

			layParams1 = secCrossImg.LayoutParameters;
			layParams1.Height = ((containerDimenSec/2)-25);
			layParams1.Width = ((containerDimenSec/2)-25);
			secCrossImg.LayoutParameters = layParams1;
			secCrossImg.Invalidate ();

			//firstCrossContainer.Visibility = ViewStates.Visible;
			//secCrossContainer.Visibility = ViewStates.Visible;

			first_img_cover.SetBackgroundDrawable (new ImageCoverDrawable((float)containerDimenFirst));
			sec_img_cover.SetBackgroundDrawable (new ImageCoverDrawable((float)containerDimenFirst));
		}
		#endregion

		#region cover Image
		public class ImageCoverDrawable : GradientDrawable {

			public ImageCoverDrawable(float radius) {
				SetShape(ShapeType.Oval);
				SetCornerRadius(radius);
				SetColor(Color.Transparent);
			}
		}
		#endregion

		#region backpressed

		public override void OnBackPressed ()
		{
			ViewModel.Close ();
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
				if(!isFirstPersonSelected){
					ViewModel.WhichImageClicked = 1;
					ViewModel.ShowFriendList();
				}
			};

			sec_img.Click += (object sender, EventArgs e) => {
				if(!isSecondPersonSelected){
					ViewModel.WhichImageClicked = 2;
					ViewModel.ShowFriendList();
				}
			};


			firstCrossImg.Click += (object sender, EventArgs e) => {
				isFirstPersonSelected = false;
				ViewModel.FirstPersonCeleb = null;
				ViewModel.FirstPersonPeople = null;
				HandleFirstPersonSelected();
			};

			secCrossImg.Click += (object sender, EventArgs e) => {
				isSecondPersonSelected = false;
				ViewModel.SecondPersonCeleb = null;
				ViewModel.SecondPersonPeople = null;
				HandleSecondPersonSelected();
			};

		}
		#endregion


		protected override void OnResume ()
		{
			base.OnResume ();

			if (ViewModel.WhichImageClicked == 1) {
				if (ViewModel.FirstPersonCeleb != null) {
					ViewModel.WhichImageClicked = 0;
					//Mvx.Trace("celeb name in match view for first image: "+ViewModel.FirstPersonCeleb.GivenNames);

					firstPersonImage = ViewModel.FirstPersonCeleb.Img;
					isFirstPersonSelected = true;
					HandleFirstPersonSelected ();

				}else if(ViewModel.FirstPersonPeople != null){
					ViewModel.WhichImageClicked = 0;

					firstPersonImage = ViewModel.FirstPersonPeople.ProfilePicURL;
					isFirstPersonSelected = true;
					HandleFirstPersonSelected ();
				}
			} else if (ViewModel.WhichImageClicked == 2){
				if (ViewModel.SecondPersonCeleb != null) {
					ViewModel.WhichImageClicked = 0;
					//Mvx.Trace("celeb name in match view for sec image: "+ViewModel.SecondPersonCeleb.GivenNames);

					secondPersonImage = ViewModel.SecondPersonCeleb.Img;
					isSecondPersonSelected = true;
					HandleSecondPersonSelected ();

				}else if(ViewModel.SecondPersonPeople != null){
					ViewModel.WhichImageClicked = 0;
					//Mvx.Trace("People name in match view for Sec image: "+ViewModel.SecondPersonPeople.Name);

					secondPersonImage = ViewModel.SecondPersonPeople.ProfilePicURL;
					isSecondPersonSelected = true;
					HandleSecondPersonSelected ();
				}
			}
		}

		public void HandleFirstPersonSelected()
		{
			if (isFirstPersonSelected) {
				if (URLUtil.IsValidUrl(firstPersonImage)) {
					first_img.SetImageURI (Android.Net.Uri.Parse (firstPersonImage));
				} else {
					first_img.SetImageResource(Resource.Drawable.no_img);
				}
				firstCrossContainer.Visibility = ViewStates.Visible;
			} else {
				first_img.SetImageResource (Resource.Drawable.empty_matcher_img);
				firstCrossContainer.Visibility = ViewStates.Gone;
			}
		}

		public void HandleSecondPersonSelected()
		{
			if (isSecondPersonSelected) {
				if (URLUtil.IsValidUrl(secondPersonImage)) {
					sec_img.SetImageURI (Android.Net.Uri.Parse (secondPersonImage));
				} else {
					sec_img.SetImageResource (Resource.Drawable.user_no_img);
				}
				secCrossContainer.Visibility = ViewStates.Visible;
			} else {
				sec_img.SetImageResource (Resource.Drawable.empty_matcher_img);
				secCrossContainer.Visibility = ViewStates.Gone;
			}
		}


		/*private Bitmap GetImageBitmapFromUrl(string url)
		{
			Bitmap imageBitmap = null;

			using (var webClient = new WebClient())
			{
				var imageBytes = webClient.DownloadData(url);
				if (imageBytes != null && imageBytes.Length > 0)
				{
					imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
				}
			}

			return imageBitmap;
		}

		var imageBitmap = GetImageBitmapFromUrl("http://xamarin.com/resources/design/home/devices.png");
		imagen.SetImageBitmap(imageBitmap);*/

	}
}