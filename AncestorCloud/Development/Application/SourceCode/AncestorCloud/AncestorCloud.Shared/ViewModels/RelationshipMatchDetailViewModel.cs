using System;
using System.Collections.Generic;

using Cirrious.CrossCore.Platform;
using Cirrious.CrossCore;


namespace AncestorCloud.Shared.ViewModels
{
	public class RelationshipMatchDetailViewModel :BaseViewModel
	{

		public RelationshipMatchDetailViewModel()
		{
			
		}


		public void Init(DetailParameter parameter)
		{
			//this.MatchResult = parameter.MatchResult;

			if (parameter == null) return;

			IMvxJsonConverter converter = Mvx.Resolve<IMvxJsonConverter>();
			MatchResult = converter.DeserializeObject<RelationshipFindResult>(parameter.MatchResult);
			//RealInit(deserialized);

			SetCommonResult ();
		}

		#region Properties

		private  RelationshipFindResult _matchResult;

		public RelationshipFindResult MatchResult 
		{
			get  { return _matchResult; }
			set
			{
				_matchResult = value;
				RaisePropertyChanged (() => MatchResult);
			}
		}


		//TODO: Need to check it is required or not: Based on client's reply
		private  List<RelationshipFindResult> _matchResultList;

		public List<RelationshipFindResult> MatchResultList 
		{
			get  { return _matchResultList ?? new  List<RelationshipFindResult> ();}
			set
			{
				_matchResultList = value;
				RaisePropertyChanged (() => MatchResultList);
			}
		}


		#endregion

		#region Data Methods

		void SetCommonResult()
		{
			foreach (CommonMember member in MatchResult.IndiList1) 
			{
				if (MatchResult.CommonIndiOgfn == member.IndiOgfn) 
				{
					MatchResult.CommonResult = member;
				}
			}
			MatchResultList = MatchResultList ?? new List<RelationshipFindResult> ();

			MatchResultList.Add (MatchResult);
		}

		#endregion


		#region
		public void Close()
		{
			this.Close(this);
		}

		public void ShowPastMatches()
		{
			ShowViewModel<PastMatchesViewModel> ();
		}

		#endregion

		#region Parameter Class

		public class DetailParameter
		{
			public String MatchResult { get; set;}
		}



		private readonly IDatabaseService _databaseService;

		public RelationshipMatchDetailViewModel(IDatabaseService  service)
		{
			_databaseService = service;
			GetRelationshipMatchDetailData ();
		}

		#endregion

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

