using System;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.CrossCore;
using System.Collections.Generic;

namespace AncestorCloud.Shared.ViewModels
{
	public class AddFamilyViewModel:BaseViewModel
	{

		#region Globals
		private IAddFamilyService _addService;

		private IDatabaseService _databaseService;

		private readonly IAlert Alert;

		private readonly IReachabilityService _reachabilityService;

		FamilyDataManager _familyDataManager;

		public String AddType { get; set;}
		public String ReferenceType { get; set;}
		#endregion

		#region Initialization
		public AddFamilyViewModel(IAddFamilyService service, IAlert alert,IReachabilityService reachabilty)
		{
			_addService = service;
			_databaseService = Mvx.Resolve<IDatabaseService>();
			Alert = alert;
			_reachabilityService = reachabilty;
			_familyDataManager = new FamilyDataManager ();

		}

		public void Init(DetailParameter param)
		{
			this.AddType = param.AddPersonType ?? String.Empty;
			this.ReferenceType = param.AddPersonRelationShipRefType ?? String.Empty;
		}
		#endregion

		#region Prtoperties
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
		#endregion
	
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

		#region Helper Methods

		public void Close()
		{
			this.Close (this);
		}

		public void ShowMyFamily()
		{
			ShowViewModel<MyFamilyViewModel> ();
		}

		#endregion

		#region AddPerson
		public async void AddPerson(){

			if (Validate ()) {
				if (_reachabilityService.IsNetworkNotReachable ()) {
					Mvx.Resolve<IAlert> ().ShowAlert (AlertConstant.INTERNET_ERROR_MESSAGE, AlertConstant.INTERNET_ERROR);
				} else {

					bool isValid = true;

					LoginModel lModal = _databaseService.GetLoginDetails ();

					People modal = new People ();

					modal.FirstName = this.FirstName;
					modal.MiddleName = this.MiddleName;
					modal.LastName = this.LastName;
					modal.DateOfBirth = this.BirthDate;
					modal.BirthLocation = this.BirthLocation;
					modal.Gender = this.Gender;
					modal.SessionId = lModal.Value;
					modal.LoginUserLinkID = lModal.UserEmail;
					modal.RelationReference = ReferenceType;
					//modal.LoggedinUserINDIOFGN = lModal.IndiOGFN;
					//modal.LoggedinUserFAMOFGN = lModal.FamOGFN;

					if (AddType.Equals ("Grandparent")) {
						
						List<People> listP = _familyDataManager.GetParents();

						if (listP.Count > 0) {
							if (ReferenceType.Equals (AppConstant.Father_Reference)) {

								foreach (People p in listP) {
									if (p.Gender != null) {
										if (p.Relation.Equals ("Parent") || p.Relation.Equals ("father")) {
											if (p.Gender.Equals ("Male")) {
												modal.LoggedinUserINDIOFGN = p.IndiOgfn;
												//modal.LoggedinUserFAMOFGN = p.FamOGFN;
											}
										}
									}
								}

							} else if (ReferenceType.Equals (AppConstant.Mother_Reference)) {

								foreach (People p in listP) {
									if (p.Gender != null) {
										if (p.Relation.Equals ("Parent") || p.Relation.Equals ("mother")) {
											if (p.Gender.Equals ("Female")) {
												modal.LoggedinUserINDIOFGN = p.IndiOgfn;
												//modal.LoggedinUserFAMOFGN = p.FamOGFN;
											}
										}
									}
								}

							}

							if(modal.LoggedinUserINDIOFGN == null){

								isValid = false;
								//Alert.ShowAlert ("Please add parents first to add Grand Parents","");
								//modal.LoggedinUserFAMOFGN = listP [0].FamOGFN;
							}

							modal.Relation = AppConstant.GrandParent_comparison;
							modal.RelationType = AppConstant.Parent_comparison;

						} else {
							isValid = false;
							Alert.ShowAlert ("Please add parents first to add Grand Parents","");
						}
					} else if (AddType.Equals ("Great Grandparent")) {

						List<People> listP = _familyDataManager.GetGrandParents();

						if (listP.Count > 0) {
							if (ReferenceType.Equals (AppConstant.Grand_Father_Father_Reference)) {

								foreach (People p in listP) {
									if (p.Gender != null) {
										if (p.Relation.Equals ("Grandparent"))
										{
											if (p.RelationReference.Equals (AppConstant.Father_Reference)) {
												if (p.Gender.Equals ("Male")) {
													modal.LoggedinUserINDIOFGN = p.IndiOgfn;
													//modal.LoggedinUserFAMOFGN = p.FamOGFN;
												}
											}
										}
									}
								}

							} else if (ReferenceType.Equals (AppConstant.Grand_Mother_Father_Reference)) {

								foreach (People p in listP) {
									if (p.Gender != null) {
										if (p.Relation.Equals ("Grandparent")) {
											if (p.RelationReference.Equals (AppConstant.Father_Reference)) {
												if (p.Gender.Equals ("Female")) {
													modal.LoggedinUserINDIOFGN = p.IndiOgfn;
													//modal.LoggedinUserFAMOFGN = p.FamOGFN;
												}
											}
										}
									}
								}

							}else if (ReferenceType.Equals (AppConstant.Grand_Father_Mother_Reference)) {

								foreach (People p in listP) {
									if (p.Gender != null) {
										if (p.Relation.Equals ("Grandparent")) {
											if (p.RelationReference.Equals (AppConstant.Mother_Reference)) {
												if (p.Gender.Equals ("Male")) {
													modal.LoggedinUserINDIOFGN = p.IndiOgfn;
													//modal.LoggedinUserFAMOFGN = p.FamOGFN;
												}
											}
										}
									}
								}

							}else if (ReferenceType.Equals (AppConstant.Grand_Mother_Mother_Reference)) {

								foreach (People p in listP) {
									if (p.Gender != null) {
										if (p.Relation.Equals ("Grandparent")) {
											if (p.RelationReference.Equals (AppConstant.Mother_Reference)) {
												if (p.Gender.Equals ("Female")) {
													modal.LoggedinUserINDIOFGN = p.IndiOgfn;
													//modal.LoggedinUserFAMOFGN = p.FamOGFN;
												}
											}
										}
									}
								}

							}

							if(modal.LoggedinUserINDIOFGN == null){
								isValid = false;
								Alert.ShowAlert ("Please add Grand parents first to add Great Grand Parents","");
							}

							modal.Relation = AppConstant.GreatGrandParent_comparison;
							modal.RelationType = AppConstant.Parent_comparison;

						} else {
							isValid = false;
							Alert.ShowAlert ("Please add Grand parents first to add Great Grand Parents","");
						}


					} else {
						modal.LoggedinUserINDIOFGN = lModal.IndiOGFN;
						modal.Relation = this.AddType;
						modal.RelationType = this.AddType;
						//modal.LoggedinUserFAMOFGN = lModal.FamOGFN;
					}

			
					if(isValid){
						ResponseModel<People> response = await _addService.AddFamilyMember (modal);

						if (response.Status == ResponseStatus.OK) 
						{
							if (response.Content != null)
								_databaseService.InsertRelative (response.Content as People);
							Alert.ShowAlert (AlertConstant.SUCCESS_RESPONSE_ALERT, AlertConstant.SUCCESS_ALERT);
							Close ();
						}
						else if (response.ResponseCode.Equals (AppConstant.DEVELOPER_NOT_LOGIN_CODE)) {
							Alert.ShowLogoutAlert (AlertConstant.AUTO_LOGIN_RESPONSE_ERROR_MESSAGE, AlertConstant.SUCCESS_ERROR);
						}
						else {
							Alert.ShowAlert (AlertConstant.SUCCESS_ERROR_MESSAGE, AlertConstant.SUCCESS_ERROR);
						}
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
				Alert.ShowAlert(AlertConstant.NAME_ALERT_MESSAGE,AlertConstant.NAME_ALERT);
			}

			else if (String.IsNullOrEmpty (this.Gender))
			{
				isValid = false;
				Alert.ShowAlert(AlertConstant.GENDER_ALERT_MESSAGE,AlertConstant.GENDER_ALERT);
			}

			/*else if((AddType != null) && ( AddType.Contains("Grandparent") || AddType.Contains("Great Grandparent"))){
				if (String.IsNullOrEmpty (this.ReferenceType)) 
				{
					isValid = false;
					Alert.ShowAlert("Relationship is required, please select a value.","Relation Missing");
				}
			}*/

			return isValid;
			
		}
		#endregion


		#region Parameter Inner Class

		public class DetailParameter
		{
			public String AddPersonType{ get; set;}

			public String AddPersonRelationShipRefType{ get; set;}
		}

		#endregion

	}


}

