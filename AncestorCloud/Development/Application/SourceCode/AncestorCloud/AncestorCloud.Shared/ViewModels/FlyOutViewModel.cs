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
	

		public FlyOutViewModel ()
		{
         	this.menuItems = new List<MenuViewModel>
			{  

				new MenuViewModel
				{

					Section = Section.MyFamily,
					Title = "My Family",
					ViewModelType = typeof(FamilyViewModel),
				},
				new MenuViewModel
				{
					Section = Section.Matcher,
					Title = "Matcher",
					ViewModelType = typeof(FbFamilyViewModel)
				},   
				new MenuViewModel
				{
					Section = Section.MyFamily,
					Title = "Research Help",
					ViewModelType = typeof(FamilyViewModel)
				},
				new MenuViewModel
				{
					
					Section = Section.Matcher,
					Title = "Log Out",
					ViewModelType = typeof(HomePageViewModel),

				},
				
			};
			var _messenger = Mvx.Resolve<IMvxMessenger>();
			navigationMenuToggleToken = _messenger.SubscribeOnMainThread<Message>(message => this.Close(this));

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



	}
}

