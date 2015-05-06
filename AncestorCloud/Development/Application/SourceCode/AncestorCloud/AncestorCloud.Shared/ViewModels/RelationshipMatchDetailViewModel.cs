using System;
using System.Collections.Generic;

namespace AncestorCloud.Shared.ViewModels
{
	public class RelationshipMatchDetailViewModel :BaseViewModel
	{
		public void ShowPastMatches()
		{
			ShowViewModel<PastMatchesViewModel> ();
		}


		public void Close(){
			this.Close(this);
		}


		private readonly IDatabaseService _databaseService;

		public RelationshipMatchDetailViewModel(IDatabaseService  service)
		{
			_databaseService = service;
			GetRelationshipMatchDetailData ();
		}


		#region Sqlite Methods

		public void GetRelationshipMatchDetailData()
		{
			List<People> list = _databaseService.RelativeMatching ("");
			RelationshipMatchDetailList = list;
		}
		#endregion

		#region Properties

		private List<People> relationshipMatchDetailList;

		public List<People> RelationshipMatchDetailList
		{
			get { return relationshipMatchDetailList; }
			set
			{
				relationshipMatchDetailList = value;
				RaisePropertyChanged(() => RelationshipMatchDetailList);
			}
		}
		#endregion

	}
}

