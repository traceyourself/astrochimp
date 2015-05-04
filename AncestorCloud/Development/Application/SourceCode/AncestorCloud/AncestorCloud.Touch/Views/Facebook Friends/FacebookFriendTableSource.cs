using System;
using UIKit;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Foundation;

namespace AncestorCloud.Touch
{
	public class FacebookFriendTableSource :MvxTableViewSource
	{
		readonly string cellIdentifier = "FacebookFriendCell";

		public FacebookFriendTableSource(UITableView tableView): base(tableView)
		{
			tableView.RegisterNibForCellReuse(UINib.FromName("FacebookFriendCell", NSBundle.MainBundle),
				FacebookFriendCell.Key);

		}

		protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
		{
			return tableView.DequeueReusableCell(FacebookFriendCell.Key, indexPath);
		}

	}
}



