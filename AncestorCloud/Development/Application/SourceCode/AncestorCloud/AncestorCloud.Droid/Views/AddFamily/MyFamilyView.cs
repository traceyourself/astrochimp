
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
using AncestorCloud.Shared;
using AncestorCloud.Shared.ViewModels;

namespace AncestorCloud.Droid
{
	[Activity (Label = "AddFamilyView")]			
	public class MyFamilyView : BaseActivity
	{

		ListView listView;
		ActionBar actionBar;
		TextView FbBtn,loginBtn;
		List<ListDataStructure> dataList;


		public new MyFamilyViewModel ViewModel
		{
			get { return base.ViewModel as MyFamilyViewModel; }
			set { base.ViewModel = value; }
		}

		#region oncreate
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.add_family);
			// Create your application here

			InitViews ();
			ConfigureActionBar ();
			ApplyActions ();
			CreateListAdapter ();

		}
		#endregion

		#region customized methods
		private void InitViews()
		{
			actionBar = FindViewById<ActionBar> (Resource.Id.actionBar);
			listView = FindViewById<ListView> (Resource.Id.add_family_list);
		}

		private void ConfigureActionBar()
		{
			actionBar.SetCenterImageText (Resource.Drawable.myfamily_title,Resources.GetString(Resource.String.my_family_menu));
			actionBar.SetLeftCornerImage (Resource.Drawable.back);
			var backButton = actionBar.FindViewById <RelativeLayout> (Resource.Id.action_bar_left_btn);

			backButton.Click += (sender, e) => {
				ViewModel.Close();
			};
		}

		private void ApplyActions()
		{
			
		}
		#endregion


		#region Create List Adapter
		private void CreateListAdapter ()
		{
			/*dataList = new List<ListDataStructure> ();

			ListDataStructure first = new ListDataStructure (true,false,false,"Siblings","","","");
			dataList.Add (first);
			first = new ListDataStructure (false,false,true,"","","Madison Eames (sister)","1991");
			dataList.Add (first);
			first = new ListDataStructure (false,true,false,"","Add Sibling","","");
			dataList.Add (first);

			first = new ListDataStructure (true,false,false,"Parents","","","");
			dataList.Add (first);
			first = new ListDataStructure (false,false,true,"","","Robert Eames (father)","1956");
			dataList.Add (first);
			first = new ListDataStructure (false,false,true,"","","Mary Herzog Eames (mother)","1958");
			dataList.Add (first);
			first = new ListDataStructure (false,true,false,"","Add Parent","","");
			dataList.Add (first);

			first = new ListDataStructure (true,false,false,"Grandparents","","","");
			dataList.Add (first);
			first = new ListDataStructure (false,false,true,"","","Merrill Eames (Grandfather)","1930");
			dataList.Add (first);
			first = new ListDataStructure (false,false,true,"","","Louise Gibbons Eames (Grandmother)","1935");
			dataList.Add (first);
			first = new ListDataStructure (false,false,true,"","","Evevon Jennings Herzog (Grandmother)","1935");
			dataList.Add (first);
			first = new ListDataStructure (false,true,false,"","Add Grandparent","","");
			dataList.Add (first);

			first = new ListDataStructure (true,false,false,"Geart Grandparents","","","");
			dataList.Add (first);
			first = new ListDataStructure (false,true,false,"","Add Geart Grandparents","","");  
			dataList.Add (first);*/


			//if(ViewModel.FamilyList.Count > 0){

				dataList = FilterDataList (ViewModel.FamilyList);

				MyFamilyListAdapter adapter = new MyFamilyListAdapter (this,dataList);
				listView.Adapter = adapter;
				listView.Invalidate ();	
			//}


		}
		#endregion

		#region list filteration
		public List<ListDataStructure> FilterDataList(List<People> mainList){

			List<ListDataStructure> resultList = new List<ListDataStructure> ();

			List<ListDataStructure> siblingList = new List<ListDataStructure> ();
			List<ListDataStructure> parentList = new List<ListDataStructure> ();
			List<ListDataStructure> grandParentList = new List<ListDataStructure> ();
			List<ListDataStructure> greatGrandParentList = new List<ListDataStructure> ();

			ListDataStructure listStruct;



			for(int i=0;i<mainList.Count;i++){
				People item = mainList[i];
				string relation = item.Relation;

				if (relation.Contains (Resources.GetString(Resource.String.Brother_comparison)) || relation.Contains (Resources.GetString(Resource.String.Sister_comparison))) 
				{
					listStruct = new ListDataStructure(false,false,true,"","",item);
					siblingList.Add (listStruct);
				}

				else if(relation.Contains (Resources.GetString(Resource.String.Father_comparison)) || relation.Contains (Resources.GetString(Resource.String.Mother_comparison)))
				{
					listStruct = new ListDataStructure(false,false,true,"","",item);
					parentList.Add (listStruct);
				}

				else if(relation.Contains (Resources.GetString(Resource.String.GrandFather_comparison)) || relation.Contains (Resources.GetString(Resource.String.GrandMother_comparison)))
				{
					listStruct = new ListDataStructure(false,false,true,"","",item);
					grandParentList.Add (listStruct);
				}

				else if(relation.Contains (Resources.GetString(Resource.String.GreatGrandFather_comparison)) || relation.Contains (Resources.GetString(Resource.String.GreatGrandMother_comparison)))
				{
					listStruct = new ListDataStructure(false,false,true,"","",item);
					greatGrandParentList.Add (listStruct);
				}
			}

			//Siblings==
			listStruct = new ListDataStructure(true,false,false,Resources.GetString(Resource.String.Sibling_header),"",null);
			resultList.Add (listStruct);
			if(siblingList.Count > 0){
				for(int i=0;i<siblingList.Count;i++){
					resultList.Add (siblingList[i]);
				}
			}
			listStruct = new ListDataStructure(false,true,false,"",Resources.GetString(Resource.String.Sibling_Footer),null);
			resultList.Add (listStruct);
			//========

			//Parents==
			listStruct = new ListDataStructure(true,false,false,Resources.GetString(Resource.String.Parent_header),"",null);
			resultList.Add (listStruct);
			if(parentList.Count > 0){
				for(int i=0;i<parentList.Count;i++){
					resultList.Add (parentList[i]);
				}
			}
			listStruct = new ListDataStructure(false,true,false,"",Resources.GetString(Resource.String.Parent_Footer),null);
			resultList.Add (listStruct);
			//=========

			//GrandParents==
			listStruct = new ListDataStructure(true,false,false,Resources.GetString(Resource.String.Grandparent_header),"",null);
			resultList.Add (listStruct);
			if(grandParentList.Count > 0){
				for(int i=0;i<grandParentList.Count;i++){
					resultList.Add (grandParentList[i]);
				}
			}
			listStruct = new ListDataStructure(false,true,false,"",Resources.GetString(Resource.String.Grandparent_Footer),null);
			resultList.Add (listStruct);
			//=========

			//Great Grand Parents==
			listStruct = new ListDataStructure(true,false,false,Resources.GetString(Resource.String.Greatgrandparent_header),"",null);
			resultList.Add (listStruct);
			if(greatGrandParentList.Count > 0){
				for(int i=0;i<greatGrandParentList.Count;i++){
					resultList.Add (greatGrandParentList[i]);
				}
			}
			listStruct = new ListDataStructure(false,true,false,"",Resources.GetString(Resource.String.GreatGrandparent_Footer),null);
			resultList.Add (listStruct);
			//=========

			return resultList;
		}
		#endregion

	}



	#region List Adapter
	public class MyFamilyListAdapter : BaseAdapter
	{
		MyFamilyView myFamilyObj;
		LayoutInflater inflater;
		List<ListDataStructure> dataList;

		public MyFamilyListAdapter(MyFamilyView myFamilyObj,List<ListDataStructure> dataList){
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
			ViewHolder holder;

			if (convertView == null) {
				convertView = inflater.Inflate (Resource.Layout.add_family_list_item, null);

				holder = new ViewHolder ();

				holder.listFooter = convertView.FindViewById<LinearLayout> (Resource.Id.list_footer);
				holder.listHeader = convertView.FindViewById<RelativeLayout> (Resource.Id.list_header);
				holder.listData = convertView.FindViewById<RelativeLayout> (Resource.Id.list_data);
				holder.editBtn = convertView.FindViewById<RelativeLayout> (Resource.Id.edit_btn);

				holder.headerTxt = convertView.FindViewById<TextView> (Resource.Id.header_txt);
				holder.nameTxt = convertView.FindViewById<TextView> (Resource.Id.name_txt);
				holder.yearTxt = convertView.FindViewById<TextView> (Resource.Id.year_txt);
				holder.footerTxt = convertView.FindViewById<TextView> (Resource.Id.footer_txt);

				//Java.Lang.Object holderObj = (Java.Lang.Object)holder;
				convertView.SetTag (Resource.Id.add_family_list,holder);
			} else {
				holder = (ViewHolder)convertView.GetTag (Resource.Id.add_family_list);
			}

			ListDataStructure structure = dataList[position];

			if(structure.isHeader)
			{
				holder.headerTxt.Text = structure.HeaderTitle;
				holder.listHeader.Visibility = ViewStates.Visible;
				holder.listFooter.Visibility = ViewStates.Gone;
				holder.listData.Visibility = ViewStates.Gone;
			}
			else if(structure.isData)
			{
				holder.nameTxt.Text = structure.PersonData.Name;
				holder.yearTxt.Text = structure.PersonData.DateOfBirth;
				holder.listHeader.Visibility = ViewStates.Gone;
				holder.listFooter.Visibility = ViewStates.Gone;
				holder.listData.Visibility = ViewStates.Visible;

			}
			else if(structure.isFooter)
			{
				holder.footerTxt.Text = structure.FooterTitle;
				holder.listHeader.Visibility = ViewStates.Gone;
				holder.listFooter.Visibility = ViewStates.Visible;
				holder.listData.Visibility = ViewStates.Gone;
			}

			/*var view = convertView ?? inflater.Inflate (Resource.Layout.add_family_list_item, parent, false);

			var contactName = view.FindViewById<TextView> (Resource.Id.ContactName);
			var contactImage = view.FindViewById<ImageView> (Resource.Id.ContactImage);

			contactName.Text = _contactList [position].DisplayName;

			if (_contactList [position].PhotoId == null) {
				contactImage = view.FindViewById<ImageView> (Resource.Id.ContactImage);
				contactImage.SetImageResource (Resource.Drawable.contactImage);
			}  else {
				var contactUri = ContentUris.WithAppendedId (
					ContactsContract.Contacts.ContentUri, _contactList [position].Id);
				var contactPhotoUri = Android.Net.Uri.WithAppendedPath (contactUri,
					Contacts.Photos.ContentDirectory);
				contactImage.SetImageURI (contactPhotoUri);
			}*/

			return convertView;
		}
	}

	public class ViewHolder : Java.Lang.Object{

		public LinearLayout listFooter;
		public RelativeLayout listHeader,listData,editBtn;
		public TextView headerTxt,nameTxt,yearTxt,footerTxt;

	}
	#endregion

	#region DataStructure for list
	public class ListDataStructure
	{
		public bool isHeader{ get; set;}
		public bool isFooter{ get; set;} 
		public bool isData{ get; set;}

		public String HeaderTitle{ get; set;}
		public String FooterTitle{ get; set;}
		public People PersonData{ get; set;}



		public ListDataStructure(bool isheader,bool isfooter,bool isdata,String headertitle,String footertitle,People persondata){
			isHeader = isheader;
			isFooter = isfooter;
			isData = isdata;

			HeaderTitle = headertitle;
			FooterTitle = footertitle;
			PersonData = persondata;

		}

	}
	#endregion

}

