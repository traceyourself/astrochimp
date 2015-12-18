using System;
using Foundation;
using UIKit;
using AncestorCloud.Shared.ViewModels;
using AncestorCloud.Shared;
using System.Drawing;
using System.IO;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Plugins.Messenger;
using AncestorCloud.Core;

namespace AncestorCloud.Touch
{
	public partial class ProfilePicView : BaseViewController
	{

		UIActionSheet actionSheet;
		UIImagePickerController imagePicker;
		UIButton choosePhotoButton;
		UIImageView imageView;
		IMvxMessenger _messenger;
		private MvxSubscriptionToken ImageUploadedToken;

		public ProfilePicView () : base ("ProfilePicView", null)
		{
			_messenger = Mvx.Resolve<IMvxMessenger>();
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public new ProfilePicViewModel ViewModel
		{
			get { return base.ViewModel as ProfilePicViewModel; }
			set { base.ViewModel = value; }
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			SetUpView ();


			AddEvent ();


			// Perform any additional setup after loading the view, typically from a nib.

			if (!ViewModel.IsFromSignup) {
				SkipButton.Hidden = true;
			}

		}

		public override void ViewDidUnload ()
		{
			RemoveEvent();
			base.ViewDidUnload ();
		}

		public void SetUpView()
		{
			ProfilePic.TouchUpInside += ProfilePicSetUp;
			ProfilePic.Layer.CornerRadius=90f;
			ProfilePic.ClipsToBounds = true;
			ProfilePic.ImageView.ContentMode = UIViewContentMode.ScaleAspectFill;

			SetProfilePic ();
		
			UINavigationBar.Appearance.SetTitleTextAttributes (new UITextAttributes ()
				{ TextColor = Themes.TitleTextColor() });
			var Title = Utility.LocalisedBundle ().LocalizedString ("ProfilePictureTitle", "");
			this.NavigationItem.HidesBackButton = true;
			this.NavigationController.NavigationBarHidden = false;
			this.NavigationController.NavigationBar.BarTintColor= Themes.NavBarTintColor();

			UIImage image = UIImage.FromFile (StringConstants.FLYOUTICON);

			image = image.ImageWithRenderingMode (UIImageRenderingMode.AlwaysOriginal);


			this.NavigationItem.SetLeftBarButtonItem(
				new UIBarButtonItem(image
					, UIBarButtonItemStyle.Plain
					, (sender,args) => {
						{
							//message to show the menu
							var messenger = Mvx.Resolve<IMvxMessenger>();
							messenger.Publish(new ToggleFlyoutMenuMessage(this));
						}

					})
				, true);
		}

		partial void UploadButtonTapped (NSObject sender)
		{
			//System.Diagnostics.Debug.WriteLine("Upload Button Tapped");
			ViewModel.UploadImage();
		}
		partial void SkipButtonTapped (NSObject sender)
		{
			this.NavigationController.PopViewController(false);
			ViewModel.ShowFamiyViewModel();
			this.ViewModel.Close();
		}

		 void ProfilePicSetUp(object sender, EventArgs e)
		{
			actionSheet = new UIActionSheet (Utility.LocalisedBundle().LocalizedString("Camera/GalleryText",""));
			actionSheet.AddButton (Utility.LocalisedBundle().LocalizedString("CancelText",""));
			actionSheet.AddButton (Utility.LocalisedBundle().LocalizedString("CameraText",""));
			actionSheet.AddButton (Utility.LocalisedBundle().LocalizedString("GalleryText",""));
			actionSheet.DestructiveButtonIndex = 0;
			actionSheet.Clicked += delegate(object a, UIButtonEventArgs b) {
				
				switch (b.ButtonIndex)
				{
					case 1:
					{
						Camera();

					}
					break;
				
					case 2 :
					{
						Gallery();
					}
					break;

				}

			};

			actionSheet.ShowInView (View);

		}

		public void Gallery()
		{
			View.BackgroundColor =Themes.MatchTableView();

			imageView = new UIImageView(new RectangleF(10, 80, 300, 300));
			Add (imageView);

				// create a new picker controller
				imagePicker = new UIImagePickerController ();

				// set our source to the photo library
				imagePicker.SourceType = UIImagePickerControllerSourceType.PhotoLibrary;

				// set what media types
				imagePicker.MediaTypes = UIImagePickerController.AvailableMediaTypes (UIImagePickerControllerSourceType.PhotoLibrary);

				imagePicker.FinishedPickingMedia += Handle_FinishedPickingMedia;
				imagePicker.Canceled += Handle_Canceled;

				// show the picker
				NavigationController.PresentModalViewController(imagePicker, true);

//			TweetStation.Camera.TakePicture (this, (obj) =>{
//				var photo = obj.ValueForKey(new NSString("UIImagePickerControllerOriginalImage")) as UIImage;
//				var meta = obj.ValueForKey(new NSString("UIImagePickerControllerMediaMetadata")) as NSDictionary;
//
//
//			}


		}
		public void Camera()
		{
			View.BackgroundColor = Themes.MatchTableView();

			imageView = new UIImageView(new RectangleF(10, 80, 300, 300));
			Add (imageView);

			// create a new picker controller
			imagePicker = new UIImagePickerController ();

			// set our source to the photo library
			imagePicker.SourceType = UIImagePickerControllerSourceType.Camera;

			// set what media types
			imagePicker.MediaTypes = UIImagePickerController.AvailableMediaTypes (UIImagePickerControllerSourceType.Camera);

			imagePicker.FinishedPickingMedia += Handle_FinishedPickingMedia;
			imagePicker.Canceled += Handle_Canceled;

			// show the picker
			NavigationController.PresentModalViewController(imagePicker, true);

		}


		void Handle_Canceled (object sender, EventArgs e) 
		{
			imagePicker.DismissViewControllerAsync(true);
		}

		// This is a sample method that handles the FinishedPickingMediaEvent
		protected void Handle_FinishedPickingMedia (object sender, UIImagePickerMediaPickedEventArgs e)
		{
			// determine what was selected, video or image
			bool isImage = false;
			switch(e.Info[UIImagePickerController.MediaType].ToString())
			{
			case "public.image":
//				Console.WriteLine("Image selected");
				isImage = true;
				break;

			case "public.video":
//				Console.WriteLine("Video selected");
				break;
			}

			Console.Write("Reference URL: [" + UIImagePickerController.ReferenceUrl + "]");

			// get common info (shared between images and video)
			NSUrl referenceURL = e.Info[new NSString("UIImagePickerControllerReferenceUrl")] as NSUrl;
			if (referenceURL != null) 
				Console.WriteLine(referenceURL.ToString ());

			// if it was an image, get the other image info
			if(isImage) {

				// get the original image
				UIImage originalImage = e.Info[UIImagePickerController.OriginalImage] as UIImage;
				if(originalImage != null) {
					// do something with the image
					//Console.WriteLine ("got the original image");
					//imageView.Image = originalImage;
					ProfilePic.SetBackgroundImage (originalImage, UIControlState.Normal);
				}

				// get the edited image
				UIImage editedImage = e.Info[UIImagePickerController.EditedImage] as UIImage;
				if(editedImage != null) {
					// do something with the image
					//Console.WriteLine ("got the edited image");
					//imageView.Image = editedImage;
				}

				//- get the image metadata
//				NSDictionary imageMetadata = e.Info[UIImagePickerController.MediaMetadata] as NSDictionary;
//				if(imageMetadata != null) {
//					// do something with the metadata
//					Console.WriteLine ("got image metadata");
//				}
				//originalImage = ResizeImage(originalImage);


				AppDelegate appDelegate = (AppDelegate)UIApplication.SharedApplication.Delegate;
				appDelegate.UIImageProfilePic = originalImage;

				var documentsDirectory = Environment.GetFolderPath
					(Environment.SpecialFolder.MyDocuments);

				string jpgFilename = System.IO.Path.Combine (documentsDirectory, "../Library/Caches/ProfilePic.jpg");
				NSData imgData = originalImage.AsJPEG();
				NSError err = null;
				if (imgData.Save(jpgFilename, false, out err))
				{
					//Console.WriteLine("saved at " + jpgFilename);
					Stream stream = File.OpenRead (jpgFilename);
					ViewModel.ProfilePicStream = stream;
					
				} else {
					//Console.WriteLine("NOT saved as" + jpgFilename + " because" + err.LocalizedDescription);
				}

			}
//			// if it's a video
//			else {
//				// get video url
//				NSUrl mediaURL = e.Info[UIImagePickerController.MediaURL] as NSUrl;
//				if(mediaURL != null) {
//					//
//					Console.WriteLine(mediaURL.ToString());
//				}
//			}

			// dismiss the picker
			imagePicker.DismissViewControllerAsync (true);
		}


		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			App.controllerTypeRef = ControllerType.Primary;

			this.View.BackgroundColor = Themes.MatchTableView();
			SetProfilePic ();
			this.View.BackgroundColor = UIColor.Red;
		}


		public void AddEvent()
		{
			ImageUploadedToken = _messenger.SubscribeOnMainThread<ProfilePicUploadedMessage>(Message => this.ImageUploadedHandler ());
		}

		public void RemoveEvent()
		{
			_messenger.Unsubscribe<ProfilePicUploadedMessage> (ImageUploadedToken);
		}
		public void ImageUploadedHandler()
		{
			//Utilities.CurrentUserimage = CurrentImage;

			SetProfilePic ();

			if (ViewModel.IsFromSignup) {
				if(this.NavigationController != null)
					this.NavigationController.PopViewController (false);
			}
			//ViewModel.Close ();
		}

		private void SetProfilePic()
		{
			AppDelegate appDelegate = (AppDelegate)UIApplication.SharedApplication.Delegate;
			if (appDelegate.UIImageProfilePic != null) {
				UIImage image = MaxResizeImage(appDelegate.UIImageProfilePic,180,180);
				ProfilePic.SetBackgroundImage (image, UIControlState.Normal);
			}
		}

		public UIImage ResizeImage(UIImage img )
		{
			
			float mwidth = (float)img.Size.Width;
			float mheight = (float)img.Size.Height;
			float newWidth = 360f;
			float newHeigth = mheight * newWidth / mwidth; // I always hope I get this scaling thing right. #crossedfingers
			UIGraphics.BeginImageContextWithOptions(new SizeF(mwidth, mheight), false, 1.0f);
			img.Draw (new RectangleF (0, 0, newWidth, newHeigth));
			img = UIGraphics.GetImageFromCurrentImageContext();
			UIGraphics.EndImageContext ();
			return img;
		}

		public UIImage MaxResizeImage(UIImage sourceImage, float maxWidth, float maxHeight)
		{
			var sourceSize = sourceImage.Size;
			var maxResizeFactor = Math.Max(maxWidth / sourceSize.Width, maxHeight / sourceSize.Height);
			if (maxResizeFactor > 1) return sourceImage;
			var width = maxResizeFactor * sourceSize.Width;
			var height = maxResizeFactor * sourceSize.Height;
			//UIGraphics.BeginImageContext(new SizeF((float)width, (float)height));
			UIGraphics.BeginImageContextWithOptions(new SizeF((float)width, (float)height), false, 2.0f);
			sourceImage.Draw(new RectangleF(0, 0, (float)width, (float)height));
			var resultImage = UIGraphics.GetImageFromCurrentImageContext();
			UIGraphics.EndImageContext();

			return CropImage(resultImage,0,0,(int)maxWidth,(int)maxHeight);
		}

		private UIImage CropImage(UIImage sourceImage, int crop_x, int crop_y, int width, int height)
		{
			var imgSize = sourceImage.Size;
			UIGraphics.BeginImageContext(new SizeF(width, height));
			var context = UIGraphics.GetCurrentContext();
			var clippedRect = new RectangleF(0, 0, width, height);
			context.ClipToRect(clippedRect);
			var drawRect = new RectangleF(-crop_x, -crop_y,(float) imgSize.Width, (float)imgSize.Height);
			sourceImage.Draw(drawRect);
			var modifiedImage = UIGraphics.GetImageFromCurrentImageContext();
			UIGraphics.EndImageContext();
			return modifiedImage;
		}
	

	}
}

