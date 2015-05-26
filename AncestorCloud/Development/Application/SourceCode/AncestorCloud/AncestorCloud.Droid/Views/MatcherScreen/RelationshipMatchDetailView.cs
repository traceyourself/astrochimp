
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
using Android.Graphics;
using AncestorCloud.Shared;
using Android.Webkit;
using Xamarin.Social.Services;
using Xamarin.Social;
using Android.Content.PM;

namespace AncestorCloud.Droid
{
	[Activity (Label = "RelationshipMatchDetailView", ScreenOrientation = ScreenOrientation.Portrait)]			
	public class RelationshipMatchDetailView : BaseActivity
	{
		
		ActionBar actionBar;
		ListView resultlist;
		ImageView firstPersonImage,secondPersonImage;
		TextView firstName,secName,degree;

		public new RelationshipMatchDetailViewModel ViewModel
		{
			get { return base.ViewModel as RelationshipMatchDetailViewModel; }
			set { base.ViewModel = value; }
		}

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			// Create your application here
			SetContentView(Resource.Layout.matcher_result);

			initUI ();
			configureActionBar ();

			ApplyActions ();
			CreateListAdapter ();
			ApplyData ();
		}

		#region init ui
		private void initUI()
		{
			resultlist = FindViewById<ListView> (Resource.Id.matched_list);
			firstPersonImage = FindViewById<ImageView> (Resource.Id.first_user_img);
			secondPersonImage = FindViewById<ImageView> (Resource.Id.sec_user_img);
			firstName = FindViewById<TextView> (Resource.Id.first_user_name);
			secName = FindViewById<TextView> (Resource.Id.sec_user_name);
			degree = FindViewById<TextView> (Resource.Id.percent);
		}
		#endregion

		#region Action Bar Configuration
		private void configureActionBar(){

			actionBar = FindViewById <ActionBar>(Resource.Id.actionBar);
			actionBar.SetLeftCornerImage (Resource.Drawable.back);

			actionBar.SetCenterImageText (Resource.Drawable.match_icon,Resources.GetString(Resource.String.matcher_menu));

			actionBar.SetRightBigImage (Resource.Drawable.clock_icon);
			var pastButton = actionBar.FindViewById <RelativeLayout> (Resource.Id.action_bar_right_btn);

			pastButton.Click += (sender, e) => {
				ViewModel.ShowPastMatches();
			};

			var backButton = actionBar.FindViewById <RelativeLayout> (Resource.Id.action_bar_left_btn);

			backButton.Click += (sender, e) => {
				ViewModel.Close();
			};
		}
		#endregion

		#region Create List Adapter
		private void CreateListAdapter ()
		{
			MatchedListAdapter adapter = new MatchedListAdapter (this,ViewModel.MatchResultList);
			resultlist.Adapter = adapter;
			resultlist.Invalidate ();	

			resultlist.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) => {
				ShareOnTwitter(e.Position);
			};
		}
		#endregion

		#region Apply Actions
		private void ApplyActions(){
			
		}
		#endregion


		#region Apply Data
		private void ApplyData(){
			try{
				firstName.Text = ""+ViewModel.FirstPersonNAME;
				secName.Text = "" + ViewModel.SecondPersonNAME;
			
				if (URLUtil.IsValidUrl(ViewModel.FirstPersonURL)) {
					Koush.UrlImageViewHelper.SetUrlDrawable (firstPersonImage,ViewModel.FirstPersonURL,Resource.Drawable.user_no_img);
				} else {
					string userEmail = ViewModel.GetUserData().UserEmail;
					if(ViewModel.FirstPersonNAME.Equals(userEmail))
					{
						if(Utilities.CurrentUserimage != null){
							firstPersonImage.SetImageBitmap(Utilities.CurrentUserimage);
						}else{
							firstPersonImage.SetImageResource(Resource.Drawable.user_no_img);
						}
					}else{
						firstPersonImage.SetImageResource(Resource.Drawable.user_no_img);
					}
				}

				if (URLUtil.IsValidUrl(ViewModel.SecondPersonURL)) {
					Koush.UrlImageViewHelper.SetUrlDrawable (secondPersonImage,ViewModel.SecondPersonURL,Resource.Drawable.user_no_img);
				} else {
					
					string userEmail = ViewModel.GetUserData().UserEmail;
					if(ViewModel.SecondPersonNAME.Equals(userEmail))
					{
						if(Utilities.CurrentUserimage != null){
							secondPersonImage.SetImageBitmap(Utilities.CurrentUserimage);
						}else{
							secondPersonImage.SetImageResource(Resource.Drawable.user_no_img);
						}
					}else{
						secondPersonImage.SetImageResource(Resource.Drawable.user_no_img);
					}
				}

				degree.Text = ""+ViewModel.MatchResult.Degrees+StringConstants.DEGREE_SYMBOL;
			}catch(Exception e){
				Mvx.Trace (e.StackTrace);
			}
		}
		#endregion

		public void ShareOnTwitter(int position)
		{

			TwitterService mTwitter=  new TwitterService {
				ConsumerKey = StringConstants.TWITTER_KEY,//"SD9KnCinDrqxJZ7eRTl6BbD77", 
				ConsumerSecret = StringConstants.TWITTER_SECRET,//"unJjpf51B5Ad3Lxt5I1qPfQj8u1SYE4cdXzk2vTkFJOkaszfQQ",
				CallbackUrl = new Uri (StringConstants.TWITTER_CALLBACK_URL)
			};

			Item item = new Item {
				Text = "Ancestor Cloud"
			};

			Intent intent = mTwitter.GetShareUI (this, item, shareResult => {
				//shareButton.Text = service.Title + " shared: " + shareResult;
				try{
					Toast.MakeText(this,""+shareResult,ToastLength.Short).Show();
				}catch(Exception e){
					Mvx.Trace(e.StackTrace);
				}
			});

			StartActivity (intent);
		}
	}

	#region List Adapter
	public class MatchedListAdapter : BaseAdapter
	{
		LayoutInflater inflater;
		RelationshipMatchDetailView myObj;
		List<RelationshipFindResult> dataList; 

		public MatchedListAdapter(RelationshipMatchDetailView myObj,List<RelationshipFindResult> data){
			this.myObj = myObj;
			dataList = data;
			inflater = (LayoutInflater)myObj.GetSystemService (Context.LayoutInflaterService);
		}

		public override int Count {
			get { return dataList.Count; }
		}

		public override Java.Lang.Object GetItem (int position) {
			// could wrap an item in a Java.Lang.Object
			// to return it here if needed
			return null;
		}

		public override long GetItemId (int position) {
			return position;
		}

		public override View GetView (int position, View convertView, ViewGroup parent)
		{
			MatchedViewHolder holder;

			if (convertView == null) {
				convertView = inflater.Inflate (Resource.Layout.matched_list_item, null);

				holder = new MatchedViewHolder ();

				holder.mainContainer = convertView.FindViewById<RelativeLayout> (Resource.Id.main_container_matched);

				holder.common_txt = convertView.FindViewById<TextView> (Resource.Id.common);
				holder.username = convertView.FindViewById<TextView> (Resource.Id.username);
				holder.year = convertView.FindViewById<TextView> (Resource.Id.year);
				holder.percent_right = convertView.FindViewById<TextView> (Resource.Id.percent_right);

				convertView.SetTag (Resource.Id.add_family_list,holder);
			} else {
				holder = (MatchedViewHolder)convertView.GetTag (Resource.Id.add_family_list);
			}

			if(dataList[position].CommonResult != null){
				string name = dataList [position].CommonResult.Name;
				holder.username.Text = name.Replace ("/","");
				holder.percent_right.Text = dataList [position].Degrees + StringConstants.DEGREE_SYMBOL;
				holder.year.Text = "";

				holder.common_txt.Visibility = ViewStates.Visible;
				holder.mainContainer.SetBackgroundColor (myObj.Resources.GetColor(Resource.Color.degree_circle_color));//Color.ParseColor("#ACD7DA"));
			}else{
				holder.common_txt.Visibility = ViewStates.Gone;
				holder.mainContainer.SetBackgroundColor (Color.Transparent);	
			}
			return convertView;
		}
	}

	public class MatchedViewHolder : Java.Lang.Object{

		public RelativeLayout mainContainer;
		public TextView common_txt,username,year,percent_right;

	}
	#endregion

}