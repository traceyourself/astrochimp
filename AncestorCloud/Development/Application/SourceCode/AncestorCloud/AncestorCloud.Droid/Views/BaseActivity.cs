using System;
using Cirrious.MvvmCross.Droid.Views;
using AncestorCloud.Shared;
using Android.App;
using Android.OS;
using Cirrious.CrossCore;

namespace AncestorCloud.Droid
{
	public class BaseActivity : MvxActivity
	{

		protected override void OnCreate (Bundle bundle)
		{
			try{
				base.OnCreate (bundle);
				Utilities.CurrentActiveActivity = this;
			}catch(Exception e){
				System.Diagnostics.Debug.WriteLine (e.InnerException);
			}
		}

	}
}

