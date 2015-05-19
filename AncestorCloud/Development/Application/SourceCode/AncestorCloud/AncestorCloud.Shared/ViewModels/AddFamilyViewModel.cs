using System;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.CrossCore;

namespace AncestorCloud.Shared.ViewModels
{
	public class AddFamilyViewModel:BaseViewModel
	{
		private IAddFamilyService _addService;

		private IDatabaseService _databaseService;

		private IAlert Alert;

		public String AddType { get; set;}

		private readonly IReachabilityService _reachabilityService;

		public AddFamilyViewModel(IAddFamilyService service, IAlert alert,IReachabilityService reachabilty)
		{
			_addService = service;
			_databaseService = Mvx.Resolve<IDatabaseService>();
			Alert = alert;
			_reachabilityService = reachabilty;
		}

		public void Init(DetailParameter param)
		{
			this.AddType = param.AddPersonType;
		}

		#region fields
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



		private string _middlename;
		public string MiddleName
		{
			get{ return _middlename;}
			set
			{
				_middlename = value;
				RaisePropertyChanged (() => MiddleName);
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

		private string _birthdate;
		public string BirthDate
		{
			get { return _birthdate; }
			set
			{
				_birthdate = value;
				RaisePropertyChanged(() => BirthDate);
			}
		}

		private string _gender;
		public string Gender
		{
			get { return _gender; }
			set
			{
				_gender = value;
				RaisePropertyChanged(() => Gender);
			}
		}

		private string _birthloc;
		public string BirthLocation
		{
			get { return _birthloc; }
			set
			{
				_birthloc = value;
				RaisePropertyChanged(() => BirthLocation);
			}
		}

		#endregion

		private People _familyMember;

		public People FamilyMember 
		{ 
			get { return _familyMember ?? new People(); }
			set
			{
				_familyMember = value;
				RaisePropertyChanged(() => FamilyMember);
			}
		
		}


		#region Commands

		private ACCommand _addPersonCommand;

		public ACCommand AddPersonCommand
		{
			get 
			{ 
				return this._addPersonCommand ?? (this._addPersonCommand = new ACCommand (this.AddPerson));
			}
		}


		#endregion

		#region close call
		public void Close()
		{
			this.Close (this);
		}
		#endregion


		public void ShowMyFamily()
		{
			ShowViewModel<MyFamilyViewModel> ();
		}

	

		#region addPerson
		public async void AddPerson(){

			if (Validate ()) {
				if (_reachabilityService.IsNetworkNotReachable ()) {
					Mvx.Resolve<IAlert> ().ShowAlert ("Please check internet connection", "Network not available");
				} else {
					LoginModel lModal = _databaseService.GetLoginDetails ();
			
					People modal = new People ();

					modal.FirstName = this.FirstName;
					modal.MiddleName = this.MiddleName;
					modal.LastName = this.LastName;
					modal.DateOfBirth = this.BirthDate;
					modal.BirthLocation = this.BirthLocation;
					modal.Gender = this.Gender;
					modal.SessionId = lModal.Value;
					modal.Relation = this.AddType;
					modal.LoggedinUserINDIOFGN = lModal.IndiOGFN;
					modal.LoginUserLinkID = lModal.UserEmail;
					modal.LoggedinUserFAMOFGN = lModal.FamOGFN;
					
					ResponseModel<People> response = await _addService.AddFamilyMember (modal);

					if (response.Status == ResponseStatus.OK) {
						if(response.Content != null)
						_databaseService.InsertRelative (response.Content as People);
						Alert.ShowAlert ("Successfully Added", "Success");
						Close ();
					} else {
						Alert.ShowAlert ("Failed to add, Please try Again...", "Error");
					}
				}
			}
		}
		#endregion

		#region validations
		public bool Validate()
		{
			bool isValid = true;

			if (String.IsNullOrEmpty (this.FirstName)) 
			{
				isValid = false;
				Alert.ShowAlert("First Name is required,please enter a value for the field.","FirstName Missing");
			}

			/*else if (String.IsNullOrEmpty (this.BirthDate)) 
			{
				isValid = false;
				Alert.ShowAlert("Birth Year is required,please enter a value for the field.","Birth Year Missing");
			}*/

			else if (String.IsNullOrEmpty (this.Gender))
			{
				isValid = false;
				Alert.ShowAlert("Gender is required,please select a value.","Gender Missing");
			}

			return isValid;
			
		}
		#endregion

		public class DetailParameter
		{
			public String AddPersonType{ get; set;}
		}

	}


}

