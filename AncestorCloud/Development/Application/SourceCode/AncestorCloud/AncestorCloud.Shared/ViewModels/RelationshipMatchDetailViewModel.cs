using System;
using System.Collections.Generic;

using Cirrious.CrossCore.Platform;
using Cirrious.CrossCore;


namespace AncestorCloud.Shared.ViewModels
{
	public class RelationshipMatchDetailViewModel :BaseViewModel
	{
		public String FirstPersonURL{ get; set;}
		public String SecondPersonURL{ get; set;}
		public String FirstPersonNAME{ get; set;}
		public String SecondPersonNAME{ get; set;}
		private readonly IDatabaseService _databaseService;

		public void Init(DetailParameter parameter)
		{
			//this.MatchResult = parameter.MatchResult;

			if (parameter == null) return;

			IMvxJsonConverter converter = Mvx.Resolve<IMvxJsonConverter>();
			MatchResult = converter.DeserializeObject<RelationshipFindResult>(parameter.MatchResult);
			//RealInit(deserialized);
			FirstPersonURL= parameter.FirstPersonUrl;
			SecondPersonURL = parameter.SecondPersonUrl;
			FirstPersonNAME = parameter.FirstPersonName;
			SecondPersonNAME = parameter.SecondPersonName;
			SetCommonResult ();
		}


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
			get  { return _matchResultList;}
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
			//ShowViewModel<NoMatchesViewModel>();
		}

		public void ShowPastMatchesNoData()
		{
			ShowViewModel<NoMatchesViewModel>();
		}

		#endregion


		#region Parameter Class
		public class DetailParameter
		{
			public String MatchResult { get; set;}
			public String FirstPersonUrl{ get; set;}
			public String SecondPersonUrl{ get; set;}
			public String FirstPersonName{ get; set;}
			public String SecondPersonName{ get; set;}
		}


		public RelationshipMatchDetailViewModel(IDatabaseService  service)
		{
			_databaseService = service;

		}
		#endregion

	}
}