
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

namespace AncestorCloud.Droid
{
	public class HelpDialog
	{
		Dialog helpdialog;
		Activity actObj;

		public HelpDialog(Activity actObj)
		{
			this.actObj = actObj;
		}

		#region Edit Dialog
		public void ShowHelpDialog()
		{

			helpdialog = new Dialog (actObj,Android.Resource.Style.ThemeTranslucentNoTitleBar);
			helpdialog.SetContentView (Resource.Layout.help_dialog);

			RelativeLayout crossbtn = helpdialog.FindViewById<RelativeLayout> (Resource.Id.cross_edit_btn);

			crossbtn.Click += (object sender, EventArgs e) => {
				helpdialog.Dismiss();
			};

			helpdialog.Show ();
		}
		#endregion

		
	}
}

