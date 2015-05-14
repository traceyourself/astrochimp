using System;
using UIKit;
using System.Drawing;
using CoreGraphics;

namespace AncestorCloud.Touch
{
	public class MyContactTitleView: UIView
	{


		public MyContactTitleView (string title,CGRect frame) : base (frame)
		{
			UIImageView img = new UIImageView(UIImage.FromFile("contact_white.png"));
			this.AddSubview (img);

			UILabel label = new UILabel {
				Text= title,
				TextColor = UIColor.White,
				Frame= new RectangleF((float)img.Frame.Size.Width+5.0f,0,80,20)
			};
			this.AddSubview (label);

		}

	}
}