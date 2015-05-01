﻿using System;
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

		public AddFamilyViewModel(IAddFamilyService service, IAlert alert)
		{
			_addService = service;
			_databaseService = Mvx.Resolve<IDatabaseService>();
			Alert = alert;
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

		#region close call
		public void Close()
		{
			this.Close (this);
		}
		#endregion

		#region addPerson
		public async void AddPerson(){

			if(Validate())
			{
				LoginModel lModal = _databaseService.GetLoginDetails ();

				People modal = new People ();

				modal.FirstName = this.FirstName;
				modal.MiddleName = this.MiddleName;
				modal.LastName = this.LastName;
				modal.DateOfBirth = this.BirthDate;
				modal.BirthLocation = this.BirthLocation;
				modal.Gender = this.Gender;
				modal.IndiOgfn = lModal.IndiOGFN;
				modal.SessionId = lModal.Value;
				modal.Relation = this.AddType;
					
				ResponseModel<ResponseDataModel> response = await _addService.AddFamilyMember(modal);

				if (response.Status == ResponseStatus.OK) {
					Alert.ShowAlert ("Successfully Added","Success");
					Close ();
				} else {
					Alert.ShowAlert ("Failed to add, Please try Again...","Error");
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

	}
}

