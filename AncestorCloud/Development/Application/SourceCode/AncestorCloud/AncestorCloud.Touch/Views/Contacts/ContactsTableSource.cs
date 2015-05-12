using System;
using UIKit;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Foundation;

namespace AncestorCloud.Touch
{
	public class ContactsTableSource :MvxTableViewSource
	{
		readonly string cellIdentifier = "ContactsCell";

		public ContactsTableSource(UITableView tableView): base(tableView)
		{
			tableView.RegisterNibForCellReuse(UINib.FromName("ContactsCell", NSBundle.MainBundle),
				ContactsCell.Key);

		}

		protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
		{
			return tableView.DequeueReusableCell(ContactsCell.Key, indexPath);
		}

	}


}