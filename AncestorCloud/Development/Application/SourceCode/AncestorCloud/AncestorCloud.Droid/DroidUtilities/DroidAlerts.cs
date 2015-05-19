
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
using Cirrious.CrossCore;

namespace AncestorCloud.Droid
{
	public class DroidAlerts:IAlert
	{
		public void ShowAlert (string message, string title)
		{
			//Mvx.Trace("In globals");

			var myHandler = new Handler ();
				
			myHandler.Post(() => {
				Toast.MakeText (Application.Context,""+message, ToastLength.Long).Show();

			});
		}


		public void ShowAlertWithOk (string message, string title, AlertType alert)
		{
			
		}
	}
}

