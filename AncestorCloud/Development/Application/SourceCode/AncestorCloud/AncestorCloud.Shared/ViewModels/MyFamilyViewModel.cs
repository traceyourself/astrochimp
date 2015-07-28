using System;
using System.Collections.Generic;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Plugins.Messenger;
using AncestorCloud.Core;


namespace AncestorCloud.Shared.ViewModels
{
	public class MyFamilyViewModel:BaseViewModel
	{
		#region Globals
		LoginModel model;

		private readonly IDatabaseService _databaseService;

		private readonly IAddFamilyService _addService;
		private readonly IGetFamilyService _getFamilyService;

		private readonly IPercentageService _percentageService;

		private readonly IAlert Alert;

		private readonly IReachabilityService _reachabilityService;

		private readonly IIndiDetailService _indiDetailService;

		public String _PercentageComplete{ get; set;}

		IMvxMessenger _messenger = Mvx.Resolve<IMvxMessenger>();
		FamilyDataManager _familyDataManager;

		#endregion

		public MyFamilyViewModel(IDatabaseService  service, IReachabilityService reachabilty)
		{
			_databaseService = service;
			_addService = Mvx.Resolve<IAddFamilyService>();
			_getFamilyService = Mvx.Resolve<IGetFamilyService>();
			_percentageService = Mvx.Resolve<IPercentageService> ();
			Alert = Mvx.Resolve<IAlert>();
			_familyDataManager = new FamilyDataManager ();

			GetFbFamilyData ();
			_reachabilityService = reachabilty;
		}

		#region show Add family dialog model
		public void ShowAddFamilyViewModel()
		{
			ShowViewModel<AddFamilyViewModel> ();
		}
		#endregion

		#region show my family
		public void ShowFamilyViewModel()
		{
			ShowViewModel<FamilyViewModel> ();
		}
		#endregion

		#region Help

		public void ShowHelpViewModel()
		{
			ShowViewModel<HelpViewModel> ();
		}

		#endregion

		#region __IOS__ Only

		private People familyMember;

		public People FamilyMember
		{
			get { return familyMember; }
			set
			{
				familyMember = value;
				RaisePropertyChanged(() => FamilyMember);
			}
		}

		#endregion

		#region Properties
		private List<People> familyList;

		public List<People> FamilyList
		{
			get { return familyList; }
			set
			{
				familyList = value;
				RaisePropertyChanged(() => FamilyList);
			}
		}
			
		private List<TableItem> parentTableDataList;

		public List<TableItem> ParentsTableDataList
		{
			get { return parentTableDataList; }
			set
			{
				parentTableDataList = value;
				RaisePropertyChanged(() => ParentsTableDataList);
			}
		}

		private List<TableItem> gparentTableDataList;

		public List<TableItem> GParentsTableDataList
		{
			get { return gparentTableDataList; }
			set
			{
				gparentTableDataList = value;
				RaisePropertyChanged(() => GParentsTableDataList);
			}
		}

		private List<TableItem> ggparentTableDataList;

		public List<TableItem> GGParentsTableDataList
		{
			get { return ggparentTableDataList; }
			set
			{
				ggparentTableDataList = value;
				RaisePropertyChanged(() => GGParentsTableDataList);
			}
		}

		#endregion


		#region Sqlite Methods

		public void GetFbFamilyData()
		{
			LoginModel login = _databaseService.GetLoginDetails ();
			List<People> list = _databaseService.RelativeMatching ("",login.UserEmail);
			FamilyList = list;
			FetchPercentageComplete ();
		}
		#endregion

		#region get FamilyMembers From server
		public async void GetFamilyMembersFromServer()
		{
			LoginModel login = _databaseService.GetLoginDetails ();
			ResponseModel<List<People>> listFromServer = await _getFamilyService.GetFamilyMembers (login);
			if (listFromServer.Status == ResponseStatus.OK) {
				FamilyList = listFromServer.Content;
			}
			_messenger.Publish(new MyFamilyReloadMessage(this));
			FetchPercentageComplete ();
		}
		#endregion

		#region Close Method
		public void Close()
		{
			this.Close(this);
		}
		#endregion

		public void ShowEditViewModel()
		{
			ShowViewModel <LoginViewModel> ();
		}

		public void ShowAddParents(String relation, string relationShipType)
		{
			ShowViewModel<AddFamilyViewModel> (new AddFamilyViewModel.DetailParameter { AddPersonType = relation, AddPersonRelationShipRefType = relationShipType });
		}

		public void ShowEditFamily()
		{
			ShowViewModel<EditFamilyViewModel>();
		}

		#region EDIT SERVICE __IOS__ Only
		public async void EditPerson()
		{
			LoginModel lModal = _databaseService.GetLoginDetails ();

			FamilyMember.SessionId = lModal.Value;

			//TODO : Remove this line when data is live
			//FamilyMember.IndiOgfn = lModal.IndiOGFN;

			if (_reachabilityService.IsNetworkNotReachable ()) {
				Mvx.Resolve<IAlert> ().ShowAlert (AlertConstant.INTERNET_ERROR_MESSAGE,AlertConstant.INTERNET_ERROR);
			} else {

				ResponseModel<People> response = await _addService.EditFamilyMember (FamilyMember);

				if (response.Status == ResponseStatus.OK) {
					_databaseService.UpdateRelative (response.Content as People);
					_messenger.Publish(new MyFamilyReloadMessage(this));
					Alert.ShowAlert (AlertConstant.EDIT_SUCCESS_MESSAGE,AlertConstant.EDIT_SUCCESS);
				} else {
					if (response.ResponseCode.Equals (AppConstant.DEVELOPER_NOT_LOGIN_CODE)) {
						Alert.ShowLogoutAlert (AlertConstant.AUTO_LOGIN_RESPONSE_ERROR_MESSAGE,AlertConstant.SUCCESS_ERROR);
					} else {
						Alert.ShowAlert (AlertConstant.EDIT_ERROR_MESSAGE, AlertConstant.EDIT_ERROR);
					}
				}
			}
			return ;
		} 
		#endregion

		#region PercentageModel
		public async void  FetchPercentageComplete()
		{
			LoginModel model = _databaseService.GetLoginDetails ();

			ResponseModel<LoginModel> loginresponse = await _percentageService.GetPercentComplete(model);

			if (loginresponse.Status == ResponseStatus.OK) {

				 model = loginresponse.Content;
			
				_PercentageComplete = model.PercentageComplete;

				Mvx.Trace ("PERCENTAGE:-" + model.PercentageComplete);

				_databaseService.InsertLoginDetails (model as LoginModel);

				_messenger.Publish(new PercentageMessage(this));

			}
		}
		#endregion


		#region check validity for family Adiition
		public void CheckIfCanAddPerson(string relation, string relationshipRef)
		{
			string typeToAdd = relation;
			string _relationTypeRef = GetRelationshipType (relationshipRef);
			LoginModel model = _databaseService.GetLoginDetails ();
			//Sibling_comparison
			if(typeToAdd.Contains("Sibling") || typeToAdd.Equals("Parent")){
				/*if (Mvx.CanResolve<IAndroidService> ()) {
					ShowViewModel<AddFamilyViewModel> (new AddFamilyViewModel.DetailParameter { AddPersonType = relation, AddPersonRelationShipRefType = _relationTypeRef });
					ShowAddFamilyViewModel ();
				} else {*/
					ShowAddParents (relation,_relationTypeRef);
				//}

			}else{
				if(typeToAdd.Equals("Grandparent")){

					List<People> listP = _familyDataManager.GetParents();//_databaseService.RelativeMatching (AppConstant.Parent_comparison,model.UserEmail);
					if (listP != null && listP.Count > 0) 
					{
						/*if (Mvx.CanResolve<IAndroidService> ()) 
						{
							ShowAddFamilyViewModel ();
						}
						else 
						{*/
							ShowAddParents (relation,_relationTypeRef);
						//}
					}
					else 
					{
						Alert.ShowAlert ("Please add parents first to add grand parents","");
					}

				}else if(typeToAdd.Equals("Great Grandparent")){
					List<People> listP = _familyDataManager.GetGrandParents();//_databaseService.RelativeMatching (AppConstant.GrandParent_comparison,model.UserEmail);
					if (listP != null && listP.Count > 0) {
						/*if (Mvx.CanResolve<IAndroidService> ()) 
						{
							ShowAddFamilyViewModel ();
						}else 
						{*/
							ShowAddParents (relation,_relationTypeRef);
						//}
					} else {
						Alert.ShowAlert ("Please add grand parents first to add great grand parents","");
					}
				}
			}
				
			/*****
			ViewModel.ShowAddFamilyViewModel();
			******/
		}
		#endregion

		#region Reference Type Relationship

		string GetRelationshipType(string relationshipRef)
		{
			string _ref = String.Empty;

			if (relationshipRef.Equals (AppConstant.FATHERS_PARENT))
			{
				_ref = AppConstant.Father_Reference;
			}
			else if (relationshipRef.Equals (AppConstant.MOTHERS_PARENT))
			{
				_ref = AppConstant.Mother_Reference;
			}
			else if (relationshipRef.Equals (AppConstant.FATHERS_FATHERS_PARENT))
			{
				_ref = AppConstant.Grand_Father_Father_Reference;
			}
			else if (relationshipRef.Equals (AppConstant.FATHERS_MOTHERS_PARENT))
			{
				_ref = AppConstant.Grand_Father_Mother_Reference;

			}
			else if (relationshipRef.Equals (AppConstant.MOTHERS_FATHERS_PARENT))
			{
				_ref = AppConstant.Grand_Mother_Father_Reference;
			}
			else if (relationshipRef.Equals (AppConstant.MOTHERS_MOTHERS_PARENT))
			{
				_ref = AppConstant.Grand_Mother_Mother_Reference;
			}

			return _ref;
				
		}

		#endregion


	}
}