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
using Cirrious.MvvmCross.Plugins.Messenger;
using AncestorCloud.Core;
using Android.Content.PM;
using Android.Graphics.Drawables;

namespace AncestorCloud.Droid
{
	[Activity (Label = "MyFamilyView", ScreenOrientation = ScreenOrientation.Portrait)]			
	public class MyFamilyView : BaseActivity
	{

		ListView listView;
		ActionBar actionBar;
		TextView FbBtn,loginBtn,percentText;
		public TextView birthDateDialogTxt;
		List<ListDataStructure> dataList;
		public Dialog editDialog;
		ImageView helpIcon;
		IMvxMessenger _messenger;
		private MvxSubscriptionToken ReloadViewToken,percentToken;
		Spinner yearSelector;
		IDatabaseService _databaseService;
		IAlert Alert;

		List<ListDataStructure> siblingParentList;
		List<ListDataStructure> grandParentList;
		List<ListDataStructure> greatGrandParentList;

		#region tab var region
		TextView parentsTxt,gParentsTxt,ggParentsTxt;
		View parentsBottomDiv,gParentsBottomDiv,ggParentsBottomDiv;
		LinearLayout parentsBtn,gParentsBtn,ggParentsBtn;
		ListView parentSiblingList,gParentList,ggParentList;
		public int currentTab = 0;
		#endregion


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
			_messenger = Mvx.Resolve<IMvxMessenger>();
			_databaseService = Mvx.Resolve<IDatabaseService>();
			Alert = Mvx.Resolve<IAlert>();

			InitViews ();
			ConfigureActionBar ();
			ApplyActions ();
		}
		#endregion

		protected override void OnResume ()
		{
			base.OnResume ();
			Utilities.CurrentActiveActivity = this;
			//For checking after adding a member
			//CreateListAdapter ();

			//For checking after editing a member
			ReloadViewToken = _messenger.SubscribeOnMainThread<MyFamilyReloadMessage>(Message => this.CreateListAdapter (true));
			percentToken = _messenger.SubscribeOnMainThread<PercentageMessage>(Message => this.SetPercentage());
			//ViewModel.FetchPercentageComplete ();
			ViewModel.GetFamilyMembersFromServer ();
		}

		protected override void OnPause ()
		{
			base.OnPause ();
			_messenger.Unsubscribe<MyFamilyReloadMessage> (ReloadViewToken);
			_messenger.Unsubscribe<PercentageMessage> (percentToken);
		}

		#region customized methods
		private void InitViews()
		{
			actionBar = FindViewById<ActionBar> (Resource.Id.actionBar);
			//listView = FindViewById<ListView> (Resource.Id.add_family_list);
			helpIcon = FindViewById<ImageView> (Resource.Id.question_icon);
			percentText = FindViewById<TextView> (Resource.Id.percent_txt);
			//yearSelector = FindViewById<Spinner> (Resource.Id.year_selector_inlay);

			//Tabs and Layouts
			parentsTxt = FindViewById<TextView> (Resource.Id.parents_txt);
			gParentsTxt = FindViewById<TextView> (Resource.Id.g_parents_txt);
			ggParentsTxt = FindViewById<TextView> (Resource.Id.g_g_parents_txt);

			parentsBottomDiv = FindViewById<View> (Resource.Id.parent_bottom_div);
			gParentsBottomDiv = FindViewById<View> (Resource.Id.g_parent_bottom_div);
			ggParentsBottomDiv = FindViewById<View> (Resource.Id.g_g_parent_bottom_div);

			parentsBtn = FindViewById<LinearLayout> (Resource.Id.parents_btn);
			gParentsBtn = FindViewById<LinearLayout> (Resource.Id.g_parents_btn);
			ggParentsBtn = FindViewById<LinearLayout> (Resource.Id.gg_parents_btn);

			parentSiblingList = FindViewById<ListView> (Resource.Id.parent_sibling_list);
			gParentList = FindViewById<ListView> (Resource.Id.grand_parent_list);
			ggParentList = FindViewById<ListView> (Resource.Id.great_grand_parent_list);
			//=============
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

			parentsBtn.Click += (object sender, EventArgs e) => {
				if(currentTab != 0){
					HandleTabClicks(0);
					currentTab = 0;
				}
			};

			gParentsBtn.Click += (object sender, EventArgs e) => {
				if(currentTab != 1){
					HandleTabClicks(1);
					currentTab = 1;
				}
			};

			ggParentsBtn.Click += (object sender, EventArgs e) => {
				if(currentTab != 2){
					HandleTabClicks(2);
					currentTab = 2;
				}
			};
		}

		public void HandleTabClicks(int which)
		{

			parentsTxt.SetTextColor(Color.White);
			gParentsTxt.SetTextColor(Color.White);
			ggParentsTxt.SetTextColor(Color.White);

			parentsBottomDiv.Visibility = ViewStates.Gone;
			gParentsBottomDiv.Visibility = ViewStates.Gone;
			ggParentsBottomDiv.Visibility = ViewStates.Gone;

			parentSiblingList.Visibility = ViewStates.Gone;
			gParentList.Visibility = ViewStates.Gone;
			ggParentList.Visibility = ViewStates.Gone;

			if (which == 0) {
				parentsTxt.SetTextColor (Resources.GetColor (Resource.Color.tab_text_div_color));
				parentsBottomDiv.Visibility = ViewStates.Visible;
				parentSiblingList.Visibility = ViewStates.Visible;
			}
			else if(which == 1)
			{
				gParentsTxt.SetTextColor (Resources.GetColor (Resource.Color.tab_text_div_color));
				gParentsBottomDiv.Visibility = ViewStates.Visible;
				gParentList.Visibility = ViewStates.Visible;
			}
			else if(which == 2)
			{
				ggParentsTxt.SetTextColor (Resources.GetColor (Resource.Color.tab_text_div_color));
				ggParentsBottomDiv.Visibility = ViewStates.Visible;
				ggParentList.Visibility = ViewStates.Visible;
			}
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
		private void CreateListAdapter (bool reload)
		{
			if(reload){
				ViewModel.GetFbFamilyData ();
			}

			siblingParentList = FilterSiblingParentList (ViewModel.FamilyList);

			MyFamilyListAdapter siblingParentAdapter = new MyFamilyListAdapter (this,siblingParentList);
			parentSiblingList.Adapter = siblingParentAdapter;
			parentSiblingList.Invalidate ();	


			grandParentList = FilterGrandParentList (ViewModel.FamilyList);

			MyFamilyListAdapter grandParentAdapter = new MyFamilyListAdapter (this,grandParentList);
			gParentList.Adapter = grandParentAdapter;
			gParentList.Invalidate ();	


			greatGrandParentList = FilterGreatGrandParentList (ViewModel.FamilyList);

			MyFamilyListAdapter greatGrandParentAdapter = new MyFamilyListAdapter (this,greatGrandParentList);
			ggParentList.Adapter = greatGrandParentAdapter;
			ggParentList.Invalidate ();	
		}
		#endregion


		public void SetPercentage()
		{
			percentText.Text = ViewModel._PercentageComplete+Resources.GetString(Resource.String.percent_matching_confidence);
		}

		#region list filteration of sibling Parent list
		public List<ListDataStructure> FilterSiblingParentList(List<People> mainList)
		{
			List<ListDataStructure> siblingList = new List<ListDataStructure> ();
			List<ListDataStructure> parentList = new List<ListDataStructure> ();

			List<ListDataStructure> siblingParentList = new List<ListDataStructure> ();

			ListDataStructure listStruct;


			if(mainList != null){
				for(int i=0;i<mainList.Count;i++){
					People item = mainList[i];
					string relation = item.Relation;

					if (relation.Equals (StringConstants.Brother_comparison) || relation.Equals (StringConstants.Sister_comparison) || relation.Equals (StringConstants.Sibling_comparison)) 
					{
						listStruct = new ListDataStructure(false,false,true,"","",item);
						siblingList.Add (listStruct);
					}

					else if(relation.Equals (StringConstants.Father_comparison) || relation.Equals (StringConstants.Mother_comparison) || relation.Equals (StringConstants.Parent_comparison))
					{
						listStruct = new ListDataStructure(false,false,true,"","",item);
						parentList.Add (listStruct);
					}
				}
			}

			//Parents and Siblings=======
			listStruct = new ListDataStructure(true,false,false,Resources.GetString(Resource.String.Parent_header),"",null);
			siblingParentList.Add (listStruct);
			if(parentList.Count > 0){
				for(int i=0;i<parentList.Count;i++){
					siblingParentList.Add (parentList[i]);
				}
			}
			listStruct = new ListDataStructure(false,true,false,"",Resources.GetString(Resource.String.Parent_Footer),null);
			siblingParentList.Add (listStruct);

			listStruct = new ListDataStructure(true,false,false,Resources.GetString(Resource.String.Sibling_header),"",null);
			siblingParentList.Add (listStruct);

			if(siblingList.Count > 0){
				for(int i=0;i<siblingList.Count;i++){
					siblingParentList.Add (siblingList[i]);
				}
			}

			listStruct = new ListDataStructure(false,true,false,"",Resources.GetString(Resource.String.Sibling_Footer),null);
			siblingParentList.Add (listStruct);
			//==========================

			return siblingParentList;
		}
		#endregion

		#region list filteration of grand Parent list
		public List<ListDataStructure> FilterGrandParentList(List<People> mainList)
		{
			List<ListDataStructure> grandParentFatherList = new List<ListDataStructure> ();
			List<ListDataStructure> grandParentMotherList = new List<ListDataStructure> ();

			List<ListDataStructure> grandParentList = new List<ListDataStructure> ();

			ListDataStructure listStruct;

			if(mainList != null){
				for(int i=0;i<mainList.Count;i++){
					People item = mainList[i];
					string relation = item.Relation;

					if (relation.Equals (StringConstants.GRANDFATHER_COMPARISON) || relation.Equals  (StringConstants.GRANDMOTHER_COMPARISON) || relation.Equals ("Grandparent")|| relation.Equals (AppConstant.GrandParent_comparison))
					{
						if (item.RelationReference.Equals (AppConstant.Father_Reference)) 
						{
							listStruct = new ListDataStructure(false,false,true,"","",item);
							grandParentFatherList.Add (listStruct);

						}else if(item.RelationReference.Equals (AppConstant.Mother_Reference))
						{
							listStruct = new ListDataStructure(false,false,true,"","",item);
							grandParentMotherList.Add (listStruct);
						}
					}
				}
			}

			listStruct = new ListDataStructure(true,false,false,Resources.GetString(Resource.String.ShowGrandMotherSectionHeader),"",null);
			grandParentList.Add (listStruct);
			if(grandParentMotherList.Count > 0){
				for(int i=0;i<grandParentMotherList.Count;i++){
					grandParentList.Add (grandParentMotherList[i]);
				}
			}
			listStruct = new ListDataStructure(false,true,false,"",Resources.GetString(Resource.String.ShowGrandMOtherSectionFooter),null);
			grandParentList.Add (listStruct);

			listStruct = new ListDataStructure(true,false,false,Resources.GetString(Resource.String.ShowGrandFatherSectionHeader),"",null);
			grandParentList.Add (listStruct);
			if(grandParentFatherList.Count > 0){
				for(int i=0;i<grandParentFatherList.Count;i++){
					grandParentList.Add (grandParentFatherList[i]);
				}
			}
			listStruct = new ListDataStructure(false,true,false,"",Resources.GetString(Resource.String.ShowGrandFatherSectionFooter),null);
			grandParentList.Add (listStruct);

			return grandParentList;
		}
		#endregion


		#region list filteration of great grand Parent list
		public List<ListDataStructure> FilterGreatGrandParentList(List<People> mainList)
		{

			List<ListDataStructure> FFPList = new List<ListDataStructure> ();
			List<ListDataStructure> FMPList = new List<ListDataStructure> ();
			List<ListDataStructure> MFPList = new List<ListDataStructure> ();
			List<ListDataStructure> MMPList = new List<ListDataStructure> ();

			List<ListDataStructure> greatGrandParentList = new List<ListDataStructure> ();

			ListDataStructure listStruct;

			if(mainList != null){
				for(int i=0;i<mainList.Count;i++){
					People item = mainList[i];
					string relation = item.Relation;

					/*listStruct = new ListDataStructure(false,false,true,"","",item);
					greatGrandParentList.Add (listStruct);*/

					if (relation.Equals (StringConstants.GREATGRANDFATHER_COMPARISON) || relation.Equals (StringConstants.GREATGRANDMOTHER_COMPARISON) || relation.Equals ("Great Grandparent") || relation.Equals (AppConstant.GreatGrandParent_comparison))
					{
						if (item.RelationReference.Equals (AppConstant.Grand_Father_Father_Reference)) 
						{
							listStruct = new ListDataStructure(false,false,true,"","",item);
							FFPList.Add (listStruct);
						}else if(item.RelationReference.Equals (AppConstant.Grand_Father_Mother_Reference))
						{
							listStruct = new ListDataStructure(false,false,true,"","",item);
							FMPList.Add (listStruct);
						}
						else if (item.RelationReference.Equals (AppConstant.Grand_Mother_Father_Reference)) 
						{
							listStruct = new ListDataStructure(false,false,true,"","",item);
							MFPList.Add (listStruct);
				
						}else if(item.RelationReference.Equals (AppConstant.Grand_Mother_Mother_Reference))
						{
							listStruct = new ListDataStructure(false,false,true,"","",item);
							MMPList.Add (listStruct);
						}
					}
				}
			}

		
			listStruct = new ListDataStructure(true,false,false,Resources.GetString(Resource.String.ShowGreatGrandFatherFatherSectionHeader),"",null);
			greatGrandParentList.Add (listStruct);

			if(FFPList.Count > 0){
				for(int i=0;i<FFPList.Count;i++){
					greatGrandParentList.Add (FFPList[i]);
				}
			}

			listStruct = new ListDataStructure(false,true,false,"",Resources.GetString(Resource.String.ShowGreatGrandFatherFatherSectionFooter),null);
			greatGrandParentList.Add (listStruct);

			//==============

			listStruct = new ListDataStructure(true,false,false,Resources.GetString(Resource.String.ShowGreatGrandFatherMotherSectionHeader),"",null);
			greatGrandParentList.Add (listStruct);

			if(FMPList.Count > 0){
				for(int i=0;i<FMPList.Count;i++){
					greatGrandParentList.Add (FMPList[i]);
				}
			}

			listStruct = new ListDataStructure(false,true,false,"",Resources.GetString(Resource.String.ShowGreatGrandFatherMotherSectionFooter),null);
			greatGrandParentList.Add (listStruct);

			//===============

			listStruct = new ListDataStructure(true,false,false,Resources.GetString(Resource.String.ShowGreatGrandMotherFatherSectionHeader),"",null);
			greatGrandParentList.Add (listStruct);

			if(MFPList.Count > 0){
				for(int i=0;i<MFPList.Count;i++){
					greatGrandParentList.Add (MFPList[i]);
				}
			}

			listStruct = new ListDataStructure(false,true,false,"",Resources.GetString(Resource.String.ShowGreatGrandMotherFatherSectionFooter),null);
			greatGrandParentList.Add (listStruct);

			//=================

			listStruct = new ListDataStructure(true,false,false,Resources.GetString(Resource.String.ShowGreatGrandMotherMotherSectionHeader),"",null);
			greatGrandParentList.Add (listStruct);

			if(MMPList.Count > 0){
				for(int i=0;i<MMPList.Count;i++){
					greatGrandParentList.Add (MMPList[i]);
				}
			}

			listStruct = new ListDataStructure(false,true,false,"",Resources.GetString(Resource.String.ShowGreatGrandMotherMotherSectionFooter),null);
			greatGrandParentList.Add (listStruct);

			//=================

			return greatGrandParentList;
		}
		#endregion



		#region Edit Dialog
		public void ShowEditDialog(int position)
		{
			People peopleData = null;

			if(currentTab == 0){
				peopleData = siblingParentList[position].PersonData;
			}else if(currentTab == 1){
				peopleData = grandParentList[position].PersonData;
			}else if(currentTab == 2){
				peopleData = greatGrandParentList[position].PersonData;
			}

			String gender = "";
			editDialog = new Dialog (this,Resource.Style.TransparentDialog);

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
			yearSelector = editDialog.FindViewById<Spinner> (Resource.Id.year_selector);

			male.Click += (object sender, EventArgs e) => {
				male.SetBackgroundResource(Resource.Drawable.male_selected);	
				female.SetBackgroundColor(Color.Transparent);
				gender = Resources.GetString(Resource.String.Male);
			};

			female.Click += (object sender, EventArgs e) => {
				male.SetBackgroundColor(Color.Transparent);	
				female.SetBackgroundResource(Resource.Drawable.female_selected);
				gender = Resources.GetString(Resource.String.Female);
			};

			birthDateDialogTxt.Click += (object sender, EventArgs e) => {
				//ShowDatePicker();
				yearSelector.PerformClick();
			};

			crossbtn.Click += (object sender, EventArgs e) => {
				try{
					editDialog.Dismiss();
					//editDialog = null;
				}catch(Exception ex){
					Mvx.Trace(ex.StackTrace);
				}
			};

			saveBtn.Click += (object sender, EventArgs e) => {
			
				Boolean isValid = true;

				if(String.IsNullOrEmpty(first_name.Text))
				{
					isValid = false;
					Toast.MakeText(this,Resources.GetString(Resource.String.enter_f_name),ToastLength.Short).Show();
				}else if(String.IsNullOrEmpty(gender)){
					isValid = false;
					Toast.MakeText(this,Resources.GetString(Resource.String.select_gender),ToastLength.Short).Show();
				}

				if(isValid)
				{
					peopleData.FirstName = first_name.Text.ToString();
					peopleData.LastName = last_name.Text.ToString();
					peopleData.MiddleName = mid_name.Text.ToString();
					peopleData.DateOfBirth = year_field.Text.ToString();
					peopleData.BirthLocation = birth_loc.Text.ToString();
					peopleData.Gender = gender;

					ViewModel.FamilyMember = peopleData;
					ViewModel.EditPerson();

					editDialog.Dismiss();
				}
			};


			List<string> populateList = new List<string> ();

			Calendar cal = Calendar.GetInstance (Java.Util.Locale.Us);
			int start = 1849;
			int upto = cal.Get (Calendar.Year);

			for(int i=start;i<=upto;i++){
				if(i == start){
					populateList.Add ("");
				}else{
					populateList.Add (""+i);
				}
			}

			//var adapter = new ArrayAdapter (this,Android.Resource.Layout.SimpleListItem1,populateList);
			var adapter = new ArrayAdapter (this,Resource.Layout.spinner_item,populateList);
			//adapter.SetDropDownViewResource (Android.Resource.Layout.SimpleSpinnerDropDownItem);
			yearSelector.Adapter = adapter;

			yearSelector.ItemSelected += (object sender, AdapterView.ItemSelectedEventArgs e) => {
				year_field.Text = yearSelector.SelectedItem.ToString();
			};

			//Set data Accordingly===
			try{
				try{

					if(peopleData.FirstName == null){
						first_name.Text = peopleData.Name;
					}else{
						first_name.Text = peopleData.FirstName;
					}

					mid_name.Text = peopleData.MiddleName;
					last_name.Text = peopleData.LastName;
					birth_loc.Text = peopleData.BirthLocation;

					nameTitle.Text = first_name.Text+" "+peopleData.MiddleName+" "+peopleData.LastName+"("+peopleData.Relation+")";

					if (peopleData.Gender.Equals (StringConstants.MALE)) {
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

			editDialog.Window.SetSoftInputMode(SoftInput.StateAlwaysHidden);

			editDialog.DismissEvent += (object sender, EventArgs e) => {
				try{
					//editDialog.Dismiss();
					editDialog = null;
				}catch(Exception ex){
					Mvx.Trace(ex.StackTrace);
				}
			};

			editDialog.Show ();

		}
		#endregion


		#region check validity for family Adiition
		public void CheckIfCanAddPerson()
		{
			string typeToAdd = Utilities.AddPersonType;
			LoginModel model = _databaseService.GetLoginDetails ();
			//Sibling_comparison
			if(typeToAdd.Contains("Sibling") || typeToAdd.Equals("Parent")){
				ViewModel.ShowAddFamilyViewModel();
			}else{
				if(typeToAdd.Equals("Grandparent")){

					List<People> listP = _databaseService.RelativeMatching (StringConstants.Parent_comparison,model.UserEmail);
					if (listP != null && listP.Count > 0) {
						ViewModel.ShowAddFamilyViewModel ();
					} else {
						listP = _databaseService.RelativeMatching (StringConstants.Father_comparison,model.UserEmail);
						if (listP != null && listP.Count > 0) {
							ViewModel.ShowAddFamilyViewModel ();
						}else{
							Alert.ShowAlert ("Please add parents first to add grand parents","");
						}
					}

				}else if(typeToAdd.Equals("Great Grandparent")){
					List<People> listP = _databaseService.RelativeMatching (StringConstants.GrandParent_comparison,model.UserEmail);
					if (listP != null && listP.Count > 0) {
						ViewModel.ShowAddFamilyViewModel ();
					} else {
						Alert.ShowAlert ("Please add grand parents first to add great grand parents","");
					}
				}
			}


			/*****
			ViewModel.ShowAddFamilyViewModel();
			******/
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
				convertView.SetTag (Resource.Id.parent_sibling_list,holder);

			} else {
				holder = (ViewHolder)convertView.GetTag (Resource.Id.parent_sibling_list);
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
				String name = "";

				if (structure.PersonData.FirstName != null && structure.PersonData.FirstName.Length > 0) {
					name = structure.PersonData.FirstName+" "+structure.PersonData.MiddleName+" "+structure.PersonData.LastName;
				} else {
					name = structure.PersonData.Name;
				}

				holder.yearTxt.Text = structure.PersonData.DateOfBirth;
				holder.listHeader.Visibility = ViewStates.Gone;
				holder.listFooter.Visibility = ViewStates.Gone;
				holder.listData.Visibility = ViewStates.Visible;
				holder.editBtn.Tag = ""+position;

				String relation = structure.PersonData.Relation;
				if (relation.Equals (StringConstants.Brother_comparison) || relation.Equals (StringConstants.Sister_comparison) || relation.Equals (StringConstants.Sibling_comparison)) {
					holder.nameTxt.Text = name;
				} else {

					String gen = "" + structure.PersonData.Gender;

					if (!String.IsNullOrEmpty (gen)) {
						String attach = "";
						if (gen.Equals ("Male")) {
							attach = "(Father)";
						} else {
							attach = "(Mother)";
						}
						holder.nameTxt.Text = name + " " + attach;
					} else {
						holder.nameTxt.Text = name;
					}
				}

				holder.editBtn.Click += (object sender, EventArgs e) => {
					//System.Diagnostics.Debug.WriteLine("edit clicked at : "+position);
					try{
						RelativeLayout btn = (RelativeLayout)sender;
						Mvx.Trace(""+btn.Tag);

						if(myFamilyObj.editDialog != null){
							if(!myFamilyObj.editDialog.IsShowing){
								int pos = int.Parse(""+btn.Tag);
								myFamilyObj.ShowEditDialog(pos);
							}
						}else{
							int pos = int.Parse(""+btn.Tag);
							myFamilyObj.ShowEditDialog(pos);
						}
					}catch(Exception e1)
					{
						Mvx.Trace(e1.StackTrace);
					}

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
					/*string []arr = structure.FooterTitle.Split(new char[]{' '},5);

					string type = arr[1];
					try{
						if(arr.Length > 1){
							type += " "+arr[2];
						}
					}catch(Exception e1){
						Mvx.Trace(e1.StackTrace);
					}
					Utilities.AddPersonType = type;*/

					//myFamilyObj.ViewModel.CheckIfCanAddPerson(type);

					String footer = structure.FooterTitle;

					if(myFamilyObj.currentTab == 0)
					{

						if(footer.Equals(myFamilyObj.Resources.GetString(Resource.String.Parent_Footer)))
						{
							Utilities.AddPersonType = "Parent";
							myFamilyObj.ViewModel.CheckIfCanAddPerson("Parent","");
						}
						else if(footer.Equals(myFamilyObj.Resources.GetString(Resource.String.Sibling_Footer)))
						{
							Utilities.AddPersonType = "Sibling";
							myFamilyObj.ViewModel.CheckIfCanAddPerson("Sibling","");
						}
					}
					else if(myFamilyObj.currentTab == 1)
					{
						Utilities.AddPersonType = "Grandparent";

						if(footer.Equals(myFamilyObj.Resources.GetString(Resource.String.ShowGrandMOtherSectionFooter)))
						{
							myFamilyObj.ViewModel.CheckIfCanAddPerson("Grandparent",AppConstant.MOTHERS_PARENT);
						}
						else if(footer.Equals(myFamilyObj.Resources.GetString(Resource.String.ShowGrandFatherSectionFooter)))
						{
							myFamilyObj.ViewModel.CheckIfCanAddPerson("Grandparent",AppConstant.FATHERS_PARENT);
						}
					}
					else if(myFamilyObj.currentTab == 2)
					{
						Utilities.AddPersonType = "Great Grandparent";

						if(footer.Equals(myFamilyObj.Resources.GetString(Resource.String.ShowGreatGrandFatherFatherSectionFooter)))
						{
							myFamilyObj.ViewModel.CheckIfCanAddPerson("Great Grandparent",AppConstant.FATHERS_FATHERS_PARENT);
						}
						else if(footer.Equals(myFamilyObj.Resources.GetString(Resource.String.ShowGreatGrandFatherMotherSectionFooter)))
						{
							myFamilyObj.ViewModel.CheckIfCanAddPerson("Great Grandparent",AppConstant.FATHERS_MOTHERS_PARENT);
						}else if(footer.Equals(myFamilyObj.Resources.GetString(Resource.String.ShowGreatGrandMotherFatherSectionFooter)))
						{
							myFamilyObj.ViewModel.CheckIfCanAddPerson("Great Grandparent",AppConstant.MOTHERS_FATHERS_PARENT);
						}
						else if(footer.Equals(myFamilyObj.Resources.GetString(Resource.String.ShowGreatGrandMotherMotherSectionFooter)))
						{
							myFamilyObj.ViewModel.CheckIfCanAddPerson("Great Grandparent",AppConstant.MOTHERS_MOTHERS_PARENT);
						}
					}
				};
			}

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