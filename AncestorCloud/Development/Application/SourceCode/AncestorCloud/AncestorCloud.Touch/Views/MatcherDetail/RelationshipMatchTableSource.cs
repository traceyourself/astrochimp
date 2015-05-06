using System;
using UIKit;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Foundation;

namespace AncestorCloud.Touch
{
	public class RelationshipMatchTableSource : MvxTableViewSource {

		string[] tableItems;
		string cellIdentifier = "TableCell";

//		public RelationshipMatchTableSource (string[] items) 
//		{
//			tableItems = items;
//		}

		public RelationshipMatchTableSource(UITableView tableView): base(tableView)
		{
			tableView.RegisterClassForCellReuse(typeof(UITableViewCell), cellIdentifier);
			//tableView.RegisterNibForCellReuse(UINib.FromName("FbFamilyCell", NSBundle.MainBundle),FbFamilyCell.Key);

		}


		protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
		{
			//			FbFamilyCell cell = (FbFamilyCell)tableView.DequeueReusableCell (cellIdentifier);
			//			// if there are no cells to reuse, create a new one
			//			if (cell == null)
			//				cell =  FbFamilyCell.Create() ;
			//
			//			return cell;

			return tableView.DequeueReusableCell(cellIdentifier, indexPath);
		}
	}

}

