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
using AncestorCloud.Core;

namespace AncestorCloud.Touch
{
	public partial class MyFamilyView : BaseViewController
	{
		private MvxSubscriptionToken navigationMenuToken;

		private MvxSubscriptionToken ReloadViewToken;

		private MvxSubscriptionToken LoadViewToken;

		private MvxSubscriptionToken PercentageToken;

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


			//ViewModel.FetchPercentageComplete ();
			ViewModel.GetFamilyMembersFromServer();

			OnKeyboardChanged += (object sender, OnKeyboardChangedArgs e) => {

				if(editFamily != null)
					editFamily.OnKeyboardChanged(sender,e);
			};

			SetSegmentControlUI ();
			SegmentControlTapped (null);
			// Perform any additional setup after loading the view, typically from a nib.
		}

//		public override void ViewWillDisappear (bool animated)
//		{
//			if (!NavigationController.ViewControllers.Contains (this)) {
//				var messenger = Mvx.Resolve<IMvxMessenger> ();
//				messenger.Publish (new NavigationBarHiddenMessage (this, true)); 
//
//			}
//
//			RemoveMessengers ();
//			base.ViewWillDisappear (animated);
//		}
		public override void ViewWillDisappear (bool animated)
		{
			if (NavigationController == null) {
				base.ViewWillDisappear (animated);
				return;
			}
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
			this.NavigationController.NavigationBarHidden = false;
			base.ViewWillAppear (animated);

			App.controllerTypeRef = ControllerType.Secondary;

			ReloadView ();
		}

		private void ReloadView()
		{
			RemoveMessengers ();
			ViewModel.GetFbFamilyData ();
			CreateMyFamilyTable ();
			AddEvents ();

		}

		public void LoadView()
		{
			CreateMyFamilyTable ();

		}

		#endregion

		#region Binding
		/*
		 * Changed the flow of data filtering - due to new UI flow
		 * Just modified / Not re-written
		 * 
		*/
		private void CreateMyFamilyTable()
		{
			List<TableItem> data = CreateParentTableItems(ViewModel.FamilyList);
			List<TableItem> gdata = CreateGParentTableItems(ViewModel.FamilyList);
			List<TableItem> ggdata = CreateGGParentTableItems(ViewModel.FamilyList);

			ViewModel.ParentsTableDataList = data;
			ViewModel.GParentsTableDataList = gdata;
			ViewModel.GGParentsTableDataList = ggdata;

			var source = new MyFamilyTableSource (myFamilyTable,FamilyType.Parent);
			var gsource = new MyFamilyTableSource (grandParentTableViewObj,FamilyType.GParent);
			var ggsource = new MyFamilyTableSource (greatGrandParentTableVIewObj,FamilyType.GGParent);

			myFamilyTable.Source = source;
			grandParentTableViewObj.Source = gsource;
			greatGrandParentTableVIewObj.Source = ggsource;

			var set = this.CreateBindingSet<MyFamilyView , MyFamilyViewModel> ();
			set.Bind (source).To (vm => vm.ParentsTableDataList).TwoWay();
			set.Bind (gsource).To (vm => vm.GParentsTableDataList).TwoWay();
			set.Bind (ggsource).To (vm => vm.GGParentsTableDataList).TwoWay();
			//set.Bind (PercentageLabel).To (vm => vm._PercentageComplete);
			set.Apply ();
		}

		#endregion

		public void SetFamilyItem()
		{
			this.Title = Utility.LocalisedBundle ().LocalizedString ("MyFamilyText", "");
			UINavigationBar.Appearance.SetTitleTextAttributes (new UITextAttributes ()
				{ TextColor =Themes.TitleTextColor() });
			this.NavigationController.NavigationBar.BarTintColor = Themes.NavBarTintColor();
			//this.NavigationItem.SetHidesBackButton (true, false);
			this.NavigationController.NavigationBar.TintColor=Themes.TitleTextColor();
			float width = (float)UIScreen.MainScreen.ApplicationFrame.Size.Width;
			if (width <= 320f) {
				this.NavigationItem.TitleView = new MyTitleView (this.Title,new RectangleF(0,0,100,20));

			} else {
				this.NavigationItem.TitleView = new MyTitleView (this.Title,new RectangleF(0,0,130,20));
			}

		}

		private void AddEvents ()
		{
			
			(myFamilyTable.Source as MyFamilyTableSource).FooterClickedDelegate += ShowAddParents;
			(grandParentTableViewObj.Source as MyFamilyTableSource).FooterClickedDelegate += ShowAddParents;
			(greatGrandParentTableVIewObj.Source as MyFamilyTableSource).FooterClickedDelegate += ShowAddParents;
			navigationMenuToken = _messenger.SubscribeOnMainThread<MyTableCellTappedMessage>(message => this.ShowEditFamily(message.FamilyMember));
			ReloadViewToken = _messenger.SubscribeOnMainThread<MyFamilyReloadMessage>(Message => this.ReloadView());
			LoadViewToken = _messenger.SubscribeOnMainThread<MyFamilyLoadViewMessage>(Message => this.LoadView());
			PercentageToken = _messenger.SubscribeOnMainThread<PercentageMessage>(Message => this.LoadPercent());
		}

		void RemoveMessengers()
		{
			if(navigationMenuToken != null)
				_messenger.Unsubscribe<MyTableCellTappedMessage> (navigationMenuToken);
			
			if(ReloadViewToken != null)
				_messenger.Unsubscribe<MyFamilyReloadMessage> (ReloadViewToken);

			if(LoadViewToken != null)
				_messenger.Unsubscribe<MyFamilyLoadViewMessage> (LoadViewToken);

			if(myFamilyTable.Source != null)
				(myFamilyTable.Source as MyFamilyTableSource).FooterClickedDelegate -= ShowAddParents;
			
			if(grandParentTableViewObj.Source != null)
				(grandParentTableViewObj.Source as MyFamilyTableSource).FooterClickedDelegate -= ShowAddParents;
			
			if(greatGrandParentTableVIewObj.Source != null)
				(greatGrandParentTableVIewObj.Source as MyFamilyTableSource).FooterClickedDelegate -= ShowAddParents;
			
//			_messenger.Unsubscribe<ToggleFlyoutMenuMessage> (navigationMenuToggleToken);
//			_messenger.Unsubscribe<NavigationBarHiddenMessage> (navigationBarHiddenToken);
			if(PercentageToken != null)
			_messenger.Unsubscribe<PercentageMessage> (PercentageToken);


		}

		public void ShowAddParents(object obj)
		{
			TableItem item = obj as TableItem;
			//ViewModel.ShowAddParents (item.SectionFooter);
			ViewModel.CheckIfCanAddPerson(item.SectionFooter);
		}
			
		partial void CrossButtonTaped (NSObject sender)
		{
			ViewModel.ShowHelpViewModel();
		}


		#region list Filteration

		public List<TableItem> CreateParentTableItems(List<People> mainList)
		{
			List<TableItem> resultList = new List<TableItem> ();

			foreach(People p in mainList){
				Mvx.Trace (p.Name+" :: "+p.Relation);
			}

			List<People> siblingList = new List<People> ();
			List<People> parentList = new List<People> ();
			List<People> grandParentList = new List<People> ();
			List<People> greatGrandParentList = new List<People> ();


			for(int i=0;i<mainList.Count;i++){
				People item = mainList[i];
				string relation = item.Relation;

				if (relation.Equals (StringConstants.BROTHER_COMPARISON) || relation.Equals (StringConstants.SISTER_COMPARISON) || relation.Equals ("Sibling"))
				{
					siblingList.Add (item);
				}

				if (relation.Equals (StringConstants.FATHER_COMPARISON) || relation.Equals (StringConstants.MOTHER_COMPARISON) || relation.Equals ("Parent") )
				{
					parentList.Add (item);
				}
//				if (relation.Equals (StringConstants.GRANDFATHER_COMPARISON) || relation.Equals  (StringConstants.GRANDMOTHER_COMPARISON) || relation.Equals ("Grandparent")|| relation.Equals (AppConstant.GrandParent_comparison))
//				{
//					grandParentList.Add (item);
//				}
//				if (relation.Equals (StringConstants.GREATGRANDFATHER_COMPARISON) || relation.Equals (StringConstants.GREATGRANDMOTHER_COMPARISON) || relation.Equals ("Great Grandparent") || relation.Equals (AppConstant.GreatGrandParent_comparison))
//				{
//					greatGrandParentList.Add (item);
//				}


			}

			TableItem parentsData= new TableItem ();
			parentsData.SectionHeader = Utility.LocalisedBundle ().LocalizedString("ParentSectionHeader","");
			parentsData.SectionFooter = Utility.LocalisedBundle ().LocalizedString("ParentSectionFooter","");
			parentsData.DataItems = parentList;


			resultList.Add (parentsData);


			TableItem siblingData = new TableItem ();
			siblingData.SectionHeader = Utility.LocalisedBundle ().LocalizedString("SiblingSectionHeader","");
			siblingData.SectionFooter = Utility.LocalisedBundle ().LocalizedString("SiblingSectionFooter","");
			siblingData.DataItems = siblingList;


			resultList.Add (siblingData);



//			TableItem grandParentData= new TableItem ();
//			grandParentData.SectionHeader = Utility.LocalisedBundle ().LocalizedString("GrandparentSectionHeader","");
//			grandParentData.SectionFooter = Utility.LocalisedBundle ().LocalizedString("GrandparentSectionFooter","");
//			grandParentData.DataItems = grandParentList;
//
//
//			resultList.Add (grandParentData);
//
//			TableItem greatGrandParentData= new TableItem ();
//			greatGrandParentData.SectionHeader = Utility.LocalisedBundle ().LocalizedString("GreatGrandparentSectionHeader","");
//			greatGrandParentData.SectionFooter = Utility.LocalisedBundle ().LocalizedString("GreatGrandparentSectionFooter","");
//			greatGrandParentData.DataItems = greatGrandParentList;
//
//			resultList.Add (greatGrandParentData);

			return resultList;
		}

		public List<TableItem> CreateGParentTableItems(List<People> mainList)
		{
			List<TableItem> resultList = new List<TableItem> ();

			foreach(People p in mainList){
				Mvx.Trace (p.Name+" :: "+p.Relation);
			}

//			List<People> siblingList = new List<People> ();
//			List<People> parentList = new List<People> ();
			List<People> grandParentList = new List<People> ();
			//List<People> greatGrandParentList = new List<People> ();


			for(int i=0;i<mainList.Count;i++){
				People item = mainList[i];
				string relation = item.Relation;

//				if (relation.Equals (StringConstants.BROTHER_COMPARISON) || relation.Equals (StringConstants.SISTER_COMPARISON) || relation.Equals ("Sibling"))
//				{
//					siblingList.Add (item);
//				}
//
//				if (relation.Equals (StringConstants.FATHER_COMPARISON) || relation.Equals (StringConstants.MOTHER_COMPARISON) || relation.Equals ("Parent") )
//				{
//					parentList.Add (item);
//				}
				if (relation.Equals (StringConstants.GRANDFATHER_COMPARISON) || relation.Equals  (StringConstants.GRANDMOTHER_COMPARISON) || relation.Equals ("Grandparent")|| relation.Equals (AppConstant.GrandParent_comparison))
				{
					grandParentList.Add (item);
				}
				//				if (relation.Equals (StringConstants.GREATGRANDFATHER_COMPARISON) || relation.Equals (StringConstants.GREATGRANDMOTHER_COMPARISON) || relation.Equals ("Great Grandparent") || relation.Equals (AppConstant.GreatGrandParent_comparison))
				//				{
				//					greatGrandParentList.Add (item);
				//				}


			}
				
			TableItem grandParentData= new TableItem ();
			grandParentData.SectionHeader = Utility.LocalisedBundle ().LocalizedString("GrandparentSectionHeader","");
			grandParentData.SectionFooter = Utility.LocalisedBundle ().LocalizedString("GrandparentSectionFooter","");
			grandParentData.DataItems = grandParentList;
			
			
			resultList.Add (grandParentData);
			//
			//			TableItem greatGrandParentData= new TableItem ();
			//			greatGrandParentData.SectionHeader = Utility.LocalisedBundle ().LocalizedString("GreatGrandparentSectionHeader","");
			//			greatGrandParentData.SectionFooter = Utility.LocalisedBundle ().LocalizedString("GreatGrandparentSectionFooter","");
			//			greatGrandParentData.DataItems = greatGrandParentList;
			//
			//			resultList.Add (greatGrandParentData);

			return resultList;
		}

		public List<TableItem> CreateGGParentTableItems(List<People> mainList)
		{
			List<TableItem> resultList = new List<TableItem> ();

			foreach(People p in mainList){
				Mvx.Trace (p.Name+" :: "+p.Relation);
			}
				
			List<People> greatGrandParentList = new List<People> ();


			for(int i=0;i<mainList.Count;i++){
				People item = mainList[i];
				string relation = item.Relation;


				if (relation.Equals (StringConstants.GREATGRANDFATHER_COMPARISON) || relation.Equals (StringConstants.GREATGRANDMOTHER_COMPARISON) || relation.Equals ("Great Grandparent") || relation.Equals (AppConstant.GreatGrandParent_comparison))
				{
					greatGrandParentList.Add (item);
				}


			}

				TableItem greatGrandParentData= new TableItem ();
				greatGrandParentData.SectionHeader = Utility.LocalisedBundle ().LocalizedString("GreatGrandparentSectionHeader","");
				greatGrandParentData.SectionFooter = Utility.LocalisedBundle ().LocalizedString("GreatGrandparentSectionFooter","");
				greatGrandParentData.DataItems = greatGrandParentList;
			
				resultList.Add (greatGrandParentData);

			return resultList;
		}

		#endregion


		#region Segment Methods

		void SetSegmentControlUI()
		{
			this.segmentControlObj.TintColor = UIColor.Clear;
			this.segmentControlObj.SetTitleTextAttributes (new UITextAttributes ()
				{ TextColor = UIColor.White ,Font = UIFont.BoldSystemFontOfSize(14)},UIControlState.Normal);

			this.segmentControlObj.SetTitleTextAttributes (new UITextAttributes ()
				{ TextColor = UIColor.FromRGB(42,206,215) ,Font = UIFont.BoldSystemFontOfSize(14)},UIControlState.Selected);

			//[[UISegmentedControl appearance] setTitleTextAttributes:@{NSForegroundColorAttributeName:[UIColor redColor]} forState:UIControlStateNormal];
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

		public void LoadPercent()
		{
			PercentageLabel.Text = ViewModel._PercentageComplete+ Utility.LocalisedBundle ().LocalizedString("MatchingText","");;
		}


		#region NEW TAB METHODS

		partial void SegmentControlTapped (NSObject sender)
		{
			SetTabFooterImage((int)segmentControlObj.SelectedSegment);

			EnableTabeleViews((int)segmentControlObj.SelectedSegment);

			switch(segmentControlObj.SelectedSegment)
			{
			case 0:
				
				break;
			case 1:
				break;
			case 2:
				break;
			}
		}


		void SetTabFooterImage(int index)
		{
			switch(segmentControlObj.SelectedSegment)
			{
			case 0:
				firstTabImageView.Hidden = false;
				secondTabImageView.Hidden = true;
				thirdTabImageVIew.Hidden = true;
				break;
			case 1:
				firstTabImageView.Hidden = true;
				secondTabImageView.Hidden = false;
				thirdTabImageVIew.Hidden = true;
				break;
			case 2:
				firstTabImageView.Hidden = true;
				secondTabImageView.Hidden = true;
				thirdTabImageVIew.Hidden = false;
				break;
			}
		}

		void EnableTabeleViews(int index)
		{
			switch(segmentControlObj.SelectedSegment)
			{
			case 0:
				myFamilyTable.Hidden = false;
				grandParentTableViewObj.Hidden = true;
				greatGrandParentTableVIewObj.Hidden = true;
				break;
			case 1:
				myFamilyTable.Hidden = true;
				grandParentTableViewObj.Hidden = false;
				greatGrandParentTableVIewObj.Hidden = true;
				break;
			case 2:
				myFamilyTable.Hidden = true;
				grandParentTableViewObj.Hidden = true;
				greatGrandParentTableVIewObj.Hidden = false;
				break;
			}
		}

		#endregion


	}
}

