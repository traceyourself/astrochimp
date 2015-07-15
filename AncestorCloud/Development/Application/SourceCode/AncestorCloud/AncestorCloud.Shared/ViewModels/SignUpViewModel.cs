using System;
using Cirrious.MvvmCross.ViewModels;
using System.Windows.Input;
using Cirrious.CrossCore;
using System.Collections.Generic;
using Newtonsoft.Json;
using Cirrious.CrossCore.Platform;

namespace AncestorCloud.Shared.ViewModels
{
	public class SignUpViewModel : BaseViewModel
	{
		#region Globals
		public ISignUpService _ISignUpService; 

		private IDatabaseService _databaseService;

		private IAlert Alert;

		private readonly FaceBookLinkManager _facebookLinkManager;

		private readonly IReachabilityService _reachabilityService;

		private readonly IGroupCreateService _groupService;

		private readonly IFamilyCreateService _famService;
		#endregion

		#region SignUpViewModel

		public SignUpViewModel(ISignUpService service,IAlert alert, IReachabilityService reachabilty, IGroupCreateService _service,IFamilyCreateService famService)//, IDatabaseService dService)

		{
			_ISignUpService = service;
			_databaseService = Mvx.Resolve<IDatabaseService>();
			Alert = alert;
			_facebookLinkManager = new FaceBookLinkManager ();
			_reachabilityService = reachabilty;
			_groupService = _service;
			_famService = famService;
		
		}

		#endregion

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


		private string _firstName;

		public string FirstName
		{
			get { return _firstName; }
			set
			{
				_firstName = value;
				RaisePropertyChanged(() => FirstName);
			}
		}

		private string _lastName;

		public string LastName
		{
			get { return _lastName; }
			set
			{
				_lastName = value;
				RaisePropertyChanged(() => LastName);
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

		private bool _allowSignUp;

		public bool AllowSignUp
		{
			get { return _allowSignUp; }
			set
			{
				_allowSignUp = value;
				RaisePropertyChanged(() => AllowSignUp);
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

		public void ShowProfilePicViewModel()
		{
			ShowViewModel<ProfilePicViewModel> (new ProfilePicViewModel.DetailParameter { FromSignUp = true });

			//ShowViewModel <ProfilePicViewModel> ();
			this.Close ();
		}

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
			ShowViewModel <FbFamilyViewModel> ();

		}
			
		#region Flyout View

		public void ShowFlyOutViewModel()
		{
			ShowViewModel<FlyOutViewModel> (new FlyOutViewModel.DetailParameters { IsFBLogin = IsFbLogin });
		}

		#endregion

		#region Close Method

		public void Close()
		{
			this.Close(this);
		}

		#endregion

		private ACCommand _signUpCommand;

		public ICommand SignUpCommand
		{
			get
			{
				return this._signUpCommand ?? (this._signUpCommand = new ACCommand(this.DoSignUp));
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
				return this._linkFbUserData ?? (this._linkFbUserData = new ACCommand(this.DoFacebookSignInUserLink));
			}
		}


		#region

		public async void DoSignUp()
		{
//			IsFbLogin = false;
//			ShowProfilePicViewModel ();
//			//this.Close (this);//XXXXXX
//			//									CallFlyoutCommand.Execute(null);
//			CloseCommand.Execute (null);
//			return;


			if (ValidateCredentials ()) {

				if (_reachabilityService.IsNetworkNotReachable ()) {
					Alert.ShowAlert (AlertConstant.INTERNET_ERROR_MESSAGE, AlertConstant.INTERNET_ERROR);
				}
				// Validate Parameters
				ResponseModel<LoginModel> response = await _ISignUpService.SignUp (FirstName,LastName, Email, Password, AppConstant.DEVELOPERID, AppConstant.DEVELOPERPASSWORD);

				if (response.Status == ResponseStatus.OK) {

					_databaseService.InsertLoginDetails (response.Content as LoginModel);

					//tell View about data arriving
					//_databaseService.InsertLoginDetails(response.Content as LoginModel);

					/*ResponseModel<LoginModel> loginResponse = await _groupService.CreateGroup (response.Content as LoginModel);

					if (loginResponse.Status == ResponseStatus.OK) {*/
					
						/*ResponseModel<LoginModel> famResponse = await _famService.CreateFamily (loginResponse.Content as LoginModel);
					
						if (famResponse.Status == ResponseStatus.OK) {
							_databaseService.InsertLoginDetails (famResponse.Content as LoginModel);*/
							//_databaseService.GetLoginDetails ();

							if (Mvx.CanResolve<IAndroidService> ()) {
							//ShowMyFamilyViewModel ();
								ShowProfilePicViewModel ();
								this.Close (this);
								CloseCommand.Execute (null);
							} else {
								IsFbLogin = false;
								ShowProfilePicViewModel ();
								//this.Close (this);//XXXXXX
//									CallFlyoutCommand.Execute(null);
								CloseCommand.Execute (null);
							}
						/*}
						else {
							Alert.ShowAlert (AlertConstant.SIGNUP_ERROR_MESSAGE, AlertConstant.SIGNUP_ERROR);
						}
					}
					else
					{
						Alert.ShowAlert (AlertConstant.SIGNUP_ERROR_MESSAGE, AlertConstant.SIGNUP_ERROR);
					}*/
				}
			}
		}
		#endregion

		#region validation


		public bool ValidateCredentials()
		{
			bool ok = true; 

			if (String.IsNullOrEmpty (this.FirstName)) 
			{
				ok = false;
				Alert.ShowAlert(AlertConstant.SIGNUP_FIRSTNAME_ERROR_MESSAGE,AlertConstant.SIGNUP_FIRSTNAME_ERROR);
			}
			else if (String.IsNullOrEmpty (this.LastName)) 
			{
				ok = false;
				Alert.ShowAlert(AlertConstant.SIGNUP_SECNAME_ERROR_MESSAGE,AlertConstant.SIGNUP_SECNAME_ERROR);
			}

			else if (String.IsNullOrEmpty (this.Email)) 
			{
				ok = false;
				Alert.ShowAlert(AlertConstant.SIGNUP_EMAIL_MESSAGE,AlertConstant.SIGNUP_EMAIL_ERROR);
			}
			else if (!DataValidator.EmailIsValid (this.Email)) 
			{
				ok = false;
				Alert.ShowAlert(AlertConstant.SIGNUP_EMAIL_INVALID_MESSAGE,AlertConstant.SIGNUP_EMAIL_INVALID);
			}

			else if (String.IsNullOrEmpty (this.Password)) 
			{
				ok = false;
				Alert.ShowAlert(AlertConstant.SIGNUP_PASSWORD_ERROR_MESSAGE,AlertConstant.SIGNUP_PASSWORD_ERROR);
			}

			return ok;
		}


		#endregion

		#region Facebook Data


//		public void GetFbData()
//		{
//
//			Dictionary <string,object> dict = JsonConvert.DeserializeObject<Dictionary<string,object>> (FbResponseText);
//
//			User user = DataParser.GetUserDetails (dict);
//
//			foreach (var keyValuePair in dict) {
//
//				Utility.Log (" :" + keyValuePair.Key+ keyValuePair.Value);
//			}
//		}



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

		public async void DoFacebookSignInUserLink()
		{
			ResponseStatus status = await _facebookLinkManager.LinkFaceBookSignUpUser ();

			if (status == ResponseStatus.Fail) {
				Alert.ShowAlert (AlertConstant.SIGNUP_RESPONSE_ERROR_MESSAGE, AlertConstant.SIGNUP_RESPONSE_ERROR);
			} else {
				SignUp ();
			}
		}

		private void SignUp()
		{
			if (Mvx.CanResolve<IAndroidService> ()) 
			{
				ShowMyFamilyViewModel ();
				this.Close (this);
				CloseCommand.Execute (null);
			}
			else
			{
				IsFbLogin = true;
				CallFlyoutCommand.Execute(null);
				CloseCommand.Execute (null);
			}

		}

		#endregion


		public void ShowTermsandCondition()
		{

			//this.Close ();
			ShowViewModel<TermsandConditionViewModel> ();

		}



	}
}

