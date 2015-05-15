using System;
using UIKit;
using MonoTouch.Dialog;

namespace AncestorCloud.Touch
{
	public class CustomStringElement : ImageStringElement
	{
		public CustomStringElement(string caption, UIImage image): base(caption,image)
		{

		}

		public override UITableViewCell GetCell (UITableView tv)
		{
			var cell = base.GetCell (tv);
			cell.BackgroundColor = UIColor.FromRGB(46, 58, 73);
			cell.TextLabel.TextColor = UIColor.White;
			tv.ScrollEnabled = false;
			//tv.SeparatorStyle = UITableViewCellSeparatorStyle.None;
			return cell;

		}

	}


	public class CustomViewElement : UIViewElement
	{
		public CustomViewElement(string caption, UIView image): base(caption,image,false)
		{

		}

		public override UITableViewCell GetCell (UITableView tv)
		{
			var cell = base.GetCell (tv);
			cell.BackgroundColor = UIColor.FromRGB(46, 58, 73);
			cell.TextLabel.TextColor = UIColor.White;
			cell.SelectionStyle = UITableViewCellSelectionStyle.None;
			tv.ScrollEnabled = false;
			tv.SeparatorStyle = UITableViewCellSeparatorStyle.None;
			return cell;

		}

	}
}

