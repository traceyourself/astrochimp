
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
			listView = FindViewById<ListView> (Resource.Id.add_family_list);
			helpIcon = FindViewById<ImageView> (Resource.Id.question_icon);
			percentText = FindViewById<TextView> (Resource.Id.percent_txt);
			//yearSelector = FindViewById<Spinner> (Resource.Id.year_selector_inlay);
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
		private void CreateListAdapter (bool reload)
		{
			if(reload){
				ViewModel.GetFbFamilyData ();
			}
			dataList = FilterDataList (ViewModel.FamilyList);

			MyFamilyListAdapter adapter = new MyFamilyListAdapter (this,dataList);
			listView.Adapter = adapter;
			listView.Invalidate ();	

		}
		#endregion


		public void SetPercentage()
		{
			percentText.Text = ViewModel._PercentageComplete+Resources.GetString(Resource.String.percent_matching_confidence);
		}

		#region list filteration
		public List<ListDataStructure> FilterDataList(List<People> mainList)
		{
			foreach(People p in mainList){
				Mvx.Trace (p.FirstName +":"+ p.Relation );
			}

			List<ListDataStructure> resultList = new List<ListDataStructure> ();

			List<ListDataStructure> siblingList = new List<ListDataStructure> ();
			List<ListDataStructure> parentList = new List<ListDataStructure> ();
			List<ListDataStructure> grandParentList = new List<ListDataStructure> ();
			List<ListDataStructure> greatGrandParentList = new List<ListDataStructure> ();

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

					else if(relation.Equals (StringConstants.GrandFather_comparison) || relation.Equals (StringConstants.GrandMother_comparison) || relation.Equals (StringConstants.GrandParent_comparison))
					{
						listStruct = new ListDataStructure(false,false,true,"","",item);
						grandParentList.Add (listStruct);
					}

					else if(relation.Equals (StringConstants.GreatGrandFather_comparison) || relation.Equals (StringConstants.GreatGrandMother_comparison) || relation.Equals (AppConstant.GreatGrandParent_comparison))
					{
						listStruct = new ListDataStructure(false,false,true,"","",item);
						greatGrandParentList.Add (listStruct);
					}
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
			People peopleData = dataList[position].PersonData;
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
				if (structure.PersonData.FirstName != null) {
					holder.nameTxt.Text = structure.PersonData.FirstName+" "+structure.PersonData.MiddleName+" "+structure.PersonData.LastName;
				} else {
					holder.nameTxt.Text = structure.PersonData.Name;
				}

				holder.yearTxt.Text = structure.PersonData.DateOfBirth;
				holder.listHeader.Visibility = ViewStates.Gone;
				holder.listFooter.Visibility = ViewStates.Gone;
				holder.listData.Visibility = ViewStates.Visible;
				holder.editBtn.Tag = ""+position;

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
					string []arr = structure.FooterTitle.Split(new char[]{' '},5);

					string type = arr[1];
					try{
						if(arr.Length > 1){
							type += " "+arr[2];
						}
					}catch(Exception e1){
						Mvx.Trace(e1.StackTrace);
					}
					Utilities.AddPersonType = type;

					myFamilyObj.ViewModel.CheckIfCanAddPerson(type);
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