using System;
using UIKit;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Foundation;

namespace AncestorCloud.Touch
{
	public class CelebritiesTableSource : MvxTableViewSource
	{

		readonly string cellIdentifier = "CelebritiesCell";


		public CelebritiesTableSource(UITableView tableView): base(tableView)
		{
			tableView.RegisterNibForCellReuse(UINib.FromName("CelebritiesCell", NSBundle.MainBundle),
				CelebritiesCell.Key);

		}

		//		public override nint RowsInSection (UITableView tableView, nint section)
		//		{
		//
		//			return 5;
		//		}



		protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
		{
			return tableView.DequeueReusableCell(CelebritiesCell.Key, indexPath);
		}


	}
}

