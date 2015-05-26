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
using System.IO;
using Cirrious.MvvmCross.Plugins.Messenger;
using AncestorCloud.Core;

namespace AncestorCloud.Droid
{
	[Activity (Label = "ProfilePicView", ScreenOrientation = ScreenOrientation.Portrait)]			
	public class ProfilePicView : BaseActivity
	{
		ActionBar actionBar;
		TextView uploadBtn,skipTxt;
		ImageView profileImg;
		AlertDialog optionDialog;

		//const for image picker
		int PickImageGalleryId = 10;
		int PickImageCameraId = 20;
		//===
		string CurrentImagePath = "";
		Bitmap CurrentImage;

		IMvxMessenger _messenger;
		private MvxSubscriptionToken ImageUploadedToken;


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

			if(Utilities.CurrentUserimage != null){
				profileImg.SetImageBitmap (Utilities.CurrentUserimage);
			}

			_messenger = Mvx.Resolve<IMvxMessenger>();

		}


		protected override void OnResume ()
		{
			base.OnResume ();
			ImageUploadedToken = _messenger.SubscribeOnMainThread<ProfilePicUploadedMessage>(Message => this.ImageUploadedHandler ());
		}


		protected override void OnPause ()
		{
			base.OnPause ();
			_messenger.Unsubscribe<ProfilePicUploadedMessage> (ImageUploadedToken);
		}

		private void initUI()
		{
			uploadBtn = FindViewById<TextView> (Resource.Id.upload_btn);
			skipTxt = FindViewById<TextView> (Resource.Id.skip_txt);
			profileImg = FindViewById<ImageView> (Resource.Id.selected_pic);
		}

		private void ApplyActions(){

			skipTxt.Click += (object sender, EventArgs e) => {
				ViewModel.ShowFamiyViewModel();
				ViewModel.Close();
			};
			profileImg.Click += (object sender, EventArgs e) => {
				CreateImageOptionDialog();
			};

			uploadBtn.Click += (object sender, EventArgs e) => {
				if(CurrentImagePath.Length == 0){
					Toast.MakeText(this,Resources.GetString(Resource.String.select_to_upload_notification),ToastLength.Short).Show();
				}else{
					var stream = System.IO.File.OpenRead(CurrentImagePath);
					ViewModel.ProfilePicStream = stream;
					ViewModel.UploadImage();
				}
			};

			if(!ViewModel.IsFromSignup){
				skipTxt.Visibility = ViewStates.Gone;
			}
		}


		#region Handler for image uploaded successfully
		public void ImageUploadedHandler()
		{
			Utilities.CurrentUserimage = CurrentImage;
			ViewModel.Close();
		}
		#endregion


		#region Action Bar Configuration
		private void configureActionBar(){
			actionBar = FindViewById <ActionBar>(Resource.Id.actionBar);

			actionBar.SetCenterText (Resources.GetString(Resource.String.profile_pic));

			if (!ViewModel.IsFromSignup) {
				actionBar.SetLeftCornerImage (Resource.Drawable.back);
				var backButton = actionBar.FindViewById <RelativeLayout> (Resource.Id.action_bar_left_btn);
				backButton.Click += (sender, e) => {
					ViewModel.Close();
				};
			}
		}
		#endregion


		public void CreateImageOptionDialog()
		{
			AlertDialog.Builder dialog = new AlertDialog.Builder (this);

			dialog.SetTitle (Resources.GetString(Resource.String.select_option));

			List<string> _lstDataItem = new List<string> ();
			_lstDataItem.Add (Resources.GetString(Resource.String.camera));
			_lstDataItem.Add (Resources.GetString(Resource.String.gallery));
			_lstDataItem.Add (Resources.GetString(Resource.String.cancel));

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
					//Mvx.Trace ("Camera");
					if (IsThereAnAppToTakePictures())
					{
						CreateDirectoryForPictures();
						Intent intent = new Intent(MediaStore.ActionImageCapture);
						string name = StringConstants.PHOTO_NAME+Guid.NewGuid()+StringConstants.PHOTO_EXTENSION;
						CameraDataHolder._file = new Java.IO.File(CameraDataHolder._dir,name);
						intent.PutExtra(MediaStore.ExtraOutput, Android.Net.Uri.FromFile(CameraDataHolder._file));
						StartActivityForResult(intent, PickImageCameraId);
						optionDialog.Dismiss ();
					}
					break;
				}
			case 1:
				{
					//Mvx.Trace ("Gallery");
					Intent = new Intent();
					Intent.SetType("image/*");
					Intent.SetAction(Intent.ActionGetContent);
					StartActivityForResult(Intent.CreateChooser(Intent,Resources.GetString(Resource.String.select_pic)), PickImageGalleryId);
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
			CameraDataHolder._dir = new Java.IO.File(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures),StringConstants.DIRECTORY_NAME);
			if (!CameraDataHolder._dir.Exists())
			{
				CameraDataHolder._dir.Mkdir ();
			}
		}

		private Java.IO.File CreateDirectoryForGalleryPictures()
		{
			Java.IO.File file = new Java.IO.File(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures), StringConstants.DIRECTORY_NAME);
			if (!file.Exists())
			{
				file.Mkdir ();
			}
			return file;
		}
		#endregion

		#region activity result method
		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
		{
			if ((requestCode == PickImageGalleryId) && (resultCode == Result.Ok) && (data != null))
			{
				Android.Net.Uri uri = data.Data;
				//Bitmap bmp = Utilities.GetBitmapFromUri (this,uri);
				string path = Utilities.GetPathToImageFromUri(this,uri);
				Bitmap bmp = Utilities.LoadAndResizeBitmap (path,1024,1024);
				CurrentImage = Utilities.GetRoundedImageFromBitmap (this, bmp, 150);
				profileImg.SetImageBitmap (CurrentImage);
				try{
					string name = StringConstants.PHOTO_NAME+Guid.NewGuid()+StringConstants.PHOTO_EXTENSION;
					Java.IO.File f = new Java.IO.File(CreateDirectoryForGalleryPictures (),name);
					var stream = new FileStream(f.Path, FileMode.Create);
					bmp.Compress(Bitmap.CompressFormat.Png, 80, stream);
					stream.Close();
					CurrentImagePath = f.Path;
				}catch(Java.Lang.Exception e){
					System.Diagnostics.Debug.WriteLine (e.StackTrace);
				}
			}
			if ((requestCode == PickImageCameraId) && (resultCode == Result.Ok))
			{
				CameraDataHolder.bitmap = Utilities.LoadAndResizeBitmap (CameraDataHolder._file.Path,1024,1024);
				Bitmap bmp = Utilities.RotateImageIfRequired (this,CameraDataHolder.bitmap,CameraDataHolder._file.Path);
				CurrentImage = Utilities.GetRoundedImageFromBitmap (this, bmp, 150);
				profileImg.SetImageBitmap (CurrentImage);
				//Save bitmap as a file======
				try{
					Java.IO.File f = CameraDataHolder._file;
					string filename = "";
					if(f.Exists()){
						filename = f.Name;
						f.Delete ();
					}

					var filePath = System.IO.Path.Combine(CameraDataHolder._dir.Path,filename);
					var stream = new FileStream(filePath, FileMode.Create);
					bmp.Compress(Bitmap.CompressFormat.Png, 80, stream);
					stream.Close();
					CameraDataHolder._file = new Java.IO.File(filePath);
					CurrentImagePath = filePath;
				} catch(Java.Lang.Exception e){
					System.Diagnostics.Debug.WriteLine (e.StackTrace);
				}
				//=============

			}
		}
		#endregion

		#region Data holder Class For Camera
		public static class CameraDataHolder{
			public static Java.IO.File _file;
			public static Java.IO.File _dir;     
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