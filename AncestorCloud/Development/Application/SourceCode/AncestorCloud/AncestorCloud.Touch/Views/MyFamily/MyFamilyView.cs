using System;
using Foundation;
using UIKit;
using AncestorCloud.Shared.ViewModels;
using System.Collections.Generic;
using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.CrossCore;
using System.Linq;
using AncestorCloud.Shared;
using Cirrious.MvvmCross.Binding.BindingContext;

namespace AncestorCloud.Touch
{
	public partial class MyFamilyView : BaseViewController
	{

		UILabel cell;

		private MvxSubscriptionToken navigationMenuToken;

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
			AddEvents ();

			// Perform any additional setup after loading the view, typically from a nib.
		}

		#region Binding

		private void CreateMyFamilyTable()
		{
			
			List<TableItem> data = CreateTableItems(ViewModel.FamilyList);

			ViewModel.TableDataList = data;


			var source = new MyFamilyTableSource (myFamilyTable);
			myFamilyTable.Source = source;

			var set = this.CreateBindingSet<MyFamilyView , MyFamilyViewModel> ();
			set.Bind (source).To (vm => vm.TableDataList);
			set.Apply ();

		}

		#endregion


		#region list Filteration

		public List<TableItem> CreateTableItems(List<People> mainList)
		{
			List<TableItem> resultList = new List<TableItem> ();

			List<People> siblingList = new List<People> ();
			List<People> parentList = new List<People> ();
			List<People> grandParentList = new List<People> ();
			List<People> greatGrandParentList = new List<People> ();


			for(int i=0;i<mainList.Count;i++){
				People item = mainList[i];
				string relation = item.Relation;


				if (relation.Contains (StringConstants.BROTHER_COMPARISON) || relation.Contains (StringConstants.SISTER_COMPARISON))
				{
					siblingList.Add (item);
				}

				if (relation.Contains (StringConstants.FATHER_COMPARISON) || relation.Contains (StringConstants.MOTHER_COMPARISON))
				{
					parentList.Add (item);
				}
				if (relation.Contains (StringConstants.GRANDFATHER_COMPARISON) || relation.Contains (StringConstants.GRANDMOTHER_COMPARISON))
				{
					grandParentList.Add (item);
				}
				if (relation.Contains (StringConstants.GREATGRANDFATHER_COMPARISON) || relation.Contains (StringConstants.GREATGRANDMOTHER_COMPARISON))
				{
					greatGrandParentList.Add (item);
				}


		 }


			TableItem siblingData = new TableItem ();
			siblingData.SectionHeader = "Siblings";
			siblingData.SectionFooter = "Sibling";
			siblingData.DataItems = siblingList;

			resultList.Add (siblingData);

			TableItem parentsData= new TableItem ();
			parentsData.SectionHeader = "Parents";
			parentsData.SectionFooter = "Parents";
			parentsData.DataItems = parentList;

			resultList.Add (parentsData);

			TableItem grandParentData= new TableItem ();
			grandParentData.SectionHeader = "Grand Parents";
			grandParentData.SectionFooter = "GrandParents";
			grandParentData.DataItems = grandParentList;

			resultList.Add (grandParentData);

			TableItem greatGrandParentData= new TableItem ();
			greatGrandParentData.SectionHeader = "Great GrandParents";
			greatGrandParentData.SectionFooter = "GreatGrandParents";
			greatGrandParentData.DataItems = greatGrandParentList;

			resultList.Add (greatGrandParentData);

		
			return resultList;
		}

		#endregion

		public void SetFamilyItem()
		{
			this.Title = "";
			UINavigationBar.Appearance.SetTitleTextAttributes (new UITextAttributes ()
				{ TextColor = UIColor.FromRGB (255,255,255) });
			this.NavigationController.NavigationBar.BarTintColor = UIColor.FromRGB (178, 45, 116);
			//this.NavigationItem.SetHidesBackButton (true, false);
			this.NavigationController.NavigationBar.TintColor=UIColor.FromRGB(255,255,255);
			//this.NavigationItem.TitleView = new MyTitleView ();
			this.NavigationController.NavigationBarHidden = false;


		}

		private void AddEvents ()
		{
			(myFamilyTable.Source as MyFamilyTableSource).FooterClickedDelegate += ShowAddParents;
			var _messenger = Mvx.Resolve<IMvxMessenger>();
			navigationMenuToken = _messenger.SubscribeOnMainThread<MyTableCellTappedMessage>(message => this.ShowEditFamily(this));

		}

		public void ShowAddParents(object obj)
		{
			ViewModel.ShowAddParents ();
		}

		public void ShowEditFamily(object obj)
		{
			EditFamilyView editFamily = new EditFamilyView ();
			UIWindow window = UIApplication.SharedApplication.KeyWindow;
			window.AddSubview (editFamily.View);
		}

		public override void ViewWillDisappear (bool animated)
		{
			if (!NavigationController.ViewControllers.Contains (this)) {
				var messenger = Mvx.Resolve<IMvxMessenger> ();
				messenger.Publish (new NavigationBarHiddenMessage (this, true)); 

			}
			base.ViewWillDisappear (animated);
		}

	}
}

