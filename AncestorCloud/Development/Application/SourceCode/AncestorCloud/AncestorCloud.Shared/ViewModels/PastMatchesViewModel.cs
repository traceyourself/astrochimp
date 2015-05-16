using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace AncestorCloud.Shared.ViewModels
{
	public class PastMatchesViewModel:BaseViewModel
	{

		#region Close Method
		public void Close()
		{
			this.Close(this);
		}
		#endregion

		private readonly IDatabaseService _databaseService;

		public PastMatchesViewModel(IDatabaseService  service)
		{
			_databaseService = service;
			GetPastMatchesData ();
		}


		#region Sqlite Methods

		public void GetPastMatchesData()
		{
			LoginModel login = _databaseService.GetLoginDetails ();
			List<People> list = _databaseService.RelativeMatching ("",login.UserEmail);
			PastMatchesList = list;
		}
		#endregion

		#region Properties

		private List<People> pastMatchesList;

		public List<People> PastMatchesList
		{
			get { return pastMatchesList; }
			set
			{
				pastMatchesList = value;
				RaisePropertyChanged(() => PastMatchesList);
			}
		}
		#endregion
	
	}
}

