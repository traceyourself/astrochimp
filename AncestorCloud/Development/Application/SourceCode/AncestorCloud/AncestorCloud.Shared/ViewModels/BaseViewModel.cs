using System;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.Plugins.Json;
using Cirrious.MvvmCross.Plugins.Messenger;
using AncestorCloud.Shared.ViewModels;

namespace AncestorCloud.Shared
{
	public class BaseViewModel : MvxViewModel
	{
		#region Globals
		readonly IDatabaseService _databaseService;
		private MvxSubscriptionToken logoutToken;
		IMvxMessenger _mvxMessenger = Mvx.Resolve<IMvxMessenger>();

		#endregion

		#region Initialization

		public BaseViewModel()
		{
			_databaseService = Mvx.Resolve<IDatabaseService> ();
			Mvx.RegisterType<IMvxJsonConverter, MvxJsonConverter>();

			AddMessenger ();
		}
		#endregion

		#region Auto Logout methods
		private void AddMessenger()
		{
			logoutToken = _mvxMessenger.SubscribeOnMainThread<LogoutMessage>(message => this.Logout());
		}

		public void RemoveMessenger()
		{
			_mvxMessenger.Unsubscribe<LogoutMessage> (logoutToken);
		}

		public void Logout()
		{
			if (Mvx.CanResolve<IAndroidService> ()) {
				ClearDatabase ();
				this.Close (this);
				if(!App.IsHomePageShown){
					ShowViewModel<HomePageViewModel> ();
					App.IsHomePageShown = true;
				}
			} else {
				//IOS part
			}
		}
		#endregion


		#region Properties
		private string title = string.Empty;
    	/// <summary>
		/// Gets or sets the name of the menu
		/// </summary>
		public string Title
		{
			get { return this.title; }
			set { this.title = value; this.RaisePropertyChanged(() => this.Title); }
		
		}
			
		private string image = string.Empty;

		public string Image
		{
			get { return this.image; }
			set { this.image = value; this.RaisePropertyChanged(() => this.Image); }

		}
		#endregion

		#region Common Methods

		public void ClearDatabase()
		{
			_databaseService.DropAllTables ();
		}

		#endregion
	}
}

