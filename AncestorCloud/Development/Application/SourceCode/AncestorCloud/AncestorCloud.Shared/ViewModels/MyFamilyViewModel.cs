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


		#endregion

		public MyFamilyViewModel(IDatabaseService  service, IReachabilityService reachabilty)
		{
			_databaseService = service;
			_addService = Mvx.Resolve<IAddFamilyService>();
			_getFamilyService = Mvx.Resolve<IGetFamilyService>();
			_percentageService = Mvx.Resolve<IPercentageService> ();
			Alert = Mvx.Resolve<IAlert>();;
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
			
		private List<TableItem> tableDataList;

		public List<TableItem> TableDataList
		{
			get { return tableDataList; }
			set
			{
				tableDataList = value;
				RaisePropertyChanged(() => TableDataList);
			}
		}

		#endregion


		#region Sqlite Methods

		public void GetFbFamilyData()
		{
			LoginModel login = _databaseService.GetLoginDetails ();
			List<People> list = _databaseService.RelativeMatching ("",login.UserEmail);
			FamilyList = list;
		}

		#endregion


		#region get FamilyMembers From server
		public async void GetFamilyMembersFromServer()
		{
			LoginModel login = _databaseService.GetLoginDetails ();
			ResponseModel<List<People>> listFromServer = await _getFamilyService.GetFamilyMembers (login);
			if (listFromServer.Status == ResponseStatus.OK) {
				FamilyList = listFromServer.Content;
				_messenger.Publish(new MyFamilyLoadViewMessage(this));
			}
				
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

		public void ShowAddParents(String relation)
		{
			ShowViewModel<AddFamilyViewModel> (new AddFamilyViewModel.DetailParameter { AddPersonType = relation });
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
			FamilyMember.IndiOgfn = lModal.IndiOGFN;

			if (_reachabilityService.IsNetworkNotReachable ()) {
				Mvx.Resolve<IAlert> ().ShowAlert (AlertConstant.INTERNET_ERROR_MESSAGE,AlertConstant.INTERNET_ERROR);
			} else {

				ResponseModel<People> response = await _addService.EditFamilyMember (FamilyMember);

				if (response.Status == ResponseStatus.OK) {
					_databaseService.UpdateRelative (response.Content as People);
					_messenger.Publish(new MyFamilyReloadMessage(this));
					Alert.ShowAlert (AlertConstant.EDIT_SUCCESS_MESSAGE,AlertConstant.EDIT_SUCCESS);
				} else {
					Alert.ShowAlert (AlertConstant.EDIT_ERROR_MESSAGE,AlertConstant.EDIT_ERROR);
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

				Mvx.Trace ("PERCENTAGE:- " + model.PercentageComplete);

				_databaseService.InsertLoginDetails (model as LoginModel);

				_messenger.Publish(new PercentageMessage(this));

			}
		}

		#endregion
	}
}