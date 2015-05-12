using System;
using UIKit;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Foundation;
using AncestorCloud.Shared;
using System.Collections.Generic;
using System.Linq;

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


		protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
		{
			CelebritiesCell cell = (CelebritiesCell) tableView.DequeueReusableCell(CelebritiesCell.Key, indexPath);
  		
			IEnumerable<Celebrity> myEnumerable = this.ItemsSource as IEnumerable<Celebrity>;

			List<Celebrity> listAgain = myEnumerable.ToList();

			Celebrity childItem = listAgain [indexPath.Row];

			cell.familyMember = childItem;

			return cell;
		}




	}
}

