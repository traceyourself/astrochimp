
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
using AncestorCloud.Shared.ViewModels;

namespace AncestorCloud.Droid
{
	[Activity (Label = "CelebritiesView")]			
	public class CelebritiesView : BaseActivity
	{
		ActionBar actionBar;

		public new AddFriendViewModel ViewModel
		{
			get { return base.ViewModel as AddFriendViewModel; }
			set { base.ViewModel = value; }
		}

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.celebrities_screen);

			initUI ();
			configureActionBar ();

			ApplyActions ();

		}

		private void initUI()
		{
			
		}

		private void ApplyActions(){

		}

		#region Action Bar Configuration
		private void configureActionBar(){

			actionBar = FindViewById <ActionBar>(Resource.Id.actionBar);
			actionBar.SetLeftCornerImage (Resource.Drawable.close_icon);

			actionBar.SetCenterText (Resources.GetString(Resource.String.select_someone_to_match));

			var crossButton = actionBar.FindViewById <RelativeLayout> (Resource.Id.action_bar_left_btn);

			crossButton.Click += (sender, e) => {
				ViewModel.Close();
			};
		}
		#endregion
	}
}

