using System;
using System.Collections.Generic;

namespace AncestorCloud.Shared.ViewModels
{
	public class MyFamilyViewModel:BaseViewModel
	{
		private readonly IDatabaseService _databaseService;

		public MyFamilyViewModel(IDatabaseService  service)
		{
			_databaseService = service;
			GetFbFamilyData ();
		}

		#region show Add family dialog model
		public void ShowAddFamilyViewModel()
		{
			ShowViewModel<AddFamilyViewModel> ();
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

		#endregion

		#region Sqlite Methods

		public void GetFbFamilyData()
		{
			List<People> list = _databaseService.RelativeMatching ("");
			FamilyList = list;
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

		public void ShowAddParents()
		{
			ShowViewModel<AddFamilyViewModel> ();
		}

		public void ShowEditFamily()
		{
			ShowViewModel<EditFamilyViewModel>();
		}

	}
}

