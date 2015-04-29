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

		private ILoginService _loginService;

		private IDatabaseService _databaseService;

		private IAlert Alert;


		public LoginViewModel(ILoginService service, IAlert alert)
		{
			_loginService = service;
			_databaseService = Mvx.Resolve<IDatabaseService>();
			Alert = alert;
			Email = "mikeyamadeo@gmail.com";
			Password = "password";

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

		#endregion


		#region Email Login

		public async void DoLogin()
		{
			// Validate Parameters

			if (ValidateCredentials ()) {
				ResponseModel<LoginModel> response = await _loginService.Login (Email, Password, AppConstant.DEVELOPERID, AppConstant.DEVELOPERPASSWORD);

				if (response.Status == ResponseStatus.OK) {

					if (Mvx.CanResolve<IAndroidService> ()) 
					{
						ShowMyFamilyViewModel ();
						CloseCommand.Execute (null);
					}
					else
					{
						IsFbLogin = false;
						CallFlyoutCommand.Execute(null);
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
		 
			if (String.IsNullOrEmpty (this.Email)) 
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
		public void SaveFbData()
		{
			
			Dictionary <string,object> dict = JsonConvert.DeserializeObject<Dictionary<string,object>> (FbResponseText);

			User user = DataParser.GetUserDetails (dict);

			_databaseService.InsertUser (user);

//			List<User> list= _databaseService.GetUsers ("1404007466586095");
//
//			System.Diagnostics.Debug.WriteLine ("LIST:"+list);
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
//
//			System.Diagnostics.Debug.WriteLine ("PEOPLE LIST :" + peopleList);
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


	}

}

