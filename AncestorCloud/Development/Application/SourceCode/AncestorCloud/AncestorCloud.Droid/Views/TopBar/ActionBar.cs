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
using Android.Util;
using Android.Content.PM;

namespace AncestorCloud.Droid
{
	[Activity (Label = "ActionBar", ScreenOrientation = ScreenOrientation.Portrait)]			
	public class ActionBar : RelativeLayout
	{
		#region global variables
		LayoutInflater inflater;
		ImageView leftImage,leftMenuImage,centerImage,rightImg,rightBigImg;
		TextView centerTxt;
		//RelativeLayout leftBtn,rightBtn;
		#endregion

		#region constructors
		public ActionBar(Context context) :base(context)
		{
			init (context);
		}

		public ActionBar(Context context, IAttributeSet attrs) : base(context, attrs)
		{
			init (context);
		}
		#endregion



		#region init Method
		private void init(Context context)
		{
			inflater = (LayoutInflater)context.GetSystemService (Context.LayoutInflaterService);
			RelativeLayout mainView = (RelativeLayout)inflater.Inflate (Resource.Layout.action_bar,null);

			leftImage = mainView.FindViewById<ImageView> (Resource.Id.action_bar_left_img);
			leftMenuImage = mainView.FindViewById<ImageView> (Resource.Id.action_bar_left_menu_img);
			centerImage = mainView.FindViewById<ImageView> (Resource.Id.action_center_img);
			rightImg = mainView.FindViewById<ImageView> (Resource.Id.action_bar_right_img);
			rightBigImg = mainView.FindViewById<ImageView> (Resource.Id.action_bar_big_right_img);

			centerTxt = mainView.FindViewById<TextView> (Resource.Id.action_center_txt);

			//leftBtn = mainView.FindViewById<RelativeLayout> (Resource.Id.action_bar_left_btn);
			//rightBtn = mainView.FindViewById<RelativeLayout> (Resource.Id.action_bar_right_btn);

			AddView (mainView);
		}
		#endregion


		#region Customizing Methods
		public void SetLeftCornerImage(int drawableId)
		{
			leftImage.SetImageResource (drawableId);
		}

		public void SetLeftCornerMenuImage(int drawableId)
		{
			leftMenuImage.SetImageResource (drawableId);
		}

		public void SetCenterText(String txt)
		{
			centerTxt.Text = txt;
			centerImage.Visibility = ViewStates.Gone;
		}

		public void SetCenterImage(int imgId)
		{
			centerImage.SetImageResource (imgId);
			centerTxt.Visibility = ViewStates.Gone;
		}

		public void SetCenterImageText(int imgId,string txt)
		{
			centerTxt.Text = txt;
			centerImage.SetImageResource (imgId);
			centerTxt.Visibility = ViewStates.Visible;
			centerImage.Visibility = ViewStates.Visible;
		}


		public void SetRightImage(int imgId)
		{
			rightImg.SetImageResource (imgId);
		}

		public void SetRightBigImage(int imgId)
		{
			rightBigImg.SetImageResource (imgId);
		}

		#endregion
	}
}

