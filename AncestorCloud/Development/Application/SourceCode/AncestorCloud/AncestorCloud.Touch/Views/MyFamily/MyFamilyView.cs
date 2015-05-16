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
using System.Drawing;
using CoreGraphics;

namespace AncestorCloud.Touch
{
	public partial class MyFamilyView : BaseViewController
	{
		private MvxSubscriptionToken navigationMenuToken;

		private MvxSubscriptionToken ReloadViewToken;

		EditFamilyView editFamily;
		IMvxMessenger _messenger = Mvx.Resolve<IMvxMessenger>();

		#region View Life Cycle Methods

		public MyFamilyView () : base ("MyFamilyView", null)
		{
		}
		public new MyFamilyViewModel ViewModel
		{
			get { return base.ViewModel as MyFamilyViewModel; }
			set { base.ViewModel = value; }
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
//			CreateMyFamilyTable ();
			SetFamilyItem ();
			//AddEvents ();

			OnKeyboardChanged += (object sender, OnKeyboardChangedArgs e) => {

				if(editFamily != null)
					editFamily.OnKeyboardChanged(sender,e);
			};
			// Perform any additional setup after loading the view, typically from a nib.
		}

		public override void ViewWillDisappear (bool animated)
		{
			if (!NavigationController.ViewControllers.Contains (this)) {
				var messenger = Mvx.Resolve<IMvxMessenger> ();
				messenger.Publish (new NavigationBarHiddenMessage (this, true)); 

			}

			RemoveMessengers ();
			base.ViewWillDisappear (animated);
		}

		public override void ViewWillUnload ()
		{
			RemoveMessengers ();

			base.ViewWillUnload ();
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			ReloadView ();
		}

		private void ReloadView()
		{
			RemoveMessengers ();
			ViewModel.GetFbFamilyData ();
			CreateMyFamilyTable ();
			AddEvents ();
		}

		#endregion

		#region Binding

		private void CreateMyFamilyTable()
		{
			List<TableItem> data = CreateTableItems(ViewModel.FamilyList);
			ViewModel.TableDataList = data;
			var source = new MyFamilyTableSource (myFamilyTable);
			myFamilyTable.Source = source;
			var set = this.CreateBindingSet<MyFamilyView , MyFamilyViewModel> ();
			set.Bind (source).To (vm => vm.TableDataList).TwoWay();
			set.Apply ();
		}

		#endregion

		public void SetFamilyItem()
		{
			this.Title= "My Family";
			UINavigationBar.Appearance.SetTitleTextAttributes (new UITextAttributes ()
				{ TextColor = UIColor.FromRGB (255,255,255) });
			this.NavigationController.NavigationBar.BarTintColor = UIColor.FromRGB (64,172,176);
			//this.NavigationItem.SetHidesBackButton (true, false);
			this.NavigationController.NavigationBar.TintColor=UIColor.FromRGB(255,255,255);
			float width = (float)UIScreen.MainScreen.ApplicationFrame.Size.Width;
			if (width <= 320f) {
				this.NavigationItem.TitleView = new MyTitleView (this.Title,new RectangleF(0,0,100,20));

			} else {
				this.NavigationItem.TitleView = new MyTitleView (this.Title,new RectangleF(0,0,130,20));
			}
			this.NavigationController.NavigationBarHidden = false;
		}

		private void AddEvents ()
		{
			(myFamilyTable.Source as MyFamilyTableSource).FooterClickedDelegate += ShowAddParents;
			navigationMenuToken = _messenger.SubscribeOnMainThread<MyTableCellTappedMessage>(message => this.ShowEditFamily(message.FamilyMember));
			ReloadViewToken = _messenger.SubscribeOnMainThread<MyFamilyReloadMessage>(Message => this.ReloadView());
		}

		void RemoveMessengers()
		{
			if(navigationMenuToken != null)
				_messenger.Unsubscribe<MyTableCellTappedMessage> (navigationMenuToken);
			if(ReloadViewToken != null)
				_messenger.Unsubscribe<MyFamilyReloadMessage> (ReloadViewToken);

			if(myFamilyTable.Source != null)
				(myFamilyTable.Source as MyFamilyTableSource).FooterClickedDelegate -= ShowAddParents;
//			_messenger.Unsubscribe<ToggleFlyoutMenuMessage> (navigationMenuToggleToken);
//			_messenger.Unsubscribe<NavigationBarHiddenMessage> (navigationBarHiddenToken);


		}

		public void ShowAddParents(object obj)
		{
			TableItem item = obj as TableItem;

			ViewModel.ShowAddParents (item.SectionFooter);
		}
			
		partial void CrossButtonTaped (NSObject sender)
		{
			ViewModel.ShowHelpViewModel();
		}


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

				if (relation.Contains (StringConstants.BROTHER_COMPARISON) || relation.Contains (StringConstants.SISTER_COMPARISON) || relation.Contains ("Sibling"))
				{
					siblingList.Add (item);
				}

				if (relation.Contains (StringConstants.FATHER_COMPARISON) || relation.Contains (StringConstants.MOTHER_COMPARISON) || relation.Contains ("Parent") )
				{
					parentList.Add (item);
				}
				if (relation.Contains (StringConstants.GRANDFATHER_COMPARISON) || relation.Contains (StringConstants.GRANDMOTHER_COMPARISON) || relation.Contains ("Grandparent"))
				{
					grandParentList.Add (item);
				}
				if (relation.Contains (StringConstants.GREATGRANDFATHER_COMPARISON) || relation.Contains (StringConstants.GREATGRANDMOTHER_COMPARISON) || relation.Contains ("GreatGrandparent"))
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
			parentsData.SectionFooter = "Parent";
			parentsData.DataItems = parentList;

			resultList.Add (parentsData);

			TableItem grandParentData= new TableItem ();
			grandParentData.SectionHeader = "Grandparents";
			grandParentData.SectionFooter = "Grandparent";
			grandParentData.DataItems = grandParentList;

			resultList.Add (grandParentData);

			TableItem greatGrandParentData= new TableItem ();
			greatGrandParentData.SectionHeader = "Great Grandparents";
			greatGrandParentData.SectionFooter = "GreatGrandparent";
			greatGrandParentData.DataItems = greatGrandParentList;

			resultList.Add (greatGrandParentData);

			return resultList;
		}

		#endregion


		#region EditFamilyView Methods

		public void ShowEditFamily(People member)
		{
			//if(editFamily == null)
			editFamily = new EditFamilyView ();
			
			editFamily.FamilyMember = member;
			editFamily.SaveButtonTappedClickedDelegate += SaveEditedFamilyDetails;
			//editFamily.View.Frame = this.View.Frame;

			CGRect frame = editFamily.View.Frame;
			frame.Width = this.View.Frame.Size.Width;
			editFamily.View.Frame = frame;

			UIWindow window = UIApplication.SharedApplication.KeyWindow;
			window.AddSubview (editFamily.View);
		}



		public void SaveEditedFamilyDetails(object member)
		{
			ViewModel.FamilyMember = member as People;
			ViewModel.EditPerson ();
			//ReloadView ();
		}
	
		#endregion

	}
}

