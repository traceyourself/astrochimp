﻿
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
using Android.Graphics;
using Java.Text;
using Java.Util;
using Cirrious.CrossCore;

namespace AncestorCloud.Droid
{
	[Activity (Label = "MyFamilyView")]			
	public class MyFamilyView : BaseActivity
	{

		ListView listView;
		ActionBar actionBar;
		TextView FbBtn,loginBtn;
		public TextView birthDateDialogTxt;
		List<ListDataStructure> dataList;
		Dialog editDialog;
		ImageView helpIcon;

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
			helpIcon = FindViewById<ImageView> (Resource.Id.question_icon);
		}

		private void ConfigureActionBar()
		{
			actionBar.SetCenterImageText (Resource.Drawable.myfamily_title,Resources.GetString(Resource.String.my_family_menu));
			actionBar.SetLeftCornerImage (Resource.Drawable.back);
			var backButton = actionBar.FindViewById <RelativeLayout> (Resource.Id.action_bar_left_btn);

			backButton.Click += (sender, e) => {
				if(Utilities.LoggedInUsingFb){
					Utilities.LoggedInUsingFb = false;
					ViewModel.ShowFamilyViewModel();
				}
				ViewModel.Close();
			};

			helpIcon.Click += (object sender, EventArgs e) => {
				new HelpDialog(this).ShowHelpDialog();
			};

		}

		private void ApplyActions()
		{
			
		}
		#endregion

		public override void OnBackPressed ()
		{
			if(Utilities.LoggedInUsingFb){
				Utilities.LoggedInUsingFb = false;
				ViewModel.ShowFamilyViewModel();
			}
			ViewModel.Close();

		}

		#region Create List Adapter
		private void CreateListAdapter ()
		{
			dataList = FilterDataList (ViewModel.FamilyList);

			MyFamilyListAdapter adapter = new MyFamilyListAdapter (this,dataList);
			listView.Adapter = adapter;
			listView.Invalidate ();	

		}
		#endregion

		#region list filteration
		public List<ListDataStructure> FilterDataList(List<People> mainList)
		{

			List<ListDataStructure> resultList = new List<ListDataStructure> ();

			List<ListDataStructure> siblingList = new List<ListDataStructure> ();
			List<ListDataStructure> parentList = new List<ListDataStructure> ();
			List<ListDataStructure> grandParentList = new List<ListDataStructure> ();
			List<ListDataStructure> greatGrandParentList = new List<ListDataStructure> ();

			ListDataStructure listStruct;

			for(int i=0;i<mainList.Count;i++){
				People item = mainList[i];
				string relation = item.Relation;

				if (relation.Contains (StringConstants.Brother_comparison) || relation.Contains (StringConstants.Sister_comparison)) 
				{
					listStruct = new ListDataStructure(false,false,true,"","",item);
					siblingList.Add (listStruct);
				}

				else if(relation.Contains (StringConstants.Father_comparison) || relation.Contains (StringConstants.Mother_comparison))
				{
					listStruct = new ListDataStructure(false,false,true,"","",item);
					parentList.Add (listStruct);
				}

				else if(relation.Contains (StringConstants.GrandFather_comparison) || relation.Contains (StringConstants.GrandMother_comparison))
				{
					listStruct = new ListDataStructure(false,false,true,"","",item);
					grandParentList.Add (listStruct);
				}

				else if(relation.Contains (StringConstants.GreatGrandFather_comparison) || relation.Contains (StringConstants.GreatGrandMother_comparison))
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

		#region Edit Dialog
		public void ShowEditDialog(int position)
		{
			
			editDialog = new Dialog (this,Android.Resource.Style.ThemeTranslucentNoTitleBar);
			editDialog.SetContentView (Resource.Layout.edit_family_dialog);

			TextView nameTitle = editDialog.FindViewById<TextView> (Resource.Id.name_title);
			LinearLayout male = editDialog.FindViewById<LinearLayout> (Resource.Id.male_container);
			LinearLayout female = editDialog.FindViewById<LinearLayout> (Resource.Id.female_container);
			RelativeLayout crossbtn = editDialog.FindViewById<RelativeLayout> (Resource.Id.cross_edit_btn);
			birthDateDialogTxt = editDialog.FindViewById<TextView> (Resource.Id.birth_year_field);
			TextView saveBtn = editDialog.FindViewById<TextView> (Resource.Id.save_btn);

			EditText first_name = editDialog.FindViewById<EditText> (Resource.Id.first_name_field);
			EditText mid_name = editDialog.FindViewById<EditText> (Resource.Id.mid_name_field);
			EditText last_name = editDialog.FindViewById<EditText> (Resource.Id.last_name_field);
			EditText birth_loc = editDialog.FindViewById<EditText> (Resource.Id.birth_loc_field);
			TextView year_field = editDialog.FindViewById<TextView> (Resource.Id.birth_year_field);
			Spinner yearSelector = editDialog.FindViewById<Spinner> (Resource.Id.year_selector);

			male.Click += (object sender, EventArgs e) => {
				male.SetBackgroundResource(Resource.Drawable.male_selected);	
				female.SetBackgroundColor(Color.ParseColor("#00000000"));
			};

			female.Click += (object sender, EventArgs e) => {
				male.SetBackgroundColor(Color.ParseColor("#00000000"));	
				female.SetBackgroundResource(Resource.Drawable.female_selected);
			};

			birthDateDialogTxt.Click += (object sender, EventArgs e) => {
				//ShowDatePicker();
				yearSelector.PerformClick();
			};

			crossbtn.Click += (object sender, EventArgs e) => {
				editDialog.Dismiss();
			};

			saveBtn.Click += (object sender, EventArgs e) => {
				editDialog.Dismiss();
			};


			List<string> populateList = new List<string> ();

			Calendar cal = Calendar.GetInstance (Java.Util.Locale.Us);
			int start = 1900;
			int upto = cal.Get (Calendar.Year);

			for(int i=start;i<=upto;i++){
				populateList.Add (""+i);
			}

			var adapter = new ArrayAdapter (this,Android.Resource.Layout.SimpleListItem1,populateList);

			//adapter.SetDropDownViewResource (Android.Resource.Layout.SimpleSpinnerDropDownItem);
			yearSelector.Adapter = adapter;

			yearSelector.ItemSelected += (object sender, AdapterView.ItemSelectedEventArgs e) => {
				year_field.Text = yearSelector.SelectedItem.ToString();
			};

			//Set data Accordingly===
			People peopleData = dataList[position].PersonData;

			try{
				try{
					first_name.Text = peopleData.FirstName;
					mid_name.Text = peopleData.MiddleName;
					last_name.Text = peopleData.LastName;
					birth_loc.Text = peopleData.BirthLocation;
					nameTitle.Text = peopleData.FirstName+" "+peopleData.MiddleName+" "+peopleData.LastName+"("+peopleData.Relation+")";

					if (peopleData.Gender.Equals ("Male")) {
						male.PerformClick ();
					} else {
						female.PerformClick ();
					}
				}catch(Exception e){
					Mvx.Trace(e.Message);
				}

				//Year Selection process==
				int diff = GetYearPosition(start,peopleData.DateOfBirth);
				yearSelector.SetSelection(diff);
				//=========
			}catch(Exception e){
				Mvx.Trace (e.StackTrace);
			}
			//=====

			editDialog.Show ();
		}
		#endregion


		#region getYear for spinner
		public int GetYearPosition(int start,string date)
		{
			string year = "";
			try{
				if(date.Length > 0){
					if(date.Contains("-")){
						string []arr = date.Split(new char[]{'-'},5);
						year = arr[arr.Length-1];
					}else{
						year = date;
					}	
				}
			}catch(Exception e){
				Mvx.Trace (e.Message);
			}

			int diff = 0;
			if(year.Length == 4){
				int yy = int.Parse(year);
				if(yy > start){
					diff = yy-start;
				}
			}
			return diff;
		}
		#endregion



		public void ShowDatePicker()
		{
			Calendar cal = Calendar.GetInstance (Java.Util.Locale.Us);
			DatePickerDialog dpd = new DatePickerDialog (this,new DateListener(this),cal.Get(Calendar.Year), cal.Get(Calendar.Month),cal.Get(Calendar.DayOfMonth));
			dpd.Show ();
		}

	}

	public class DateListener : Java.Lang.Object,Android.App.DatePickerDialog.IOnDateSetListener
	{
		MyFamilyView obj;
		SimpleDateFormat dateFormatter;

		public DateListener(MyFamilyView obj)
		{
			this.obj = obj;
			dateFormatter = new SimpleDateFormat("dd-MM-yyyy", Java.Util.Locale.Us);
		}

		public void OnDateSet(DatePicker view, int year, int monthOfYear, int dayOfMonth) {
			Calendar newDate = Calendar.GetInstance(Java.Util.Locale.Us);
			newDate.Set(year, monthOfYear, dayOfMonth);

			obj.birthDateDialogTxt.Text = ""+dateFormatter.Format(newDate.Time);
		}

		public void Dispose(){}
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

				holder.editBtn.Click += (object sender, EventArgs e) => {
					//System.Diagnostics.Debug.WriteLine("edit clicked at : "+position);
					myFamilyObj.ShowEditDialog(position);
				};
			}
			else if(structure.isFooter)
			{
				holder.footerTxt.Text = structure.FooterTitle;
				holder.listHeader.Visibility = ViewStates.Gone;
				holder.listFooter.Visibility = ViewStates.Visible;
				holder.listData.Visibility = ViewStates.Gone;

				holder.listFooter.Click += (object sender, EventArgs e) => {
					//System.Diagnostics.Debug.WriteLine("footer clicked at : "+position);
					string []arr = structure.FooterTitle.Split(new char[]{' '},5);
					Utilities.AddPersonType = arr[1];
					myFamilyObj.ViewModel.ShowAddFamilyViewModel();
				};
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