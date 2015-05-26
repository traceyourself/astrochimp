using System;
using System.Collections.Generic;
using System.Windows.Input;
using Cirrious.CrossCore;

namespace AncestorCloud.Shared.ViewModels
{
	public class PastMatchesViewModel:BaseViewModel
	{
		private readonly IMatchHistoryService _historyService;

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
			_historyService = Mvx.Resolve<IMatchHistoryService>();
			//GetPastMatchesData ();
		}

		#region Sqlite Methods
		public async void GetPastMatchesData()
		{
			LoginModel login = _databaseService.GetLoginDetails ();
			ResponseModel<List<RelationshipFindResult>> matchList = await _historyService.HistoryReadService (login);
			//List<People> list = _databaseService.RelativeMatching ("",login.UserEmail);
			//PastMatchesList = list;
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

