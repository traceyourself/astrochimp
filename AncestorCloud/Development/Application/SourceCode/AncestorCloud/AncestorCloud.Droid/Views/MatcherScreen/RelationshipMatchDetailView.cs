
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

namespace AncestorCloud.Droid
{
	[Activity (Label = "RelationshipMatchDetailView")]			
	public class RelationshipMatchDetailView : BaseActivity
	{
		
		ActionBar actionBar;
		ListView resultlist;

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
		}

		#region init ui
		private void initUI()
		{
			resultlist = FindViewById<ListView> (Resource.Id.matched_list);
		}
		#endregion

		#region Action Bar Configuration
		private void configureActionBar(){

			actionBar = FindViewById <ActionBar>(Resource.Id.actionBar);
			actionBar.SetLeftCornerImage (Resource.Drawable.back);

			actionBar.SetCenterImageText (Resource.Drawable.action_menu,Resources.GetString(Resource.String.matcher_menu));

			actionBar.SetRightBigImage (Resource.Drawable.action_menu);
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
			MatchedListAdapter adapter = new MatchedListAdapter (this);
			resultlist.Adapter = adapter;
			resultlist.Invalidate ();	

		}
		#endregion

		#region Apply Actions
		private void ApplyActions(){
			
		}
		#endregion

	}


	#region List Adapter
	public class MatchedListAdapter : BaseAdapter
	{
		LayoutInflater inflater;
		RelationshipMatchDetailView myObj;

		public MatchedListAdapter(RelationshipMatchDetailView myObj){
			this.myObj = myObj;
			inflater = (LayoutInflater)myObj.GetSystemService (Context.LayoutInflaterService);
		}

		public override int Count {
			//get { return dataList.Count; }
			get{ return 10;}
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

				convertView.SetTag (Resource.Id.add_family_list,holder);
			} else {
				holder = (MatchedViewHolder)convertView.GetTag (Resource.Id.add_family_list);
			}

			if (position == 5) {
				holder.common_txt.Visibility = ViewStates.Visible;
				holder.mainContainer.SetBackgroundColor (Color.ParseColor("#94C4EC"));
			} else {
				holder.common_txt.Visibility = ViewStates.Gone;
				holder.mainContainer.SetBackgroundColor (Color.Transparent);
			}

			return convertView;
		}
	}

	public class MatchedViewHolder : Java.Lang.Object{

		public RelativeLayout mainContainer;//main_container_matched;
		public TextView common_txt;

	}
	#endregion

}