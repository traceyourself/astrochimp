using System;
using UIKit;

namespace AncestorCloud.Touch
{
	public class MyTitleView: UIView
	{


		public MyTitleView (string title)
		{
			UIImageView img = new UIImageView(UIImage.FromFile("myfamily_icon.png"));
			this.AddSubview (img);

			UILabel label = new UILabel {
				Text= title,
			};
			this.AddSubview (label);

		}
	}
}

