using System;
using Cirrious.MvvmCross.ViewModels;
using System.Windows.Input;
using Cirrious.CrossCore;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AncestorCloud.Shared.ViewModels
{
	public class SignUpViewModel : BaseViewModel
	{
		public ISignUpService _ISignUpService; 

		private IDatabaseService _databaseService;

		private IAlert Alert;

		private readonly FaceBookLinkManager _facebookLinkManager;

		#region SignUpViewModel

		public SignUpViewModel(ISignUpService service,IAlert alert)//, IDatabaseService dService)

		{
			_ISignUpService = service;
			_databaseService = Mvx.Resolve<IDatabaseService>();
			Alert = alert;
			_facebookLinkManager = new FaceBookLinkManager ();
		
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
			ShowViewModel <ProfilePicViewModel> ();
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

			if (ValidateCredentials ()) {
				// Validate Parameters
				ResponseModel<LoginModel> response = await _ISignUpService.SignUp (FirstName,LastName, Email, Password, AppConstant.DEVELOPERID, AppConstant.DEVELOPERPASSWORD);

				if (response.Status == ResponseStatus.OK) {
					//tell View about data arriving
					_databaseService.InsertLoginDetails(response.Content as LoginModel);

					//_databaseService.GetLoginDetails ();

					if (Mvx.CanResolve<IAndroidService> ()) 
					{
						//ShowMyFamilyViewModel ();
						ShowProfilePicViewModel();
						CloseCommand.Execute (null);
					}
					else
					{
						IsFbLogin = false;
						ShowProfilePicViewModel();
						//CallFlyoutCommand.Execute(null);
						CloseCommand.Execute (null);
					}

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
				Alert.ShowAlert("FirstName is required,please enter a value for the field","Name Missing");
			}
			else if (String.IsNullOrEmpty (this.LastName)) 
			{
				ok = false;
				Alert.ShowAlert("LastName is required,please enter a value for the field","Name Missing");
			}

			else if (String.IsNullOrEmpty (this.Email)) 
			{
				ok = false;
				Alert.ShowAlert("Email is required,please enter a value for the field","Email Missing");
			}
			else if (!DataValidator.EmailIsValid (this.Email)) 
			{
				ok = false;
				Alert.ShowAlert("Email you entered is not valid","Email Invalid");
			}

			else if (String.IsNullOrEmpty (this.Password)) 
			{
				ok = false;
				Alert.ShowAlert("Password is required,please enter a value for the field","Password Missing");
			}

			return ok;
		}


		#endregion

		#region Facebook Data


		public void GetFbData()
		{

			Dictionary <string,object> dict = JsonConvert.DeserializeObject<Dictionary<string,object>> (FbResponseText);

			User user = DataParser.GetUserDetails (dict);

			foreach (var keyValuePair in dict) {

				Utility.Log (" :" + keyValuePair.Key+ keyValuePair.Value);
			}
		}



		#endregion


		#region Facebook Data
		public void SaveFbData()
		{

			Dictionary <string,object> dict = JsonConvert.DeserializeObject<Dictionary<string,object>> (FbResponseText);

			User user = DataParser.GetUserDetails (dict);

			_databaseService.InsertUser (user);

//			List<User> list= _databaseService.GetUsers ("1404007466586095");
//
//			User newuser = _databaseService.GetUser (2);
		}

		public void SaveFbFamilyData()
		{

			Dictionary <string,object> dict = JsonConvert.DeserializeObject<Dictionary<string,object>> (FbFamilyResponseText);

			List<People> list = DataParser.GetFbFamilyData (dict);

			foreach (People people in list) {

				_databaseService.InsertRelative (people);
			}
//			List<People> peopleList = _databaseService.RelativeMatching ("brother");
		}

		public void SaveFbFriendsData()
		{

			Dictionary <string,object> dict = JsonConvert.DeserializeObject<Dictionary<string,object>> (FbFriendResponseText);

			List<People> list = DataParser.GetFbFriendsData (dict);

			foreach (People people in list) {

				_databaseService.InsertRelative (people);
			}
			//			List<People> peopleList = _databaseService.RelativeMatching ("brother");
			//
			//			System.Diagnostics.Debug.WriteLine ("PEOPLE LIST :" + peopleList);
		}
		#endregion


		#region Facebook User Link Service

		public async void DoFacebookSignInUserLink()
		{
			ResponseStatus status = await _facebookLinkManager.LinkFaceBookSignUpUser ();

			if (status == ResponseStatus.Fail) {
				Alert.ShowAlert ("Not able to link Faccebbok user to OGF. Please retry by log-in again", "ERROR");
			} else {
				SignUp ();
			}


		}

		private void SignUp()
		{
			IsFbLogin = true;
			CallFlyoutCommand.Execute(null);
			CloseCommand.Execute (null);
		}

		#endregion



	}
}

