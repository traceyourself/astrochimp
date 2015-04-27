
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

namespace AncestorCloud.Droid
{
	[Activity (Label = "MyFamilyFacebookView")]			
	public class MyFamilyFacebookView : BaseActivity
	{
		ListView listView;
		ActionBar actionBar;
		List<FBListDataStructure> dataList;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.my_family_facebook);

			InitViews ();
			ConfigureActionBar ();
			ApplyActions ();
			CreateListAdapter ();

		}


		#region customized methods
		private void InitViews()
		{
			actionBar = FindViewById<ActionBar> (Resource.Id.actionBar);
			listView = FindViewById<ListView> (Resource.Id.fb_family_list);
		}

		private void ConfigureActionBar()
		{
			actionBar.SetCenterImageText (Resource.Drawable.action_menu,Resources.GetString(Resource.String.my_family_menu));
			actionBar.SetLeftCornerMenuImage (Resource.Drawable.action_menu);
		}

		private void ApplyActions()
		{

		}
		#endregion


		#region Create List Adapter
		private void CreateListAdapter ()
		{
			dataList = new List<FBListDataStructure> ();

			FBListDataStructure first = new FBListDataStructure (false,"Robert Eames(father)","");
			dataList.Add (first);
			first = new FBListDataStructure (false,"Robert Eames(father)","");
			dataList.Add (first);
			first = new FBListDataStructure (false,"Robert Eames(father)","");
			dataList.Add (first);
			first = new FBListDataStructure (false,"Robert Eames(father)","");
			dataList.Add (first);
			first = new FBListDataStructure (false,"Robert Eames(father)","");
			dataList.Add (first);
			first = new FBListDataStructure (false,"Robert Eames(father)","");
			dataList.Add (first);
			first = new FBListDataStructure (false,"Robert Eames(father)","");
			dataList.Add (first);
			first = new FBListDataStructure (false,"Robert Eames(father)","");
			dataList.Add (first);
			first = new FBListDataStructure (false,"Robert Eames(father)","");
			dataList.Add (first);
			first = new FBListDataStructure (false,"Robert Eames(father)","");
			dataList.Add (first);
			first = new FBListDataStructure (false,"Robert Eames(father)","");
			dataList.Add (first);
			first = new FBListDataStructure (false,"Robert Eames(father)","");
			dataList.Add (first);
			first = new FBListDataStructure (false,"Robert Eames(father)","");
			dataList.Add (first);
			first = new FBListDataStructure (false,"Robert Eames(father)","");
			dataList.Add (first);
			first = new FBListDataStructure (false,"Robert Eames(father)","");
			dataList.Add (first);
			first = new FBListDataStructure (false,"Robert Eames(father)","");
			dataList.Add (first);


			FbFamilyListAdapter adapter = new FbFamilyListAdapter (this,dataList);
			listView.Adapter = adapter;
			listView.Invalidate ();
		}
		#endregion
	}

	#region List Adapter
	public class FbFamilyListAdapter : BaseAdapter
	{
		MyFamilyFacebookView myFamilyObj;
		LayoutInflater inflater;
		List<FBListDataStructure> dataList;

		public FbFamilyListAdapter(MyFamilyFacebookView myFamilyObj,List<FBListDataStructure> dataList){
			this.myFamilyObj = myFamilyObj;
			this.dataList = dataList;
			inflater = (LayoutInflater)myFamilyObj.GetSystemService (Context.LayoutInflaterService);
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
			FBViewHolder holder;

			if (convertView == null) {
				convertView = inflater.Inflate (Resource.Layout.fb_family_list_item, null);

				holder = new FBViewHolder ();

				holder.userImg = convertView.FindViewById<ImageView> (Resource.Id.user_img);
				holder.check = convertView.FindViewById<CheckBox> (Resource.Id.check_box_right);
			
				holder.nametxt = convertView.FindViewById<TextView> (Resource.Id.username);

				//Java.Lang.Object holderObj = (Java.Lang.Object)holder;
				convertView.SetTag (Resource.Id.fb_family_list,holder);
			} else {
				holder = (FBViewHolder)convertView.GetTag (Resource.Id.fb_family_list);
			}

			FBListDataStructure structure = dataList[position];

			if(structure.isSelected)
			{
				holder.check.Checked = true;
			}
			else {
				holder.check.Checked = false;
			}

			holder.nametxt.Text = structure.Name;

			return convertView;
		}
	}

	public class FBViewHolder : Java.Lang.Object{

		public CheckBox check;
		public ImageView userImg;
		public TextView nametxt;

	}
	#endregion

	#region DataStructure for list
	public class FBListDataStructure
	{
		public bool isSelected{ get; set;}

		public String Name{ get; set;}
		public String ImageUrl{ get; set;}

		public FBListDataStructure(bool isselected,String name,String img){
			isSelected = isselected;

			Name = name;
			ImageUrl = img;
		}

	}
	#endregion
}

