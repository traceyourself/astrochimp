
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
using Cirrious.MvvmCross.Plugins.Messenger;

namespace AncestorCloud.Droid
{
	public class DroidAlerts:IAlert
	{
		AlertDialog ad;

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
			IMvxMessenger _mvxMessenger = Mvx.Resolve<IMvxMessenger>();

			AlertDialog.Builder builder = new AlertDialog.Builder (Utilities.CurrentActiveActivity);
			builder.SetTitle (title);
			builder.SetMessage (message);
			builder.SetPositiveButton ("Cancel",(object sender, DialogClickEventArgs e) => {
				ad.Dismiss();
			});
			builder.SetNegativeButton ("Ok",(object sender, DialogClickEventArgs e) => {

				if((int)alert == (int)AlertType.OKCancelPermit)
					_mvxMessenger.Publish(new CheckFbFriendMessage(this,true));

				if((int)alert == (int)AlertType.OKCancelSelect)
					_mvxMessenger.Publish(new SelectFbFriendMessage(this));

				if((int)alert == (int)AlertType.OKCancelSelectInvite)
					_mvxMessenger.Publish(new InviteContactMessage(this));

				if((int)alert == (int)AlertType.OKCancelSelectContact)
					_mvxMessenger.Publish(new SelectContactMessage(this));

				ad.Dismiss();
			});

			ad = builder.Create ();
			ad.Show ();

		}
	}
}

