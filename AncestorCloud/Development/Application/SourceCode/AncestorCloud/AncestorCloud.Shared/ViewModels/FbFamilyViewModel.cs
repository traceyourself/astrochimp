using System;
using System.Collections.Generic;

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
			List<People> list = _databaseService.RelativeMatching ("");
			FamilyList = list;
		}

		#endregion

	}
}

