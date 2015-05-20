
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
using Android.Graphics;
using AncestorCloud.Shared.ViewModels;
using Android.Content.PM;
using Android.Support.V4.View;

namespace AncestorCloud.Droid
{
	[Activity (Label = "HomeView", NoHistory = true, ScreenOrientation = ScreenOrientation.Portrait)]			
	public class HomePage : BaseActivity
	{
		#region global variables
		TextView loginBtn,SignupBtn;
		ViewPager viewPager;
		//public ImageView firstDot,secDot,thirdDot,fourthDot;
		public ImageView[] dotImageViews;
		#endregion

		public new HomePageViewModel ViewModel
		{
			get { return base.ViewModel as HomePageViewModel; }
			set { base.ViewModel = value; }
		}

		#region OnCreate Method
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.HomeView);
			// Create your application here
			InitializeUIComponents ();
			CreateMultiPager ();	
			ApplyClickListeners ();
		}
		#endregion

		#region intialize components
		private void InitializeUIComponents ()
		{
			viewPager = FindViewById<Android.Support.V4.View.ViewPager> (Resource.Id.multiImagePager);
			loginBtn = FindViewById<TextView> (Resource.Id.login_btn);
			SignupBtn = FindViewById<TextView> (Resource.Id.sign_up_btn);

			dotImageViews = new ImageView[4];
			dotImageViews[0] = FindViewById<ImageView> (Resource.Id.firs_dot);
			dotImageViews[1] = FindViewById<ImageView> (Resource.Id.sec_dot);
			dotImageViews[2] = FindViewById<ImageView> (Resource.Id.third_dot);
			dotImageViews[3] = FindViewById<ImageView> (Resource.Id.fourth_dot);
		}
		#endregion


		#region create Muli image
		public void CreateMultiPager()
		{
			int[] images = {
				Resource.Drawable.home_slider_img,
				Resource.Drawable.get_connected,
				Resource.Drawable.share_Invite,
				Resource.Drawable.new_cousins
			}; 

			ImagePagerAdapter adapter = new ImagePagerAdapter (this,images);
			viewPager.Adapter = adapter;
			viewPager.Invalidate ();

			viewPager.SetOnPageChangeListener (new MyPageChangelistener(this));
		}
		#endregion


		#region click listeners
		public void ApplyClickListeners ()
		{
			loginBtn.Click += (object sender, EventArgs e) => {
				ViewModel.ShowLoginViewModel();
			};

			SignupBtn.Click += (object sender, EventArgs e) => {
				ViewModel.ShowSignViewModel();
			};
		}
		#endregion

	}

	#region Page change listener
	public class MyPageChangelistener : Java.Lang.Object,ViewPager.IOnPageChangeListener
	{
		HomePage homePage;

		public MyPageChangelistener(HomePage page){
			homePage = page;
		}

		public void OnPageScrollStateChanged(int state) {}
		public void OnPageScrolled(int position, float positionOffset, int positionOffsetPixels) {}

		public void OnPageSelected(int position) {
			HandleDotChange (position);
		}
		public void Dispose(){}

		public void HandleDotChange(int pos)
		{
			for(int i=0;i<homePage.dotImageViews.Length;i++)
			{
				if(i == pos){
					homePage.dotImageViews [i].SetImageResource (Resource.Drawable.blue_dot);
				}else{
					homePage  .dotImageViews [i].SetImageResource (Resource.Drawable.gray_dot);
				}
			}
		}

	}
	#endregion


	#region View Pager Adapter
	public class ImagePagerAdapter : PagerAdapter
	{

		Context mContext;
		LayoutInflater mLayoutInflater;
		int []toLoad;

		public ImagePagerAdapter(Context context,int []toLoad) {
			mContext = context;
			this.toLoad = toLoad;
			mLayoutInflater = (LayoutInflater) mContext.GetSystemService(Context.LayoutInflaterService);
		}

		public override int Count
		{
			get { return toLoad.Length; }
		}


		public override bool IsViewFromObject(View view, Java.Lang.Object obj) {
			return view == ((LinearLayout) obj);
		}


		public override Java.Lang.Object InstantiateItem(ViewGroup container, int position) {
			View itemView = mLayoutInflater.Inflate(Resource.Layout.home_pager_item, container, false);

			ImageView imageView = itemView.FindViewById<ImageView>(Resource.Id.slider_img);
			imageView.SetImageResource(toLoad[position]);

			container.AddView(itemView);

			return itemView;
		}


		public override void DestroyItem(ViewGroup container, int position, Java.Lang.Object obj) {
			container.RemoveView((LinearLayout) obj);
		}

	}
	#endregion

}

