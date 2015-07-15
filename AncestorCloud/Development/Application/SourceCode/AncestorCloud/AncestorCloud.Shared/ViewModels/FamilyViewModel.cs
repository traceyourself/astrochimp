using System;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.CrossCore;
using System.Windows.Input;
using Cirrious.MvvmCross.Plugins.Messenger;

namespace AncestorCloud.Shared.ViewModels
{
	public class FamilyViewModel : BaseViewModel
	{

		private readonly FaceBookLinkManager _facebookLinkManager;
		private IAlert Alert;
		private MvxSubscriptionToken logoutToken;
		IMvxMessenger _mvxMessenger = Mvx.Resolve<IMvxMessenger>();

		public FamilyViewModel( IAlert alert)
		{
			_facebookLinkManager = new FaceBookLinkManager ();
			Alert = alert;

			AddMessenger ();
			if(App.IsAutoLogin)
			{
				if(Mvx.CanResolve<IAndroidService>())
				{
					App.IsAutoLogin = false;
				}

				LinkFbUserData.Execute (null);
			}
		}

		#region Globals
		private readonly IDatabaseService _databaseService;
		#endregion

		#region Init
		public FamilyViewModel()
		{
			_databaseService = Mvx.Resolve<IDatabaseService> ();	
		}
		#endregion

		#region Relationship View

		public void ShowEditViewModel()
		{
			ShowViewModel<MyFamilyViewModel> ();
		}
		#endregion

		#region Help

		public void ShowHelpViewModel()
		{
			ShowViewModel<HelpViewModel> ();
		}
		#endregion

		#region Close Method
		public void Close()
		{
			this.Close(this);
		}
		#endregion

		#region get Userdata method
		public LoginModel GetUserData()
		{
			LoginModel data = new LoginModel ();
			try{
				data = _databaseService.GetLoginDetails ();
			}catch(Exception e){
				Mvx.Trace (e.StackTrace);
			}
			return data;
		}
		#endregion

		#region call_next screen _ android
		public void CallMatcher()
		{
			ShowViewModel<MatchViewModel> ();
			this.Close(this);
		}
		#endregion

		#region call home for logout
		public void ShowHomeViewModel()
		{
			ShowViewModel<HomePageViewModel> ();
			this.Close(this);
		}
		#endregion

		#region call Reseach help
		public void ShowResearchHelpViewModel()
		{
			ShowViewModel<ResearchHelpViewModel> ();
		}
		#endregion
	
		#region logout
		public void Logout()
		{
			base.ClearDatabase ();
			ShowViewModel<HomePageViewModel>();
			this.Close(this);
		}
		#endregion

		#region ProfilePic
		public void ShowProfilePicModel()
		{
			ShowViewModel<ProfilePicViewModel> (new ProfilePicViewModel.DetailParameter { FromSignUp = false });
		}
		#endregion


		private ACCommand _linkFbUserData;
		public ICommand LinkFbUserData
		{
			get
			{
				return this._linkFbUserData ?? (this._linkFbUserData = new ACCommand(this.DoFacebookLoginUserLink));
			}
		}

		#region Facebook User Link Service
		public async void DoFacebookLoginUserLink()
		{
			ResponseStatus status = await _facebookLinkManager.LinkFaceBookLoginUser ();

			if (status == ResponseStatus.Fail) {
				Alert.ShowLogoutAlert (AlertConstant.AUTO_LOGIN_RESPONSE_ERROR_MESSAGE, AlertConstant.LOGIN_RESPONSE_ERROR);
			} 
		}
		#endregion

		private void AddMessenger()
		{
			//logoutToken = _mvxMessenger.SubscribeOnMainThread<LogoutMessage>(message => this.Logout());
		}

		public void RemoveMessenger()
		{
			//_mvxMessenger.Unsubscribe<LogoutMessage> (logoutToken);
		}

	}
}