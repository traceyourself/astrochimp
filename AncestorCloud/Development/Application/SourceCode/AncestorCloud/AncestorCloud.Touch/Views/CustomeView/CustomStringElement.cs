using System;
using UIKit;
using MonoTouch.Dialog;

namespace AncestorCloud.Touch
{
	public class CustomStringElement : StringElement
	{
		public CustomStringElement(String caption): base(caption)
		{

		}

		public override UITableViewCell GetCell (UITableView tv)
		{
			var cell = base.GetCell (tv);
			cell.BackgroundColor = UIColor.FromRGB(46, 58, 73);
			cell.TextLabel.TextColor = UIColor.White;
			return cell;

		}

	}
}

