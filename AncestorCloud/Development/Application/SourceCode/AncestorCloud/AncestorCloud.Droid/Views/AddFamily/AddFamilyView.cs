
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

namespace AncestorCloud.Droid
{
	[Activity (Label = "AddFamilyView")]			
	public class AddFamilyView : BaseActivity
	{
		LinearLayout malecheck,femalecheck;
		public TextView addBtn,dateText;
		bool maleSelected = false,femaleSelected=false;

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
		}

		protected void InitUI()
		{
			malecheck = FindViewById<LinearLayout> (Resource.Id.male_container);
			femalecheck = FindViewById<LinearLayout> (Resource.Id.female_container);
			addBtn = FindViewById<TextView> (Resource.Id.add_person_btn);
			dateText = FindViewById<TextView> (Resource.Id.birth_year_field);
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
				ViewModel.Close();
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

	}

	public class AddFamilyDateListener : Java.Lang.Object,Android.App.DatePickerDialog.IOnDateSetListener
	{
		AddFamilyView obj;
		SimpleDateFormat dateFormatter;

		public AddFamilyDateListener(AddFamilyView obj)
		{
			this.obj = obj;
			dateFormatter = new SimpleDateFormat("dd-MM-yyyy", Java.Util.Locale.Us);
		}

		public void OnDateSet(DatePicker view, int year, int monthOfYear, int dayOfMonth) {
			Calendar newDate = Calendar.GetInstance(Java.Util.Locale.Us);
			newDate.Set(year, monthOfYear, dayOfMonth);

			obj.dateText.Text = ""+dateFormatter.Format(newDate.Time);
		}

		public void Dispose(){}

	}

}

