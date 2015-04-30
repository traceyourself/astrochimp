
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

namespace AncestorCloud.Droid
{
	[Activity (Label = "AddFamilyView")]			
	public class AddFamilyView : BaseActivity
	{
		LinearLayout malecheck,femalecheck;
		public TextView addBtn,dateText;
		bool maleSelected = false,femaleSelected=false;
		ActionBar actionBar;
		EditText first_name,middle_name,last_name,birthLoc;


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
		}

		private void ConfigureActionBar()
		{
			actionBar.SetCenterText ("Add "+Utilities.AddPersonType);
			actionBar.SetLeftCornerImage (Resource.Drawable.back);
			var backButton = actionBar.FindViewById <RelativeLayout> (Resource.Id.action_bar_left_btn);

			backButton.Click += (sender, e) => {
				ViewModel.Close();
			};
		}

		protected void ApplyActions()
		{
			malecheck.Click += (object sender, EventArgs e) => {
				malecheck.SetBackgroundResource(Resource.Drawable.male_selected);	
				femalecheck.SetBackgroundColor(Color.ParseColor("#00000000"));
				maleSelected = true;
				femaleSelected = false;
			};

			femalecheck.Click += (object sender, EventArgs e) => {
				malecheck.SetBackgroundColor(Color.ParseColor("#00000000"));	
				femalecheck.SetBackgroundResource(Resource.Drawable.female_selected);
				maleSelected = false;
				femaleSelected = true;
			};

			addBtn.Click += (object sender, EventArgs e) => {
				//ViewModel.Close();
				if(Validate())
				{
					AddFamilyModel model = createModel();
					ViewModel.AddPerson(model);
				}
			};

			dateText.Click += (object sender, EventArgs e) => {
				ShowDatePicker();
			};

		}


		public void ShowDatePicker()
		{
			Calendar cal = Calendar.GetInstance (Java.Util.Locale.Us);
			DatePickerDialog dpd = new DatePickerDialog (this,new AddFamilyDateListener(this),cal.Get(Calendar.Year), cal.Get(Calendar.Month),cal.Get(Calendar.DayOfMonth));
			dpd.Show ();
		}

		public bool Validate()
		{
			return true;
		}

		public People createModel()
		{

//			People model = new People ();
//
//			model.FirstName = first_name.Text.ToString ();
//			model.MiddleName = middle_name.Text.ToString ();
//			model.LastName = last_name.Text.ToString ();
//			model.BirthLocation = birthLoc.Text.ToString ();
//			model.DateOfBirth = dateText.Text;
//
//			if (maleSelected) {
//				model.Gender = "Male";	
//			} else {
//				model.Gender = "Female";
//			}
//


			return model;
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

