using System;
using UIKit;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Foundation;

namespace AncestorCloud.Touch
{
	public class FbFamilyTableViewSource : MvxTableViewSource
	{

		readonly string cellIdentifier = "FbFamilyCell";

//		public FbFamilyTableViewSource ()
//		{
//		}

		public FbFamilyTableViewSource(UITableView tableView): base(tableView)
		{
			tableView.RegisterClassForCellReuse(typeof(FbFamilyCell), FbFamilyCell.Key);

		}

		public override nint RowsInSection (UITableView tableView, nint section)
		{

			return 5;
		}



		protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
		{
//			FbFamilyCell cell = (FbFamilyCell)tableView.DequeueReusableCell (cellIdentifier);
//			// if there are no cells to reuse, create a new one
//			if (cell == null)
//				cell =  FbFamilyCell.Create() ;
//
//			return cell;

			return tableView.DequeueReusableCell(FbFamilyCell.Key, indexPath);
		}

//		public override UITableViewCell GetCell (UITableView tableView, Foundation.NSIndexPath indexPath)
//		{
//			FbFamilyCell cell = (FbFamilyCell)tableView.DequeueReusableCell (cellIdentifier);
//			// if there are no cells to reuse, create a new one
//			if (cell == null)
//				cell =  FbFamilyCell.Create() ;
//
//			return cell;
//
//		}
	}
}

