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

			float constant = 0.88f;

			if (ViewModel.IsFromSignup) {

				constant = 1.0f;
			} else {


				float width = (float)UIScreen.MainScreen.ApplicationFrame.Size.Width;

				if (width <= 320f)
					constant = 1.0f;

				if (width >= 375f)
					constant = 0.80f;
			}




			this.View.AddConstraint (NSLayoutConstraint.Create (this.ContentView, NSLayoutAttribute.Leading, 0, this.View, NSLayoutAttribute.Left, 1.0f, 0));

			this.View.AddConstraint (NSLayoutConstraint.Create (this.ContentView, NSLayoutAttribute.Trailing , 0, this.View, NSLayoutAttribute.Right, constant, 0));
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

			ImageUploadedHandler ();
		
			UINavigationBar.Appearance.SetTitleTextAttributes (new UITextAttributes ()
				{ TextColor = UIColor.FromRGB (255,255,255) });
			this.Title="Profile Picture";
			this.NavigationItem.HidesBackButton = true;
			this.NavigationController.NavigationBarHidden = false;
			this.NavigationController.NavigationBar.BarTintColor= UIColor.FromRGB (64,172,176);



			if (ViewModel.IsFromSignup)
				return;
			
			UIImage image = UIImage.FromFile ("action_menu.png");

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
			actionSheet = new UIActionSheet ("Open Camera/Gallery");
			actionSheet.AddButton ("Cancel");
			actionSheet.AddButton ("Camera");
			actionSheet.AddButton ("Gallery");
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
			View.BackgroundColor = UIColor.FromRGB(239,239,239);

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
			View.BackgroundColor = UIColor.FromRGB(239,239,239);

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
			Console.WriteLine ("picker cancelled");
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
				Console.WriteLine("Image selected");
				isImage = true;
				break;

			case "public.video":
				Console.WriteLine("Video selected");
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
					Console.WriteLine ("got the original image");
					//imageView.Image = originalImage;
					ProfilePic.SetBackgroundImage (originalImage, UIControlState.Normal);
				}

				// get the edited image
				UIImage editedImage = e.Info[UIImagePickerController.EditedImage] as UIImage;
				if(editedImage != null) {
					// do something with the image
					Console.WriteLine ("got the edited image");
					//imageView.Image = editedImage;
				}

				//- get the image metadata
//				NSDictionary imageMetadata = e.Info[UIImagePickerController.MediaMetadata] as NSDictionary;
//				if(imageMetadata != null) {
//					// do something with the metadata
//					Console.WriteLine ("got image metadata");
//				}


				AppDelegate appDelegate = (AppDelegate)UIApplication.SharedApplication.Delegate;
				appDelegate.UIImageProfilePic = originalImage;

//				var documentsDirectory = Environment.GetFolderPath
//					(Environment.SpecialFolder.MyDocuments);

				var documents = NSFileManager.DefaultManager.GetUrls (NSSearchPathDirectory.CachesDirectory, NSSearchPathDomain.User) [0];
				string documentsDirectory = documents.ToString();

				string jpgFilename = System.IO.Path.Combine (documentsDirectory, "ProfilePic.jpg");
				NSData imgData = originalImage.AsJPEG();
				NSError err = null;
				if (imgData.Save(jpgFilename, false, out err))
				{
					Console.WriteLine("saved at " + jpgFilename);
					Stream stream = File.OpenRead (jpgFilename);
					ViewModel.ProfilePicStream = stream;
					
				} else {
					Console.WriteLine("NOT saved as" + jpgFilename + " because" + err.LocalizedDescription);
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
			this.View.BackgroundColor = UIColor.FromRGB (0, 0, 0);
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
			AppDelegate appDelegate = (AppDelegate)UIApplication.SharedApplication.Delegate;
			if (appDelegate.UIImageProfilePic != null)
				ProfilePic.SetBackgroundImage (appDelegate.UIImageProfilePic, UIControlState.Normal);
			//ViewModel.Close ();
		}
	

	}
}

