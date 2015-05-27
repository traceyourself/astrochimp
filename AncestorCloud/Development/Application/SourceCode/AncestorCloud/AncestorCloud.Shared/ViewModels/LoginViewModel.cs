using System;
using Cirrious.MvvmCross.ViewModels;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Runtime.Serialization.Formatters;
using Newtonsoft.Json.Linq;
using Cirrious.CrossCore;



namespace AncestorCloud.Shared.ViewModels
{
	public class LoginViewModel: BaseViewModel
	{
		#region Globals

		private ILoginService _loginService;

		private IDatabaseService _databaseService;

		private IAlert Alert;

		private readonly FaceBookLinkManager _facebookLinkManager;

		private readonly IReachabilityService _reachabilityService;

		//private readonly IGroupCreateService _groupService;

		//private readonly IFamilyCreateService _famService;

		private readonly IIndiDetailService _indiDetailService;

		#endregion
		public LoginViewModel(ILoginService service, IAlert alert, IReachabilityService reachabilty, IGroupCreateService _service,IFamilyCreateService famService, IIndiDetailService _indiService)
		{
			_loginService = service;
			_databaseService = Mvx.Resolve<IDatabaseService>();
			Alert = alert;
			_facebookLinkManager = new FaceBookLinkManager ();

			Email = "kumar.aditya@gmail.com";
			Password = "pass";

			_reachabilityService = reachabilty;
			//_groupService = _service;
			//_famService = famService;
			_indiDetailService = _indiService;

		}

		#region
		private string _password;

		public string Password
		{
			get { return _password; }
			set
			{
				_password = value;
				RaisePropertyChanged(() => Password);
				//UpdateLoginStatus();
			}
		}

		private string _email;

		public string Email
		{
			get { return _email; }
			set
			{
				_email = value;
				RaisePropertyChanged(() => Email);
			}
		}

		private bool isFbLogin;

		public bool IsFbLogin
		{
			get { return isFbLogin; }
			set
			{
				isFbLogin = value;
				RaisePropertyChanged(() => IsFbLogin);
			}
		}

		private string _fbResponseText;

		public string FbResponseText
		{
			get { return _fbResponseText; }
			set
			{
				_fbResponseText = value;
				RaisePropertyChanged(() => FbResponseText);
				//UpdateLoginStatus();
			}
		}

		private string _fbFamilyResponseText;

		public string FbFamilyResponseText
		{
			get { return _fbFamilyResponseText; }
			set
			{
				_fbFamilyResponseText = value;
				RaisePropertyChanged(() => FbFamilyResponseText);
			}
		}

		private string _fbFriendResponseText;

		public string FbFriendResponseText
		{
			get { return _fbFriendResponseText; }
			set
			{
				_fbFriendResponseText = value;
				RaisePropertyChanged(() => FbFriendResponseText);
			}
		}
		#endregion

		#region Close Method
		public void Close()
		{
			this.Close(this);
			if (_reachabilityService.IsNetworkNotReachable ()) {
				Alert.ShowAlert (AlertConstant.INTERNET_ERROR_MESSAGE, AlertConstant.INTERNET_ERROR);
			}
		}
		#endregion

		public void ShowMyFamilyViewModel()
		{
			ShowViewModel <FamilyViewModel> ();

		}

		public void ShowHomeViewModel()
		{
			ShowViewModel <HomePageViewModel> ();
		}


		public void ShowFbFamilyViewModel()
		{
			ShowViewModel <FbFamilyViewModel>();

		}


		#region Flyout View
		public void ShowFlyOutViewModel()
		{
			ShowViewModel<FlyOutViewModel> (new FlyOutViewModel.DetailParameters { IsFBLogin = IsFbLogin });
		}
		#endregion


		#region

		private ACCommand _loginCommand;

		public ICommand LoginCommand
		{
			get
			{
				return this._loginCommand ?? (this._loginCommand = new ACCommand(this.DoLogin));
			}
		}

		private ACCommand _closeCommand;

		public ICommand CloseCommand
		{
			get
			{
				return this._closeCommand ?? (this._closeCommand = new ACCommand(this.Close));
			}
		}

		private ACCommand _callFlyoutCommand;

		public ICommand CallFlyoutCommand
		{
			get
			{
				return this._callFlyoutCommand ?? (this._callFlyoutCommand = new ACCommand(this.ShowFlyOutViewModel));
			}
		}


		private ACCommand _saveUserFbData;

		public ICommand SaveUserFbData
		{
			get
			{
				return this._saveUserFbData ?? (this._saveUserFbData = new ACCommand(this.SaveFbData));
			}
		}

		private ACCommand _linkFbUserData;

		public ICommand LinkFbUserData
		{
			get
			{
				return this._linkFbUserData ?? (this._linkFbUserData = new ACCommand(this.DoFacebookLoginUserLink));
			}
		}

		#endregion


		#region Email Login

//		public async void DoLogin()
//		{
//			// Validate Parameters
//
//			if (ValidateCredentials ()) {
//
//				if (_reachabilityService.IsNetworkNotReachable ()) {
//					Alert.ShowAlert ("Please check internet connection", "Network not available");
//				} else {
//					ResponseModel<LoginModel> response = await _loginService.Login (Email, Password, AppConstant.DEVELOPERID, AppConstant.DEVELOPERPASSWORD);
//
//					if (response.Status == ResponseStatus.OK) {
//
//						_databaseService.InsertLoginDetails (response.Content as LoginModel);
//
//						LoginModel l = _databaseService.GetLoginDetails ();
//
//						if (l.GroupOGFN == null || l.GroupOGFN.Equals ("")) {
//
//							ResponseModel<LoginModel> loginResponse = await _groupService.CreateGroup (response.Content as LoginModel);
//
//							if (loginResponse.Status == ResponseStatus.OK) {
//
//								if (loginResponse.Status == ResponseStatus.OK) {
//
//									ResponseModel<LoginModel> famResponse = await _famService.CreateFamily (response.Content as LoginModel);
//
//									_databaseService.InsertLoginDetails (famResponse.Content as LoginModel);
//
//									//_databaseService.GetLoginDetails ();
//
//									if (Mvx.CanResolve<IAndroidService> ()) {
//										ShowMyFamilyViewModel ();
//										CloseCommand.Execute (null);
//									} else {
//										IsFbLogin = false;
//										CallFlyoutCommand.Execute (null);
//										CloseCommand.Execute (null);
//									}
//								} else {
//
//									Alert.ShowAlert ("Invalid user signon username or password.", "Login Error");
//								}
//							}
//							else {
//
//								Alert.ShowAlert ("Invalid user signon username or password.", "Login Error");
//							}
//						}
//					} else {
//
//						Alert.ShowAlert ("Invalid user signon username or password.", "Login Error");
//					}
//				}
//			}
//
//		}



		public async void DoLogin()
		{
			// Validate Parameters

			if (ValidateCredentials ()) {

				if (_reachabilityService.IsNetworkNotReachable ()) {
					Alert.ShowAlert (AlertConstant.INTERNET_ERROR_MESSAGE, AlertConstant.INTERNET_ERROR);
				} else {
					ResponseModel<LoginModel> response = await _loginService.Login (Email, Password, AppConstant.DEVELOPERID, AppConstant.DEVELOPERPASSWORD);

					if (response.Status == ResponseStatus.OK) {

						ResponseModel<LoginModel> loginresponse = await _indiDetailService.GetIndiDetails (response.Content as LoginModel);

						if (loginresponse.Status == ResponseStatus.OK) {

							LoginModel model = loginresponse.Content;
							model.AvatarOGFN = response.Content.AvatarOGFN;
							model.AvatarURL = response.Content.AvatarURL;

							_databaseService.InsertLoginDetails (model as LoginModel);

							if (Mvx.CanResolve<IAndroidService> ()) {
								ShowMyFamilyViewModel ();
								CloseCommand.Execute (null);
							} else {
								IsFbLogin = false;
								CallFlyoutCommand.Execute (null);
								CloseCommand.Execute (null);
							}
						} else {
							Alert.ShowAlert (AlertConstant.LOGIN_ERROR_MESSAGE, AlertConstant.LOGIN_ERROR);
						}
					}
					else {
						Alert.ShowAlert (AlertConstant.LOGIN_ERROR_MESSAGE, AlertConstant.LOGIN_ERROR);
					}
				}
			}

		}

		#endregion


		#region validation


		public bool ValidateCredentials()
		{
			bool ok = true;
		 
			if (String.IsNullOrEmpty (this.Email)) 
			{
				ok = false;
				Alert.ShowAlert(AlertConstant.LOGIN_EMAIL_MESSAGE,AlertConstant.LOGIN_EMAIL_ERROR);
			}
			else if (!DataValidator.EmailIsValid (this.Email)) 
			{
				ok = false;
				Alert.ShowAlert(AlertConstant.LOGIN_EMAIL_INVALID_MESSAGE,AlertConstant.LOGIN_EMAIL_INVALID);
			}

			else if (String.IsNullOrEmpty (this.Password)) 
			{
				ok = false;
				Alert.ShowAlert(AlertConstant.LOGIN_PASSWORD_ERROR_MESSAGE,AlertConstant.LOGIN_PASSWORD_ERROR);
			}

			return ok;
		}
		#endregion



		#region Facebook Data
		public void SaveFbData()
		{
			
			Dictionary <string,object> dict = JsonConvert.DeserializeObject<Dictionary<string,object>> (FbResponseText);

			User user = DataParser.GetUserDetails (dict);

			_databaseService.InsertUser (user);

		}

		public void SaveFbFamilyData()
		{

			Dictionary <string,object> dict = JsonConvert.DeserializeObject<Dictionary<string,object>> (FbFamilyResponseText);

			User fbUser = _databaseService.GetUser ();

			List<People> list = DataParser.GetFbFamilyData (dict,fbUser);

			foreach (People people in list) {

				_databaseService.InsertRelative (people);
			}

		}

		public void SaveFbFriendsData()
		{

			Dictionary <string,object> dict = JsonConvert.DeserializeObject<Dictionary<string,object>> (FbFriendResponseText);

			User fbUser = _databaseService.GetUser ();

			List<People> list = DataParser.GetFbFriendsData (dict,fbUser);

			foreach (People people in list) {

				_databaseService.InsertFBFriend (people);
			}
		}
		#endregion


		#region Facebook User Link Service

		public async void DoFacebookLoginUserLink()
		{
			ResponseStatus status = await _facebookLinkManager.LinkFaceBookLoginUser ();

			if (status == ResponseStatus.Fail) {
				Alert.ShowAlert (AlertConstant.LOGIN_RESPONSE_ERROR_MESSAGE, AlertConstant.LOGIN_RESPONSE_ERROR);
			} else {
				Login ();
			}
		}

		private void Login()
		{
			if (Mvx.CanResolve<IAndroidService> ()) {
				//ShowMyFamilyViewModel ();
				ShowFbFamilyViewModel();
				CloseCommand.Execute (null);
			} else {
				IsFbLogin = true;
				CallFlyoutCommand.Execute (null);
				CloseCommand.Execute (null);
			}
		}

		#endregion

	}

}

