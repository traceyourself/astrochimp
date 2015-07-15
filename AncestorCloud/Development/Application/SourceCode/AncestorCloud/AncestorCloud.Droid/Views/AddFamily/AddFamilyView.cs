
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
using Android.Graphics;
using AncestorCloud.Shared.ViewModels;
using Java.Util;
using Java.Text;
using AncestorCloud.Shared;
using Android.Content.PM;

namespace AncestorCloud.Droid
{
	[Activity (Label = "AddFamilyView", ScreenOrientation = ScreenOrientation.Portrait)]			
	public class AddFamilyView : BaseActivity
	{
		LinearLayout malecheck,femalecheck,fathercheck,mothercheck,gFatherFCheck,gFatherMCheck,gMotherFCheck,gMotherMCheck;
		public TextView addBtn,dateText;
		bool maleSelected = false,femaleSelected=false;
		bool fatherSelected = false,motherSelected=false;
		ActionBar actionBar;
		EditText first_name,middle_name,last_name,birthLoc;
		Spinner yearSelector;
		RelativeLayout RefLay,greatRefLay;
		String refType = "";

		public new AddFamilyViewModel ViewModel
		{
			get { return base.ViewModel as AddFamilyViewModel; }
			set { base.ViewModel = value; }
		}

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.add_family_dialog);
			// Create your application here

			InitUI ();
			ApplyActions ();
			ConfigureActionBar ();

			if (Utilities.AddPersonType.Equals ("Sibling") || Utilities.AddPersonType.Equals ("Parent")) {
				RefLay.Visibility = ViewStates.Gone;	
				greatRefLay.Visibility = ViewStates.Gone;
			} else if (Utilities.AddPersonType.Equals ("Grandparent")) {
				//RefLay.Visibility = ViewStates.Gone;	
				greatRefLay.Visibility = ViewStates.Gone;
			} else {
				RefLay.Visibility = ViewStates.Gone;	
			}
		}

		protected void InitUI()
		{
			malecheck = FindViewById<LinearLayout> (Resource.Id.male_container);
			femalecheck = FindViewById<LinearLayout> (Resource.Id.female_container);
			addBtn = FindViewById<TextView> (Resource.Id.add_person_btn);
			dateText = FindViewById<TextView> (Resource.Id.birth_year_field);
			actionBar = FindViewById<ActionBar> (Resource.Id.actionBar);

			first_name = FindViewById<EditText> (Resource.Id.first_name_field);
			middle_name = FindViewById<EditText> (Resource.Id.mid_name_field);
			last_name = FindViewById<EditText> (Resource.Id.last_name_field);
			birthLoc = FindViewById<EditText> (Resource.Id.birth_loc_field);
			yearSelector = FindViewById<Spinner> (Resource.Id.yearSpin);

			RefLay = FindViewById<RelativeLayout> (Resource.Id.ref_lay);
			greatRefLay = FindViewById<RelativeLayout> (Resource.Id.great_ref_lay);

			fathercheck = FindViewById<LinearLayout> (Resource.Id.father_container);
			mothercheck = FindViewById<LinearLayout> (Resource.Id.mother_container);

			gFatherFCheck = FindViewById<LinearLayout> (Resource.Id.grand_father_f_container);
			gFatherMCheck = FindViewById<LinearLayout> (Resource.Id.grand_father_m_container);
			gMotherFCheck = FindViewById<LinearLayout> (Resource.Id.grand_mother_f_container);
			gMotherMCheck = FindViewById<LinearLayout> (Resource.Id.grand_mother_m_container);

		}

		private void ConfigureActionBar()
		{
			actionBar.SetCenterText (Resources.GetString(Resource.String.Add)+Utilities.AddPersonType);
			actionBar.SetLeftCornerImage (Resource.Drawable.back);
			var backButton = actionBar.FindViewById <RelativeLayout> (Resource.Id.action_bar_left_btn);

			backButton.Click += (sender, e) => {
				ViewModel.Close();
			};
		}

		protected void ApplyActions()
		{

			List<string> populateList = new List<string> ();

			Calendar cal = Calendar.GetInstance (Java.Util.Locale.Us);
			int start = 1850;
			int upto = cal.Get (Calendar.Year);

			for(int i=start;i<=upto;i++){
				populateList.Add (""+i);
			}

			var adapter = new ArrayAdapter (this,Android.Resource.Layout.SimpleListItem1,populateList);

			//adapter.SetDropDownViewResource (Android.Resource.Layout.SimpleSpinnerDropDownItem);
			yearSelector.Adapter = adapter;

			yearSelector.ItemSelected += (object sender, AdapterView.ItemSelectedEventArgs e) => {
				dateText.Text = yearSelector.SelectedItem.ToString();
				ViewModel.BirthDate = dateText.Text;
			};

			dateText.Click+= (object sender, EventArgs e) => {
				yearSelector.PerformClick();	
			};

			malecheck.Click += (object sender, EventArgs e) => {
				malecheck.SetBackgroundResource(Resource.Drawable.male_selected);	
				femalecheck.SetBackgroundColor(Color.Transparent);
				maleSelected = true;
				femaleSelected = false;
			};

			femalecheck.Click += (object sender, EventArgs e) => {
				malecheck.SetBackgroundColor(Color.Transparent);	
				femalecheck.SetBackgroundResource(Resource.Drawable.female_selected);
				maleSelected = false;
				femaleSelected = true;
			};

			fathercheck.Click += (object sender, EventArgs e) => {
				fathercheck.SetBackgroundResource(Resource.Drawable.male_selected);	
				mothercheck.SetBackgroundColor(Color.Transparent);
				fatherSelected = true;
				motherSelected = false;
				refType = AppConstant.Father_Reference;
			};

			mothercheck.Click += (object sender, EventArgs e) => {
				fathercheck.SetBackgroundColor(Color.Transparent);	
				mothercheck.SetBackgroundResource(Resource.Drawable.female_selected);
				fatherSelected = false;
				motherSelected = true;
				refType = AppConstant.Mother_Reference;
			};

			gFatherFCheck.Click += (object sender, EventArgs e) => {
				handleGrandCheckClick(0);
			};
			gFatherMCheck.Click += (object sender, EventArgs e) => {
				handleGrandCheckClick(1);
			};
			gMotherFCheck.Click += (object sender, EventArgs e) => {
				handleGrandCheckClick(2);
			};
			gMotherMCheck.Click += (object sender, EventArgs e) => {
				handleGrandCheckClick(3);
			};


			addBtn.Click += (object sender, EventArgs e) => {
				Utilities.RegisterCertificateForApiHit();

				if(maleSelected)
				{
					ViewModel.Gender = StringConstants.MALE;	
				}else if(femaleSelected)
				{
					ViewModel.Gender = StringConstants.FEMALE;
				}else{
					ViewModel.Gender = "";
				}

				ViewModel.AddType = Utilities.AddPersonType;
				ViewModel.ReferenceType = refType;
				ViewModel.AddPerson();
			};

			/*dateText.Click += (object sender, EventArgs e) => {
				ShowDatePicker();
			};*/
		}

		#region check for grand parents click event
		public void handleGrandCheckClick(int which)
		{
			gFatherFCheck.SetBackgroundColor(Color.Transparent);	
			gFatherMCheck.SetBackgroundColor(Color.Transparent);	
			gMotherFCheck.SetBackgroundColor(Color.Transparent);	
			gMotherMCheck.SetBackgroundColor(Color.Transparent);	

			switch(which)
			{
			case 0:
				gFatherFCheck.SetBackgroundResource(Resource.Drawable.male_selected);
				refType = AppConstant.Father_Reference;
				break;
			case 1:
				gFatherMCheck.SetBackgroundColor(Resources.GetColor(Resource.Color.action_bar_back_color));
				refType = AppConstant.Mother_Reference;
				break;
			case 2:
				gMotherFCheck.SetBackgroundColor(Resources.GetColor(Resource.Color.action_bar_back_color));
				refType = AppConstant.Father_Reference;
				break;
			case 3:
				gMotherMCheck.SetBackgroundResource(Resource.Drawable.female_selected);
				refType = AppConstant.Mother_Reference;
				break;
			}

		}
		#endregion

		public void ShowDatePicker()
		{
			Calendar cal = Calendar.GetInstance (Java.Util.Locale.Us);
			DatePickerDialog dpd = new DatePickerDialog (this,new AddFamilyDateListener(this),cal.Get(Calendar.Year), cal.Get(Calendar.Month),cal.Get(Calendar.DayOfMonth));
			dpd.Show ();
		}

	}

	public class AddFamilyDateListener : Java.Lang.Object,Android.App.DatePickerDialog.IOnDateSetListener
	{
		AddFamilyView obj;
		SimpleDateFormat dateFormatter;

		public AddFamilyDateListener(AddFamilyView obj)
		{
			this.obj = obj;
			dateFormatter = new SimpleDateFormat("MMM dd yyyy", Java.Util.Locale.Us);
		}

		public void OnDateSet(DatePicker view, int year, int monthOfYear, int dayOfMonth) {
			Calendar newDate = Calendar.GetInstance(Java.Util.Locale.Us);
			newDate.Set(year, monthOfYear, dayOfMonth);

			obj.dateText.Text = ""+dateFormatter.Format(newDate.Time);
		}

		public void Dispose(){}

	}

}

