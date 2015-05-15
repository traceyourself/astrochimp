
using System;

using Foundation;
using UIKit;

namespace AncestorCloud.Touch
{
	public partial class CustomCellView : UIViewController
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

		private String _profileImage;

		public String ProfileImage 
		{ 
			get{ return _profileImage; }

			set{ 

				_profileImage = value;

				SetIconImage(); 
			}
		}

		public CustomCellView (string titleText, String ImageUrl) : base ("CustomCellView", null)
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
			SetIconImage ();
			
			// Perform any additional setup after loading the view, typically from a nib.
		}

		private void SetTitleLabelText()
		{
			if (TitleLabel == null)
				return;
			
			TitleLabel.Text = TitleText;
		}

		private void SetIconImage()
		{
			if (IconImage == null)
				return;

			IconImage.Image = UIImage.FromBundle(ProfileImage);
		}
	}
}

