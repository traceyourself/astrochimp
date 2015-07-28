
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AncestorCloud.Shared.ViewModels;
using Cirrious.CrossCore;
using Android.Graphics;
using Android.Content.PM;
using Cirrious.MvvmCross.Plugins.Messenger;
using AncestorCloud.Core;
using AncestorCloud.Shared;
using System.Collections.Generic;
using Java.Lang;

namespace AncestorCloud.Droid
{
	[Activity (Label = "PastMatchesView", ScreenOrientation = ScreenOrientation.Portrait)]			
	public class PastMatchesView : BaseActivity
	{
		
		ActionBar actionBar;
		ListView matchlist;
		RelativeLayout noMatchLay;
		IMvxMessenger _messenger;
		private MvxSubscriptionToken ReloadViewToken;

		public new PastMatchesViewModel ViewModel
		{
			get { return base.ViewModel as PastMatchesViewModel; }
			set { base.ViewModel = value; }
		}

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			// Create your application here
			SetContentView(Resource.Layout.past_matches);
			_messenger = Mvx.Resolve<IMvxMessenger>();
			initUI ();
			configureActionBar ();

			ApplyActions ();
			//CreateListAdapter ();

		}

		#region init ui
		private void initUI()
		{
			matchlist = FindViewById<ListView> (Resource.Id.past_matched_list);
			noMatchLay = FindViewById<RelativeLayout> (Resource.Id.no_match_lay);
		}
		#endregion

		#region Action Bar Configuration
		private void configureActionBar(){

			actionBar = FindViewById <ActionBar>(Resource.Id.actionBar);
			actionBar.SetLeftCornerImage (Resource.Drawable.back);

			actionBar.SetCenterImageText (Resource.Drawable.clock_icon,Resources.GetString(Resource.String.past_matches));

			var backButton = actionBar.FindViewById <RelativeLayout> (Resource.Id.action_bar_left_btn);

			backButton.Click += (sender, e) => {
				ViewModel.Close();
			};

		}
		#endregion

		#region Create List Adapter
		private void CreateListAdapter ()
		{
			if(ViewModel.PastMatchesList != null){
				PastMatchedListAdapter adapter = new PastMatchedListAdapter (this,ViewModel.PastMatchesList);
				matchlist.Adapter = adapter;
				matchlist.Invalidate ();	
			}else{
				NoMatchesFound();
			}
		}
		#endregion

		#region onresume
		protected override void OnResume ()
		{
			base.OnResume ();
			ReloadViewToken = _messenger.SubscribeOnMainThread<PastMatchesLoadedMessage>(Message => this.CreateListAdapter ());
			ViewModel.GetPastMatchesData ();
		}
		#endregion


		protected override void OnPause ()
		{
			base.OnPause ();
			_messenger.Unsubscribe<PastMatchesLoadedMessage> (ReloadViewToken);
		}


		public void NoMatchesFound(){
			matchlist.Visibility = ViewStates.Gone;
			noMatchLay.Visibility = ViewStates.Visible;
		}

		#region Apply Actions
		private void ApplyActions(){
			
		}
		#endregion
	}

	#region List Adapter
	public class PastMatchedListAdapter : BaseAdapter
	{
		LayoutInflater inflater;
		PastMatchesView myObj;
		List<RelationshipFindResult> dataList;


		public PastMatchedListAdapter(PastMatchesView myObj,List<RelationshipFindResult> dataList){
			this.myObj = myObj;
			this.dataList = dataList;
			inflater = (LayoutInflater)myObj.GetSystemService (Context.LayoutInflaterService);
		}

		public override int Count {
			//get { return dataList.Count; }
			get{ return dataList.Count;}
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
			MatchedHistoryViewHolder holder;

			if (convertView == null) {
				convertView = inflater.Inflate (Resource.Layout.matcher_history_list_item, null);

				holder = new MatchedHistoryViewHolder ();

				holder.firstuser_name = convertView.FindViewById<TextView> (Resource.Id.first_user_name);
				holder.secuser_name = convertView.FindViewById<TextView> (Resource.Id.sec_user_name);
				holder.matchPercent = convertView.FindViewById<TextView> (Resource.Id.percent);

				holder.firstUserImage = convertView.FindViewById<ImageView> (Resource.Id.first_user_img);
				holder.secUserImage = convertView.FindViewById<ImageView> (Resource.Id.sec_user_img);

				convertView.SetTag (Resource.Id.parent_sibling_list,holder);
			} else {
				holder = (MatchedHistoryViewHolder)convertView.GetTag (Resource.Id.parent_sibling_list);
			}

			RelationshipFindResult resultModel = dataList [position];

			holder.firstuser_name.Text = resultModel.FirstPerson.Name;
			holder.secuser_name.Text = resultModel.SecondPerson.Name;

			try{
				Koush.UrlImageViewHelper.SetUrlDrawable (holder.firstUserImage,resultModel.FirstPerson.ProfilePicURL, Resource.Drawable.user_no_img_small);
			}catch(Exception e)
			{
				Mvx.Trace (e.StackTrace);
			}

			try{
				Koush.UrlImageViewHelper.SetUrlDrawable (holder.secUserImage,resultModel.SecondPerson.ProfilePicURL, Resource.Drawable.user_no_img_small);
			}catch(Exception e)
			{
				Mvx.Trace (e.StackTrace);
			}

			holder.matchPercent.Text = resultModel.Degrees + StringConstants.DEGREE_SYMBOL;


			return convertView;
		}
	}

	public class MatchedHistoryViewHolder : Java.Lang.Object{

		public ImageView firstUserImage,secUserImage;
		public TextView firstuser_name,secuser_name,matchPercent;

	}
	#endregion

}