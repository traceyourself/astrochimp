using System;
using UIKit;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Foundation;

namespace AncestorCloud.Touch
{
	public class PastMatchesTableSoure : MvxTableViewSource
	{

		readonly string cellIdentifier = "PastMatchesCell";


		public PastMatchesTableSoure(UITableView tableView): base(tableView)
		{
			tableView.RegisterNibForCellReuse(UINib.FromName("PastMatchesCell", NSBundle.MainBundle),
				PastMatchesCell.Key);

		}

		protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
		{
			return tableView.DequeueReusableCell(PastMatchesCell.Key, indexPath);
		}


	}
}

