using System;
using Foundation;
using UIKit;
using AncestorCloud.Shared.ViewModels;
using AncestorCloud.Shared;
using System.Drawing;

namespace AncestorCloud.Touch
{
	public partial class ProfilePicView : BaseViewController
	{

		UIActionSheet actionSheet;
		UIImagePickerController imagePicker;
		UIButton choosePhotoButton;
		UIImageView imageView;

		public ProfilePicView () : base ("ProfilePicView", null)
		{
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


			
			// Perform any additional setup after loading the view, typically from a nib.
		}

		public void SetUpView()
		{
			ProfilePic.TouchUpInside += ProfilePicSetUp;
			ProfilePic.Layer.CornerRadius=75f;
			ProfilePic.ClipsToBounds = true;
			UINavigationBar.Appearance.SetTitleTextAttributes (new UITextAttributes ()
				{ TextColor = UIColor.FromRGB (255,255,255) });
			this.Title="Profile Picture";
			this.NavigationItem.HidesBackButton = true;
			this.NavigationController.NavigationBarHidden = false;
			this.NavigationController.NavigationBar.BarTintColor= UIColor.FromRGB (179, 45, 116);
			UINavigationBar.Appearance.SetTitleTextAttributes (new UITextAttributes ()
				{ TextColor = UIColor.FromRGB (255,255,255) });
		}

		partial void UploadButtonTapped (NSObject sender)
		{
			System.Diagnostics.Debug.WriteLine("Upload Button Tapped");
		}
		partial void SkipButtonTapped (NSObject sender)
		{

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
				NSDictionary imageMetadata = e.Info[UIImagePickerController.MediaMetadata] as NSDictionary;
				if(imageMetadata != null) {
					// do something with the metadata
					Console.WriteLine ("got image metadata");
				}

			}
			// if it's a video
			else {
				// get video url
				NSUrl mediaURL = e.Info[UIImagePickerController.MediaURL] as NSUrl;
				if(mediaURL != null) {
					//
					Console.WriteLine(mediaURL.ToString());
				}
			}

			// dismiss the picker
			imagePicker.DismissViewControllerAsync (true);
		}
	

	}
}

