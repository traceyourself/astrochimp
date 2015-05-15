using System;
using System.Collections.Generic;
using Cirrious.MvvmCross.ViewModels;
using System.Windows.Input;
using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.CrossCore;

namespace AncestorCloud.Shared.ViewModels
{
	public class FlyOutViewModel:BaseViewModel
	{
		private MvxSubscriptionToken navigationMenuToggleToken;
		private MvxSubscriptionToken changeFlyoutToken;
		private  bool IsFaceBookLogin{ get; set;}
	
		public void Init(DetailParameters parameter)
		{
			this.IsFaceBookLogin = parameter.IsFBLogin;
			this.SetItemList (IsFaceBookLogin);
		}
		public FlyOutViewModel ()
		{

//			var _messenger = Mvx.Resolve<IMvxMessenger>();
//			navigationMenuToggleToken = _messenger.SubscribeOnMainThread<Message>(message => this.Close(this));

			var _flyoutMessenger = Mvx.Resolve<IMvxMessenger>();
			changeFlyoutToken = _flyoutMessenger.SubscribeOnMainThread<ChangeFlyoutFlowMessage>(message => this.ReloadMenuList(message.ChangeFlyoutFlow));

			var _messenger = Mvx.Resolve<IMvxMessenger>();
			navigationMenuToggleToken = _messenger.SubscribeOnMainThread<FlyOutCloseMessage>(message => this.CloseFlyoutMenu());

		}

		public enum Section
		{
			Unknown,
			MyFamily,
			Matcher,
			ResearchHelp,
		}

		//Move into Constant class

		private List<MenuViewModel> menuItems;
		public List<MenuViewModel> MenuItems
		{
			get { return this.menuItems; }
			set { this.menuItems = value; this.RaisePropertyChanged(() => this.MenuItems); }
		}

		private MvxCommand<MenuViewModel> m_SelectMenuItemCommand;

		public ICommand SelectMenuItemCommand
		{
			get
			{
				return this.m_SelectMenuItemCommand ?? (this.m_SelectMenuItemCommand = new MvxCommand<MenuViewModel>(this.ExecuteSelectMenuItemCommand));
			}
		}

		private void ExecuteSelectMenuItemCommand(MenuViewModel item)
		{
			//navigate if we have to, pass the id so we can grab from cache... or not
			switch (item.Section)
			{

			case Section.MyFamily:
				this.ShowViewModel<FamilyViewModel>();
				break;
			case Section.Matcher:
				this.ShowViewModel<FamilyViewModel>();
				break;                
			}

		}

		#region Helper Method

		private void SetItemList(bool boolValue)
		{
			this.IsFaceBookLogin = boolValue;

			if (boolValue) {
				this.MenuItems = new List<MenuViewModel>
				{  

					new MenuViewModel
					{
						Section = Section.MyFamily,
						Title = "My Family",
						Image = "myfamily_icon.png",
						ViewModelType = typeof(FbFamilyViewModel),
					},
					new MenuViewModel
					{
						Section = Section.Matcher,
						Title = "Cousin Match",
						Image = "c.png",
						ViewModelType = typeof(MatchViewModel)
					},   
					new MenuViewModel
					{
						Section = Section.MyFamily,
						Title = "Research Help",
						Image = "search_icon.png",
						ViewModelType = typeof(ResearchHelpViewModel)
					},
					new MenuViewModel
					{
						Section = Section.Matcher,
						Title = "Log Out",
						Image = "logout_icon.png",
						ViewModelType = typeof(HomePageViewModel),

					},
					new MenuViewModel 
					{
						Section = Section.Unknown,
						//Title = "Profile",
						//Image = "noImage.png",
						ViewModelType = typeof(TestViewModel),	
					},
					new MenuViewModel 
					{
						Section = Section.Unknown,
						Title = "",
						Image = "noImage.png",
						ViewModelType = typeof(ProfilePicViewModel),	
					}


				};
			} 
			else 
			{
				this.MenuItems = new List<MenuViewModel>
				{  

					new MenuViewModel
					{
						Section = Section.MyFamily,
						Title = "My Family",
						Image = "myfamily_icon.png",
						ViewModelType = typeof(FamilyViewModel),
					},
					new MenuViewModel
					{
						Section = Section.Matcher,
						Title = "Cousin Match",
						Image = "c.png",
						ViewModelType = typeof(MatchViewModel)
					},   
					new MenuViewModel
					{
						Section = Section.MyFamily,
						Title = "Research Help",
						Image = "search_icon.png",
						ViewModelType = typeof(ResearchHelpViewModel)
					},
					new MenuViewModel
					{

						Section = Section.Matcher,
						Title = "Log Out",
						Image = "logout_icon.png",
						ViewModelType = typeof(HomePageViewModel),


					},
					new MenuViewModel 
					{
						Section = Section.Unknown,
						//Title = "Profile",
						//Image = "noImage.png",
						ViewModelType = typeof(TestViewModel),	
					},
					new MenuViewModel 
					{
						Section = Section.Unknown,
						Title = "Profile",
						Image = "noImage.png",
						ViewModelType = typeof(ProfilePicViewModel),	
					}

				};
			}
		}

		private void ReloadMenuList(bool boolValue)
		{
			this.SetItemList (boolValue);
			DoUpdate ();
		}
		#endregion


		private void DoUpdate()
		{
			var _flyoutMessenger = Mvx.Resolve<IMvxMessenger>();
			_flyoutMessenger.Publish (new ReloadFlyOutViewMessage (this));
		}

		private void CloseFlyoutMenu()
		{
			this.ClearDatabase ();
			this.Close (this);
		}

		public class DetailParameters
		{
			public bool IsFBLogin { get; set; }
		}
	}
}

