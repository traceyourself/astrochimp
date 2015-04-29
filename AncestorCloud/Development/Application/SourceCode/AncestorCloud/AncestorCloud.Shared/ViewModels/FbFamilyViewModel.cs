﻿using System;
using System.Collections.Generic;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Plugins.Messenger;
using System.Windows.Input;

namespace AncestorCloud.Shared.ViewModels
{
	public class FbFamilyViewModel : BaseViewModel
	{

		#region ShowSignViewModel
		public void ShowMyFamilyViewModel()
		{
			ShowViewModel <MyFamilyViewModel> ();
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


		private readonly IDatabaseService _databaseService;

		public FbFamilyViewModel(IDatabaseService  service)
		{
			_databaseService = service;
			GetFbFamilyData ();
		}


		public void NextButtonTapped()
		{
			var _flyoutMessenger = Mvx.Resolve<IMvxMessenger>();
			_flyoutMessenger.Publish (new ChangeFlyoutFlowMessage (this,false));
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

		#region Sqlite Methods

		public void GetFbFamilyData()
		{
			List<People> list = _databaseService.GetFamily();
			FamilyList = list;
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
	}
}

