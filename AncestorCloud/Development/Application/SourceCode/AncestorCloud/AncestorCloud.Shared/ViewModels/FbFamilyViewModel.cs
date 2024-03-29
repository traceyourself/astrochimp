﻿using System;
using System.Collections.Generic;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Plugins.Messenger;
using System.Windows.Input;

namespace AncestorCloud.Shared.ViewModels
{
	public class FbFamilyViewModel : BaseViewModel
	{

		#region Globals
		private readonly IDatabaseService _databaseService;

		private readonly FbFamilyDataManager _fbManager;

		IMvxMessenger messenger = Mvx.Resolve<IMvxMessenger> ();

		private MvxSubscriptionToken showMyFamilyViewToken;

		public FbFamilyViewModel(IDatabaseService  service)
		{
			_databaseService = service;
			GetFbFamilyData ();
			_fbManager = new FbFamilyDataManager ();
			showMyFamilyViewToken = messenger.SubscribeOnMainThread<ShowMyFamilyViewMessage>(message => this.ShowMyFamilyViewModel());

		}

		#endregion

		#region ShowSignViewModel
		public void ShowMyFamilyViewModel()
		{
			ShowViewModel <MyFamilyViewModel> ();
			if (Mvx.CanResolve<IAndroidService> ()) {
				Close ();
			}
		}

		#endregion
		#region Close Method
		public void Close()
		{
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


		public void ShowHelpView()
		{
			ShowViewModel<HelpViewModel> ();
			this.Close(this);
		}


		#region call Reseach help
		public void ShowResearchHelpViewModel()
		{
			ShowViewModel<ResearchHelpViewModel> ();
		}
		#endregion

		#region call Reseach help
		public void ShowMatcherViewModel()
		{
			ShowViewModel<MatchViewModel> ();
		}
		#endregion


		public  void NextButtonTapped()
		{
			if (!(Mvx.CanResolve<IAndroidService> ())) {
				var _flyoutMessenger = Mvx.Resolve<IMvxMessenger> ();
				_flyoutMessenger.Publish (new ChangeFlyoutFlowMessage (this, false));
			}
			 AddSelectedFamily ();

			//ShowMyFamilyViewModel ();
		}

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
		#endregion


		#region for Debugging
		public void CheckValues(){
			for(int i=0;i<familyList.Count;i++)
			{
				System.Diagnostics.Debug.WriteLine ("is checked at "+i+" : "+familyList[i].IsSelected);
			}
		}
		#endregion


		#region Sqlite Methods

		public void GetFbFamilyData()
		{
			User fbUser = _databaseService.GetUser ();
			List<People> list = _databaseService.GetFamily(fbUser);
			FamilyList = list;
		}
		#endregion


		#region get Userdata method
		public LoginModel GetUserData()
		{
			LoginModel data = new LoginModel ();
			try{
				data = _databaseService.GetLoginDetails ();
			}catch(Exception e){
			}
			return data;
		}
		#endregion

		#region Commands

		private ACCommand _nextButtonTapped;

		public ICommand NextButtonCommand
		{
			get
			{
				return this._nextButtonTapped ?? (this._nextButtonTapped = new ACCommand(this.NextButtonTapped));
			}
		}
		#endregion


		#region Add Family on server
		public void AddSelectedFamily()
		{
			 _fbManager.AddFbFamilyMembers (FamilyList);	
		}

		#endregion


		#region

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

	}
}

