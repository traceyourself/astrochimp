using System;
using UIKit;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Foundation;

namespace AncestorCloud.Touch
{

	public class RelationshipMatchTableSource :MvxTableViewSource
	{
		readonly string cellIdentifier = "RelationshipMatchDetailCell";

		public RelationshipMatchTableSource(UITableView tableView): base(tableView)
		{
			tableView.RegisterNibForCellReuse(UINib.FromName("RelationshipMatchDetailCell", NSBundle.MainBundle),
				RelationshipMatchDetailCell.Key);

		}

		protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
		{
			return tableView.DequeueReusableCell(RelationshipMatchDetailCell.Key, indexPath);
		}


	}
}

