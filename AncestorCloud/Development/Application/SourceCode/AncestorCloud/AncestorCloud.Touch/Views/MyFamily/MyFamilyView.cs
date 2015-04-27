
using System;

using Foundation;
using UIKit;
using AncestorCloud.Shared.ViewModels;
using System.Collections.Generic;

namespace AncestorCloud.Touch
{
	public partial class MyFamilyView : BaseViewController
	{

		UITableView table;
		UILabel cell;

		public MyFamilyView () : base ("MyFamilyView", null)
		{
		}
		public new MyFamilyViewModel ViewModel
		{
			get { return base.ViewModel as MyFamilyViewModel; }
			set { base.ViewModel = value; }
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
			CreateMyFamilyTable ();
			SetFamilyItem ();
			
			// Perform any additional setup after loading the view, typically from a nib.
		}

		private void CreateMyFamilyTable(){
			table = myFamilyTable;//new UITableView(View.Bounds); // defaults to Plain style
			//string[] tableItems = new string[] {"Medisomes Eames(sister)"+"\n"+"1952","Robert Eames(Father)","Mary Herzog Eames(Mother)","Merrill Eames(Grandfather)" ," + Add family"};
			List<TableItem> data = CreateTableItems();

			table.DataSource = new MyFamilyTableSource(data);
			table.Delegate = new MyFamilyTableDelegate (data);


		}

		protected List<TableItem> CreateTableItems ()
		{
			List<TableItem> dataList = new List<TableItem>();

			List<MyFamilySectionDataItem> childList = new List<MyFamilySectionDataItem>();
			MyFamilySectionDataItem childItem = new MyFamilySectionDataItem ("Kevin Stevens","1958");
			childList.Add(childItem);
			TableItem data = new TableItem (" Siblings","Add Siblings",childList);
			dataList.Add (data);


			childList = new List<MyFamilySectionDataItem>();
			childList.Add(childItem);
			childList.Add(childItem);
			data = new TableItem (" Parents","Add Parents",childList);
			dataList.Add (data);


			childList = new List<MyFamilySectionDataItem>();
			childList.Add(childItem);
			childList.Add(childItem);
			childList.Add(childItem);
			data = new TableItem (" Grandparents","Add Grandparents",childList);
			dataList.Add (data);

			childList = new List<MyFamilySectionDataItem>();
			data = new TableItem (" Great Grandparents","Add Great Grandparents",childList);
			childList.Add(childItem);
			dataList.Add (data);



			return dataList;
		}

		public void SetFamilyItem()
		{

			this.Title = "My Family";
			UINavigationBar.Appearance.SetTitleTextAttributes (new UITextAttributes ()
				{ TextColor = UIColor.FromRGB (255,255,255) });
			this.NavigationController.NavigationBar.BarTintColor = UIColor.FromRGB (178, 45, 116);
			//this.NavigationItem.SetHidesBackButton (true, false);
			this.NavigationController.NavigationBar.TintColor=UIColor.FromRGB(255,255,255);
			//this.NavigationItem.TitleView = new MyTitleView ();
			//this.NavigationController.NavigationBarHidden = true;





//			this.NavigationItem.SetLeftBarButtonItem(
//				new UIBarButtonItem(UIImage.FromFile ("cross.png")
//					, UIBarButtonItemStyle.Plain
//					, (sender,args) => {
//						ViewModel.Close();
//
//					})
//				, true);

		}
	}
}

