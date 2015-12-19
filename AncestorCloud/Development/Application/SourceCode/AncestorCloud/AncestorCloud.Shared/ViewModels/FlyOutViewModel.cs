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
		#region Globals
		//TODO:Move into Constant class
		public enum Section
		{
			Unknown,
			MyFamily,
			Matcher,
			ResearchHelp,
		}

		private MvxSubscriptionToken navigationMenuToggleToken;
		private MvxSubscriptionToken changeFlyoutToken;
		private MvxSubscriptionToken logoutToken;
		private  bool IsFaceBookLogin{ get; set;}
		private readonly IDatabaseService _databaseService;
	
		#endregion

		#region Init
		public void Init(DetailParameters parameter)
		{
			this.IsFaceBookLogin = parameter.IsFBLogin;
			this.SetItemList (IsFaceBookLogin);
		}

		public FlyOutViewModel (IDatabaseService  service)
		{

			//			var _messenger = Mvx.Resolve<IMvxMessenger>();
			//			navigationMenuToggleToken = _messenger.SubscribeOnMainThread<Message>(message => this.Close(this));

			_databaseService = service;

			var _flyoutMessenger = Mvx.Resolve<IMvxMessenger>();
			changeFlyoutToken = _flyoutMessenger.SubscribeOnMainThread<ChangeFlyoutFlowMessage>(message => this.ReloadMenuList(message.ChangeFlyoutFlow));

			var _messenger = Mvx.Resolve<IMvxMessenger>();
			navigationMenuToggleToken = _messenger.SubscribeOnMainThread<FlyOutCloseMessage>(message => this.CloseFlyoutMenu());

			var _logoutMessenger = Mvx.Resolve<IMvxMessenger>();
			logoutToken = _logoutMessenger.SubscribeOnMainThread<LogoutMessage>(message => this.DoLogout());
			_databaseService = service;

		}

		#endregion

		#region get Userdata method
		public LoginModel GetUserData()
		{
			LoginModel data = new LoginModel ();
			try{
				data = _databaseService.GetLoginDetails ();
			}
			catch
			(Exception e)
			{
			}
			return data;
		}
		#endregion

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
				this.ShowViewModel<MyFamilyViewModel>();
				break;

			case Section.ResearchHelp:
				this.ShowViewModel<ResearchHelpViewModel>();
				break;
			}
		}

		#region Helper Method

		private void SetItemList(bool boolValue)
		{
			this.IsFaceBookLogin = boolValue;


			this.MenuItems = new List<MenuViewModel>
			{  
				new MenuViewModel
				{
					Section = Section.MyFamily,
					Title = AppConstant.MYFAMILY_TITLE,
					Image = AppConstant.MYFAMILY_ICON,
					ViewModelType = typeof(FbFamilyViewModel),
				},
				new MenuViewModel
				{
					Section = Section.Matcher,
					Title = AppConstant.COUSIN_TITLE,
					Image = AppConstant.COUSIN_ICON,
					ViewModelType = typeof(MatchViewModel)
				},   
				new MenuViewModel
				{
					Section = Section.MyFamily,
					Title = AppConstant.RESEARCH_HELP_TITLE,
					Image = AppConstant.RESEARCH_HELP_ICON,
					ViewModelType = typeof(ResearchHelpViewModel)
				},
				new MenuViewModel
				{
					Section = Section.Matcher,
					Title = AppConstant.LOGOUT_TITLE,
					Image = AppConstant.LOGOUT_ICON,
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
					Title = GetUserName( GetUserData().Name),
					//Image = "profile_img.png",
					ViewModelType = typeof(ProfilePicViewModel),	
				}

			};
			
		}

		private void ReloadMenuList(bool boolValue)
		{
			DoUpdate ();
		}
		#endregion


		#region

		string GetUserName(string name)
		{
			if (name == null)
				return "";
			
			string[] nameArray = name.Split (' ');

			return nameArray [0];
		}

		#endregion

		private void DoUpdate()
		{
			//var _flyoutMessenger = Mvx.Resolve<IMvxMessenger>();
			//_flyoutMessenger.Publish (new ReloadFlyOutViewMessage (this));
		}

		private void CloseFlyoutMenu()
		{
			this.ClearDatabase ();
			this.Close (this);
		}

		private void DoLogout()
		{
			if (!Mvx.CanResolve<IAndroidService> ()) {
				//IOS part

				ClearDatabase ();
				this.Close (this);
				App.IsAutoLogin = false;

				if(App.controllerTypeRef != ControllerType.Primary)
					ShowViewModel<HomePageViewModel> ();
			}
		}
			
		#region Parameter Class

		public class DetailParameters
		{
			public bool IsFBLogin { get; set; }
		}

		#endregion
	}
}

