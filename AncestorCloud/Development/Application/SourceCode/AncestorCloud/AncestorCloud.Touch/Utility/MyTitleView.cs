using System;
using UIKit;

namespace AncestorCloud.Touch
{
	public class MyTitleView: UIView
	{
		public MyTitleView ()
		{
			UIImageView img = new UIImageView(UIImage.FromFile("cross.png"));
			this.AddSubview (img);
		}
	}
}

