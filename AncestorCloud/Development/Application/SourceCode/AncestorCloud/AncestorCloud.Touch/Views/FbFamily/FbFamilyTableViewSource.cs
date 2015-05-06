using System;
using UIKit;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Foundation;
using AncestorCloud.Shared;
using System.Collections.Generic;
using UIKit;

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
			//tableView.RegisterClassForCellReuse(typeof(FbFamilyCell), FbFamilyCell.Key);
			tableView.RegisterNibForCellReuse(UINib.FromName("FbFamilyCell", NSBundle.MainBundle),
				FbFamilyCell.Key);

		}

		protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
		{
//			FbFamilyCell cell = (FbFamilyCell)tableView.DequeueReusableCell (cellIdentifier);
//			// if there are no cells to reuse, create a new one
//			if (cell == null)
//				cell =  FbFamilyCell.Create() ;
//
//			return cell;

			FbFamilyCell cell = (FbFamilyCell) tableView.DequeueReusableCell(FbFamilyCell.Key, indexPath);

			cell.BindData ();

			return cell;
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

	public class FbFamilyTableViewDelegate : UITableViewDelegate
	{

		 List<People> peopleList;

		public FbFamilyTableViewDelegate(List<People> people)
		{
        	peopleList=people;

		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			People member = peopleList [indexPath.Row];

			member.IsSelected = !member.IsSelected;

			tableView.ReloadData ();
		}
	}
}

