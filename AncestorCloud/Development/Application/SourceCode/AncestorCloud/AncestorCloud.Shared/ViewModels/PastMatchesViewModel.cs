using System;
using System.Collections.Generic;
using System.Windows.Input;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Plugins.Messenger;
using AncestorCloud.Core;

namespace AncestorCloud.Shared.ViewModels
{
	public class PastMatchesViewModel:BaseViewModel
	{

		#region Globals

		private readonly IMatchHistoryService _historyService;
		IMvxMessenger _messenger = Mvx.Resolve<IMvxMessenger>();
		#endregion


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
			PastMatchesList = matchList.Content;
			_messenger.Publish(new PastMatchesLoadedMessage(this));
		}
		#endregion

		#region Properties

		private List<RelationshipFindResult> pastMatchesList;

		public List<RelationshipFindResult> PastMatchesList
		{
			get { return pastMatchesList; }
			set
			{
				pastMatchesList = value;
				RaisePropertyChanged(() => PastMatchesList);
			}
		}
		#endregion

		#region Close Method
		public void Close()
		{
			this.Close(this);
		}
		#endregion
	
	}
}

