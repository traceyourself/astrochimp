
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
	[Activity (Label = "CelebritiesView")]			
	public class CelebritiesView : BaseActivity
	{
		ActionBar actionBar;
		ListView celebList;
		EditText searchEd;
		RelativeLayout mePlus;

		public new CelebritiesViewModel ViewModel
		{
			get { return base.ViewModel as CelebritiesViewModel; }
			set { base.ViewModel = value; }
		}

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.celebrities_screen);

			initUI ();
			configureActionBar ();
			ApplyActions ();
			setCelebListAdapter ();
		}

		private void initUI()
		{
			celebList = FindViewById<ListView> (Resource.Id.celeb_list);
			searchEd = FindViewById<EditText> (Resource.Id.search_ed);
			mePlus = FindViewById<RelativeLayout> (Resource.Id.me_plus_box_right);
		}

		private void ApplyActions(){
			searchEd.AfterTextChanged += (object sender, Android.Text.AfterTextChangedEventArgs e) => {
				string data = searchEd.Text.ToString();
				data = data.Trim();
				if(data.Length == 0){
					ViewModel.GetCelebritiesData();
				}else{
					ViewModel.SearchKey = data;
				}
				setCelebListAdapter();
			};

			mePlus.Click += (object sender, EventArgs e) => {
				ViewModel.MePlusClicked();
			};

		}

		#region celebrity List Adapter
		private void setCelebListAdapter()
		{
			CelebrityListAdapter adapter = new CelebrityListAdapter (this,ViewModel.CelebritiesList);
			celebList.Adapter = adapter;
			celebList.Invalidate ();
		}
		#endregion

		#region Action Bar Configuration
		private void configureActionBar(){

			actionBar = FindViewById <ActionBar>(Resource.Id.actionBar);
			actionBar.SetLeftCornerImage (Resource.Drawable.back);

			actionBar.SetCenterImageText (Resource.Drawable.star_white,Resources.GetString(Resource.String.celebrities));

			var crossButton = actionBar.FindViewById <RelativeLayout> (Resource.Id.action_bar_left_btn);

			crossButton.Click += (sender, e) => {
				ViewModel.Close();
			};
		}
		#endregion
	}

	#region adapter
	public class CelebrityListAdapter : BaseAdapter
	{

		CelebritiesView myCelebObj;
		LayoutInflater inflater;
		List<Celebrity> dataList;

		public CelebrityListAdapter(CelebritiesView myCelebObj,List<Celebrity> dataList){
			this.myCelebObj = myCelebObj;
			this.dataList = dataList;
			inflater = (LayoutInflater)myCelebObj.GetSystemService (Context.LayoutInflaterService);
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
			CelebViewHolder holder;

			if (convertView == null) {
				convertView = inflater.Inflate (Resource.Layout.celebs_list_item, null);

				holder = new CelebViewHolder ();

				holder.userImg = convertView.FindViewById<ImageView> (Resource.Id.user_img);
				holder.plus = convertView.FindViewById<RelativeLayout> (Resource.Id.plus_box_right);

				holder.nametxt = convertView.FindViewById<TextView> (Resource.Id.username);

				convertView.SetTag (Resource.Id.celeb_list,holder);
			} else {
				holder = (CelebViewHolder)convertView.GetTag (Resource.Id.celeb_list);
			}

			holder.nametxt.Text = dataList[position].GivenNames+" "+dataList[position].LastName;

			holder.plus.Click += (object sender, EventArgs e) => {

				myCelebObj.ViewModel.CelebrityPlusClickHandler(dataList[position]);

			};

			return convertView;
		}
	}

	public class CelebViewHolder : Java.Lang.Object{

		public RelativeLayout plus;
		public ImageView userImg;
		public TextView nametxt;

	}
	#endregion

}

