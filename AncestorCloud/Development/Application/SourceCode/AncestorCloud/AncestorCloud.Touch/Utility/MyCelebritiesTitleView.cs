using System;
using UIKit;
using System.Drawing;
using CoreGraphics;

namespace AncestorCloud.Touch
{
	public class MyCelebritiesTitleView: UIView
	{


		public MyCelebritiesTitleView (string title,CGRect frame) : base (frame)
		{
			UIImageView img = new UIImageView(UIImage.FromFile("celebrity_white.png"));
			this.AddSubview (img);

			UILabel label = new UILabel {
				Text= title,
				TextColor = UIColor.White,
				Frame= new RectangleF((float)img.Frame.Size.Width+5.0f,0,90,20)
			};
			this.AddSubview (label);

		}

	}
}