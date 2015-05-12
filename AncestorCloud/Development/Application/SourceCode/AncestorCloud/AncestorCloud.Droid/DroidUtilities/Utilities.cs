
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
using System.Net;
using Android.Graphics.Drawables;
using Android.Media;

namespace AncestorCloud.Droid
{
	[Activity (Label = "Utilities")]			
	public class Utilities 
	{
		public static void RegisterCertificateForApiHit()
		{
			System.Net.ServicePointManager.ServerCertificateValidationCallback =
				new System.Net.Security.RemoteCertificateValidationCallback(delegate { return true; });

			/*try { 
				var w = HttpWebRequest.Create ("https://wsdev.onegreatfamily.com/v11.02/User.svc/Signin?username=mikeyamadeo@gmail.com&Password=password&DeveloperId=AncestorCloud&DeveloperPassword=492C4DD9-A129-4146-BAE9-D0D45FBC315C"); 
				using (var response = w.GetResponse ()) 
				using (var r = new StreamReader (response.GetResponseStream ())) { 
					Mvx.Trace("response : " +r.ReadToEnd ()); 
				} 
			} catch (Exception e) { 
				Console.WriteLine ("error: {0}", e); 
			} */
		}

		#region Holding instance of current Activity for Loader

		public static Activity CurrentActiveActivity{ get; set;}

		#endregion

		#region Holding person type
		public static String AddPersonType{ get; set;}
		#endregion

		#region Holding Bool to check if from fb or normal login
		public static bool LoggedInUsingFb{ get; set;}
		#endregion


		#region Round image Methods

		public static Bitmap GetRoundedImageFromBitmap (Context c,Bitmap img,int radius){
			Bitmap _bfinal = getRoundedShape (img,radius);
			return _bfinal;
		}

		public static Bitmap GetRoundedimage (Context c,String url,int imgid,int radius){

			if(url.Length > 0){
				Bitmap _bimage = GetImageBitmapFromUrl (url);

				Bitmap _bfinal = getRoundedShape (_bimage,radius);
				return _bfinal;
			}else{
				Drawable d = c.Resources.GetDrawable (imgid);
				Bitmap _bimage  = ((BitmapDrawable) d).Bitmap;

				Bitmap _bfinal = getRoundedShape (_bimage,radius);
				return _bfinal;	
			}
		}

		public static Bitmap GetImageBitmapFromUrl(string url)
		{
			Bitmap imageBitmap = null;
			if(!(url=="null"))
				using (var webClient = new WebClient())
				{
					var imageBytes = webClient.DownloadData(url);
					if (imageBytes != null && imageBytes.Length > 0)
					{
						imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
					}
				}

			System.Console.Out.WriteLine ("Return fn");
			return imageBitmap;
		}

		public static Bitmap getRoundedShape(Bitmap scaleBitmapImage,int radius) {
			int targetWidth = radius+radius;
			int targetHeight = radius+radius;
			Bitmap targetBitmap = Bitmap.CreateBitmap(targetWidth, 
				targetHeight,Bitmap.Config.Argb8888);

			Canvas canvas = new Canvas(targetBitmap);
			Android.Graphics.Path path = new Android.Graphics.Path();
			path.AddCircle(((float) targetWidth - 1) / 2,
				((float) targetHeight - 1) / 2,
				(Math.Min(((float) targetWidth), 
					((float) targetHeight)) / 2),
				Android.Graphics.Path.Direction.Ccw);

			canvas.ClipPath(path);
			Bitmap sourceBitmap = scaleBitmapImage;
			canvas.DrawBitmap(sourceBitmap, 
				new Rect(0, 0, sourceBitmap.Width,
					sourceBitmap.Height), 
				new Rect(0, 0, targetWidth, targetHeight), null);
			return targetBitmap;
		}
		#endregion

		#region get image apth
		public static string GetPathToImageFromUri(Activity act,Android.Net.Uri uri)
		{
			string path = null;
			// The projection contains the columns we want to return in our query.
			string[] projection = new[] { Android.Provider.MediaStore.Images.Media.InterfaceConsts.Data };
			using (Android.Database.ICursor cursor = act.ManagedQuery(uri, projection, null, null, null))
			{
				if (cursor != null)
				{
					int columnIndex = cursor.GetColumnIndexOrThrow(Android.Provider.MediaStore.Images.Media.InterfaceConsts.Data);
					cursor.MoveToFirst();
					path = cursor.GetString(columnIndex);
				}
			}
			return path;
		}
		#endregion


		#region bitmap helpers
		public static Android.Graphics.Bitmap GetBitmapFromUri(Activity act,Android.Net.Uri uriImage)
		{
			Android.Graphics.Bitmap mBitmap = null;
			mBitmap = Android.Provider.MediaStore.Images.Media.GetBitmap(act.ContentResolver, uriImage);
			return mBitmap;
		}


		public static Bitmap LoadAndResizeBitmap(string fileName, int width, int height)
		{
			// First we get the the dimensions of the file on disk
			BitmapFactory.Options options = new BitmapFactory.Options { InJustDecodeBounds = true };
			BitmapFactory.DecodeFile(fileName, options);

			// Next we calculate the ratio that we need to resize the image by
			// in order to fit the requested dimensions.
			int outHeight = options.OutHeight;
			int outWidth = options.OutWidth;
			int inSampleSize = 1;

			if (outHeight > height || outWidth > width)
			{
				inSampleSize = outWidth > outHeight
					? outHeight / height
					: outWidth / width;
			}

			// Now we will load the image and have BitmapFactory resize it for us.
			options.InSampleSize = inSampleSize;
			options.InJustDecodeBounds = false;
			Bitmap resizedBitmap = BitmapFactory.DecodeFile(fileName, options);

			return resizedBitmap;
		}


		public static Bitmap RotateImageIfRequired(Context context,Bitmap img,String path) {

			// Detect rotation
			int rotation = GetRotation(path);
			if(rotation!=0){
				Matrix matrix = new Matrix();
				matrix.PostRotate(rotation);
				Bitmap rotatedImg = Bitmap.CreateBitmap(img, 0, 0,img.Width,img.Height, matrix, true);
				img.Recycle();
				return rotatedImg;        
			}else{
				return img;
			}
		}

		public static int GetRotation(String imgPath)
		{
			ExifInterface ei = new ExifInterface(imgPath);
			int orientation = ei.GetAttributeInt(ExifInterface.TagOrientation,6);
			int rotation = 0;
			switch (orientation) {
			case 6:
				rotation = 90;
				break;
			case 1:
				rotation = 180;
				break;
			case 8:
				rotation = -90;
				break;
			default :
				rotation = 0;
				break;
			}
			return rotation;
		}
		#endregion
	}

}

