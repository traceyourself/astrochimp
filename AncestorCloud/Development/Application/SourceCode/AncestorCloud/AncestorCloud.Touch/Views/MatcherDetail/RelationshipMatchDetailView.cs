
using System;

using Foundation;
using UIKit;

namespace AncestorCloud.Touch
{
	public partial class RelationshipMatchDetailView : BaseViewController
	{

		UITableView table;

		public RelationshipMatchDetailView () : base ("RelationshipMatchDetailView", null)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			CreateRelationShipTable();
			
			// Perform any additional setup after loading the view, typically from a nib.
		}


		#region Relationship table

		private void CreateRelationShipTable()
		{
			this.NavigationController.NavigationBar.TintColor=UIColor.FromRGB(255,255,255);

			table = RelationshipMatchTable;//new UITableView(View.Bounds); // defaults to Plain style
			string[] tableItems = new string[] {"1972"+"   "+"Glenneth Girtrude Gates"+"  " +" 06","1956"+"   "+"Henry Wright Gates"+"          " +" 07","1925"+"   "+"Glenneth Girtrude Gates"+"  " +" 08","1907"+"   "+"Henry Wright Gates"+"          " +" 09","1925"+"   "+"Henry Wright Gates"+"          " +" 10","1907"+"   "+"Glenneth Girtrude Gates"+"  " +" 11","1925"+"   "+"Henry Wright Gates"+"          " +" 12","1921"+"   "+"Glenneth Girtrude Gates"+"  " +" 13","1972"+"   "+"Henry Wright Gates"+"          " +" 14","1956"+"   "+"Glenneth Girtrude Gates"+"  " +" 15"};
			table.Source = new RelationshipMatchTableSource(tableItems);
			this.NavigationItem.TitleView = new MyTitleView (this.Title);
			//Add (table);
		}

		#endregion
	}
}

