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
using Cirrious.CrossCore;
using Java.Lang;
using Android.Graphics;
using Android.Content.PM;
using Android.Provider;
using AncestorCloud.Shared;
using Java.IO;

namespace AncestorCloud.Droid
{
	[Activity (Label = "ProfilePicView")]			
	public class ProfilePicView : BaseActivity
	{
		ActionBar actionBar;
		LinearLayout menuLayout,contentLayout;
		TextView uploadBtn,skipTxt;
		ImageView profileImg;
		AlertDialog optionDialog;

		//const for image picker
		int PickImageGalleryId = 10;
		int PickImageCameraId = 20;
		//===

		public new ProfilePicViewModel ViewModel
		{
			get { return base.ViewModel as ProfilePicViewModel; }
			set { base.ViewModel = value; }
		}

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.upload_pic);

			initUI ();
			configureActionBar ();
			ApplyActions ();
		}

		private void initUI()
		{
			uploadBtn = FindViewById<TextView> (Resource.Id.upload_btn);
			skipTxt = FindViewById<TextView> (Resource.Id.skip_txt);
			profileImg = FindViewById<ImageView> (Resource.Id.selected_pic);
		}

		private void ApplyActions(){
			skipTxt.Click += (object sender, EventArgs e) => {
				ViewModel.Close();
			};
			profileImg.Click += (object sender, EventArgs e) => {
				CreateImageOptionDialog();
			};
		}

		#region Action Bar Configuration
		private void configureActionBar(){
			actionBar = FindViewById <ActionBar>(Resource.Id.actionBar);
			actionBar.SetLeftCornerImage (Resource.Drawable.back);

			actionBar.SetCenterText (Resources.GetString(Resource.String.profile_pic));

			var backButton = actionBar.FindViewById <RelativeLayout> (Resource.Id.action_bar_left_btn);

			backButton.Click += (sender, e) => {
				ViewModel.Close();
			};
		}
		#endregion


		public void CreateImageOptionDialog()
		{
			AlertDialog.Builder dialog = new AlertDialog.Builder (this);

			dialog.SetTitle ("Select an option");

			List<string> _lstDataItem = new List<string> ();
			_lstDataItem.Add ("Camera");
			_lstDataItem.Add ("Gallery");
			_lstDataItem.Add ("Cancel");


			var listView = new ListView (this);
			listView.Adapter = new AlertListViewAdapter (this, _lstDataItem);
			listView.ItemClick += listViewItemClick;
			dialog.SetView (listView);

			optionDialog = dialog.Create ();
			optionDialog.Show ();

		}

		#region Dialog list click handler
		void listViewItemClick (object sender, AdapterView.ItemClickEventArgs e)
		{
			switch(e.Position){
			case 0:
				{
					Mvx.Trace ("Camera");
					if (IsThereAnAppToTakePictures())
					{
						CreateDirectoryForPictures();
						Intent intent = new Intent(MediaStore.ActionImageCapture);
						CameraDataHolder._file = new File(CameraDataHolder._dir, System.String.Format("myPhoto_{0}.jpg", Guid.NewGuid()));
						intent.PutExtra(MediaStore.ExtraOutput, Android.Net.Uri.FromFile(CameraDataHolder._file));
						StartActivityForResult(intent, PickImageCameraId);
						optionDialog.Dismiss ();
					}
					break;
				}
			case 1:
				{
					Mvx.Trace ("Gallery");
					Intent = new Intent();
					Intent.SetType("image/*");
					Intent.SetAction(Intent.ActionGetContent);
					StartActivityForResult(Intent.CreateChooser(Intent, "Select Picture"), PickImageGalleryId);
					optionDialog.Dismiss ();
					break;
				}
			case 2:
				{	optionDialog.Dismiss ();
					break;
				}
			}
		}
		#endregion


		#region Camera Methods
		private bool IsThereAnAppToTakePictures()
		{
			Intent intent = new Intent(MediaStore.ActionImageCapture);
			IList<ResolveInfo> availableActivities = PackageManager.QueryIntentActivities(intent, PackageInfoFlags.MatchDefaultOnly);
			return availableActivities != null && availableActivities.Count > 0;
		}
		private void CreateDirectoryForPictures()
		{
			CameraDataHolder._dir = new File(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures), "Ancestor Cloud");
			if (!CameraDataHolder._dir.Exists())
			{
				CameraDataHolder._dir.Mkdir ();
			}
		}
		#endregion

		#region activity result method
		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
		{
			if ((requestCode == PickImageGalleryId) && (resultCode == Result.Ok) && (data != null))
			{
				Android.Net.Uri uri = data.Data;

				Bitmap bmp = Utilities.GetBitmapFromUri (this,uri);
				bmp = Utilities.GetRoundedImageFromBitmap (this, bmp, 150);
				profileImg.SetImageBitmap (bmp);
				//string path = Utilities.GetPathToImageFromUri(this,uri);
				//Toast.MakeText(this, path, ToastLength.Long);
			}
			if ((requestCode == PickImageCameraId) && (resultCode == Result.Ok))
			{
				/*Intent mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
				Android.Net.Uri contentUri = Android.Net.Uri.FromFile(CameraDataHolder._file);
				mediaScanIntent.SetData(contentUri);
				SendBroadcast(mediaScanIntent);*/

				// display in ImageView. We will resize the bitmap to fit the display
				// Loading the full sized image will consume to much memory 
				// and cause the application to crash.
				/*int height = Resources.DisplayMetrics.HeightPixels;
				int width = _imageView.Width ;*/

				CameraDataHolder.bitmap = Utilities.LoadAndResizeBitmap (CameraDataHolder._file.Path,1024,1024);
				Bitmap bmp = Utilities.GetRoundedImageFromBitmap (this, CameraDataHolder.bitmap, 150);
				bmp = Utilities.RotateImageIfRequired (this,bmp,CameraDataHolder._file.Path);
				profileImg.SetImageBitmap (bmp);
			}
		}
		#endregion

		#region Class as Data holder For Camera
		public static class CameraDataHolder{
			public static File _file;
			public static File _dir;     
			public static Bitmap bitmap;
		}
		#endregion

		#region dialog list adapter
		internal class AlertListViewAdapter: BaseAdapter<string>
		{
			Activity _context = null;
			List<System.String> _lstDataItem = null;

			public AlertListViewAdapter (Activity context, List<System.String> lstDataItem)
			{
				_context = context;
				_lstDataItem = lstDataItem;			
			}

			#region implemented abstract members of BaseAdapter

			public override long GetItemId (int position)
			{
				return position;
			}

			public override View GetView (int position, View convertView, ViewGroup parent)
			{
				if (convertView == null)
					convertView = _context.LayoutInflater.Inflate (Android.Resource.Layout.SimpleListItem1, null);

				(convertView.FindViewById<TextView> (Android.Resource.Id.Text1))
					.SetText (this [position], TextView.BufferType.Normal);

				return convertView;
			}

			public override int Count {
				get {
					return _lstDataItem.Count;
				}
			}

			#endregion

			#region implemented abstract members of BaseAdapter

			public override string this [int index] {
				get {
					return _lstDataItem [index];
				}
			}

			#endregion

		}
		#endregion

	}
}