
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
	[Activity (Label = "ContactsView")]			
	public class ContactsView : BaseActivity
	{
		ActionBar actionBar;
		ListView contactList;
		RelativeLayout mePlus;

		public new ContactsViewModel ViewModel
		{
			get { return base.ViewModel as ContactsViewModel; }
			set { base.ViewModel = value; }
		}

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.contacts_screen);

			initUI ();
			configureActionBar ();
			ApplyActions ();
			setCelebListAdapter ();
		}

		private void initUI()
		{
			contactList = FindViewById<ListView> (Resource.Id.contacts_list);
			mePlus = FindViewById<RelativeLayout> (Resource.Id.me_plus_box_right);
		}

		private void ApplyActions(){
			mePlus.Click += (object sender, EventArgs e) => {
				ViewModel.MePlusClicked();
			};
		}

		#region List Adapter
		private void setCelebListAdapter()
		{
			ContactsListAdapter adapter = new ContactsListAdapter (this,ViewModel.ContactsList);
			contactList.Adapter = adapter;
			contactList.Invalidate ();
		}
		#endregion

		#region Action Bar Configuration
		private void configureActionBar(){

			actionBar = FindViewById <ActionBar>(Resource.Id.actionBar);
			actionBar.SetLeftCornerImage (Resource.Drawable.back);

			actionBar.SetCenterImageText (Resource.Drawable.contacts_white,Resources.GetString(Resource.String.contacts));

			var crossButton = actionBar.FindViewById <RelativeLayout> (Resource.Id.action_bar_left_btn);

			crossButton.Click += (sender, e) => {
				ViewModel.Close();
			};
		}
		#endregion
	}

	#region adapter
	public class ContactsListAdapter : BaseAdapter
	{
		ContactsView mycontObj;
		LayoutInflater inflater;
		List<People> dataList;

		public ContactsListAdapter(ContactsView mycontObj,List<People> dataList){
			this.mycontObj = mycontObj;
			this.dataList = dataList;
			inflater = (LayoutInflater)mycontObj.GetSystemService (Context.LayoutInflaterService);
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
			ContViewHolder holder;

			if (convertView == null) {
				convertView = inflater.Inflate (Resource.Layout.celebs_list_item, null);

				holder = new ContViewHolder ();

				holder.userImg = convertView.FindViewById<ImageView> (Resource.Id.user_img);
				holder.plus = convertView.FindViewById<RelativeLayout> (Resource.Id.plus_box_right);

				holder.nametxt = convertView.FindViewById<TextView> (Resource.Id.username);

				convertView.SetTag (Resource.Id.celeb_list,holder);
			} else {
				holder = (ContViewHolder)convertView.GetTag (Resource.Id.celeb_list);
			}

			holder.nametxt.Text = dataList[position].Name;

			holder.plus.Click += (object sender, EventArgs e) => {
				mycontObj.ViewModel.Close();
			};

			return convertView;
		}
	}

	public class ContViewHolder : Java.Lang.Object{

		public RelativeLayout plus;
		public ImageView userImg;
		public TextView nametxt;

	}
	#endregion

}

