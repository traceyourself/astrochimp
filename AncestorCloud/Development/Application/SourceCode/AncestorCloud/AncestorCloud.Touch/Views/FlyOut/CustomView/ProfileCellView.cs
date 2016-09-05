
using System;

using Foundation;
using UIKit;

namespace AncestorCloud.Touch
{
	public partial class ProfileCellView : UIViewController
	{
		private string _titleText;
		public string TitleText
		{ 
			get{ return _titleText; } 

			set{ 
				_titleText = value;
				SetTitleLabelText(); 
			}
		}

		private UIImage _profileImage;

		public UIImage ProfileImage 
		{ 
			get{ return _profileImage; }

			set{ 

				_profileImage = value;

				SetProfilePic(); 
			}
		}

		public ProfileCellView (string titleText, UIImage ImageUrl) : base ("ProfileCellView", null)
		{
			TitleText = titleText;
			ProfileImage = ImageUrl;
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();


			SetTitleLabelText ();
			SetProfilePic ();
			
			// Perform any additional setup after loading the view, typically from a nib.
		}

		private void SetTitleLabelText()
		{
			if (UserName == null)
				return;
			UserName.Text = TitleText;
		}

		private void SetProfilePic()
		{
			if (ProfilePic == null)
				return;
			ProfilePic.Layer.CornerRadius = 26.5f;
			ProfilePic.ClipsToBounds = true;

			ProfilePic.Image = ProfileImage;
		}
	}
}

