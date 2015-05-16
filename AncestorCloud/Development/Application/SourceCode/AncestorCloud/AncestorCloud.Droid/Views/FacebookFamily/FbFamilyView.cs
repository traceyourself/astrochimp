
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
using Cirrious.MvvmCross.Binding.Droid.Views;
using AncestorCloud.Shared.ViewModels;
using AncestorCloud.Shared;

namespace AncestorCloud.Droid
{
	[Activity (Label = "FbFamilyView")]
	public class FbFamilyView : BaseActivity
	{
		MvxListView listView;
		ActionBar actionBar;
		List<FBListDataStructure> dataList;
		FlyOutContainer menu;
		LinearLayout contentLayout;
		RelativeLayout menuLayout;
		TextView nextBtn;
		ImageView helpicon;
		TextView userNameMenu;
		ImageView userImageMenu;

		public new FbFamilyViewModel ViewModel
		{
			get { return base.ViewModel as FbFamilyViewModel; }
			set { base.ViewModel = value; }
		}


		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.my_fb_familiy_fly_out);

			InitViews ();
			ConfigureActionBar ();
			ApplyActions ();
		}


		protected override void OnResume ()
		{
			base.OnResume ();
			ApplyData ();
		}

		#region customized methods
		private void InitViews()
		{
			menu = FindViewById<FlyOutContainer> (Resource.Id.flyOutContainerLay);
			menuLayout = FindViewById<RelativeLayout> (Resource.Id.FlyOutMenu);
			contentLayout = FindViewById<LinearLayout> (Resource.Id.FlyOutContent);
			actionBar = contentLayout.FindViewById<ActionBar> (Resource.Id.actionBar);
			listView = contentLayout.FindViewById<MvxListView> (Resource.Id.fb_family_list);
			nextBtn = contentLayout.FindViewById<TextView> (Resource.Id.next_btn);
			helpicon = contentLayout.FindViewById<ImageView> (Resource.Id.question_icon);

			userNameMenu = menuLayout.FindViewById<TextView> (Resource.Id.user_name_menu);
			userImageMenu = menuLayout.FindViewById<ImageView> (Resource.Id.user_img_menu);

		}

		private void ConfigureActionBar()
		{
			actionBar.SetCenterImageText (Resource.Drawable.myfamily_title,Resources.GetString(Resource.String.my_family_menu));
			actionBar.SetLeftCornerMenuImage (Resource.Drawable.action_menu);

			var menuButton = actionBar.FindViewById <RelativeLayout> (Resource.Id.action_bar_left_btn);

			menuButton.Click += (sender, e) => {
				menu.AnimatedOpened = !menu.AnimatedOpened;
			};
		}

		private void ApplyActions()
		{
			menuLayout.FindViewById<LinearLayout> (Resource.Id.my_family_menu_btn).Click += (object sender, EventArgs e) => {
				if(menu.AnimatedOpened){
					menu.AnimatedOpened = !menu.AnimatedOpened;
				}
			};

			menuLayout.FindViewById<LinearLayout> (Resource.Id.matcher_menu_btn).Click += (object sender, EventArgs e) => {
				if(menu.AnimatedOpened){
					//menu.AnimatedOpened = !menu.AnimatedOpened;
					ViewModel.ShowMatcherViewModel();
					ViewModel.Close();
				}
			};

			menuLayout.FindViewById<LinearLayout> (Resource.Id.research_menu_btn).Click += (object sender, EventArgs e) => {
				if(menu.AnimatedOpened){
					//menu.AnimatedOpened = !menu.AnimatedOpened;
					ViewModel.ShowResearchHelpViewModel();
					ViewModel.Close();
				}
			};

			menuLayout.FindViewById<LinearLayout> (Resource.Id.logout_menu_btn).Click += (object sender, EventArgs e) => {
				if(menu.AnimatedOpened){
					//menu.AnimatedOpened = !menu.AnimatedOpened;
					Utilities.CurrentUserimage = null;
					ViewModel.Logout();
				}
			};

			menuLayout.FindViewById<LinearLayout> (Resource.Id.profile_menu_btn).Click += (object sender, EventArgs e) => {
				menu.AnimatedOpened = !menu.AnimatedOpened;
				ViewModel.ShowProfilePicModel();
			};

			nextBtn.Click += (object sender, EventArgs e) => {
				Utilities.LoggedInUsingFb = true;
				ViewModel.NextButtonCommand.Execute(null);
				ViewModel.Close();
				//ViewModel.CheckValues();
			};

			helpicon.Click += (object sender, EventArgs e) => {
				new HelpDialog(this).ShowHelpDialog();
			};

		}
		#endregion


		public void ApplyData(){
			//userNameMenu;
			if (Utilities.CurrentUserimage != null) {
				userImageMenu.SetImageBitmap (Utilities.CurrentUserimage);	
			}

			LoginModel modal = ViewModel.GetUserData();
			userNameMenu.Text = modal.UserEmail;
		}
	}

	#region List Adapter
	public class FbFamilyListAdapter : BaseAdapter
	{
		FbFamilyView myFamilyObj;
		LayoutInflater inflater;
		List<FBListDataStructure> dataList;

		public FbFamilyListAdapter(FbFamilyView myFamilyObj,List<FBListDataStructure> dataList){
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
			FBViewHolder holder;

			if (convertView == null) {
				convertView = inflater.Inflate (Resource.Layout.fb_family_list_item, null);

				holder = new FBViewHolder ();

				holder.userImg = convertView.FindViewById<ImageView> (Resource.Id.user_img);
				holder.check = convertView.FindViewById<CheckBox> (Resource.Id.check_box_right);
			
				holder.nametxt = convertView.FindViewById<TextView> (Resource.Id.username);

				//Java.Lang.Object holderObj = (Java.Lang.Object)holder;
				convertView.SetTag (Resource.Id.fb_family_list,holder);
			} else {
				holder = (FBViewHolder)convertView.GetTag (Resource.Id.fb_family_list);
			}

			FBListDataStructure structure = dataList[position];

			if(structure.isSelected)
			{
				holder.check.Checked = true;
			}
			else {
				holder.check.Checked = false;
			}

			holder.nametxt.Text = structure.Name;

			return convertView;
		}
	}

	public class FBViewHolder : Java.Lang.Object{

		public CheckBox check;
		public ImageView userImg;
		public TextView nametxt;

	}
	#endregion

	#region DataStructure for list
	public class FBListDataStructure
	{
		public bool isSelected{ get; set;}

		public String Name{ get; set;}
		public String ImageUrl{ get; set;}

		public FBListDataStructure(bool isselected,String name,String img){
			isSelected = isselected;

			Name = name;
			ImageUrl = img;
		}

	}
	#endregion
}

