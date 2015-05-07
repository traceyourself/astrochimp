using System;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Platform;

namespace AncestorCloud.Shared.ViewModels
{
	public class MatchViewModel:BaseViewModel
	{
		private readonly IMatchService _matchService;
		private readonly IDatabaseService _databaseService;

		public MatchViewModel()
		{
			_matchService = Mvx.Resolve<IMatchService> ();	
			_databaseService = Mvx.Resolve<IDatabaseService> ();
		}


		#region Relationship View

		public void ShowRelationshipMatchDetailViewModel()
		{
			//ShowViewModel<RelationshipMatchDetailViewModel> ();
			MatchService();
		}
		#endregion


		#region friendList
		public void ShowFriendList()
		{
			System.Diagnostics.Debug.WriteLine ("Tapped:");
			ShowViewModel<AddFriendViewModel> ();
		}

		#endregion

		#region Close Method
		public void Close()
		{
			this.Close(this);
		}
		#endregion

		#region Family Call Method
		public void ShowFamilyView()
		{
			ShowViewModel<FamilyViewModel>();
			this.Close(this);
		}
		#endregion

		#region call Reseach help
		public void ShowResearchHelpViewModel()
		{
			ShowViewModel<ResearchHelpViewModel> ();
		}
		#endregion


		#region show past matches
		public void ShowPastMatchesViewModel()
		{
			ShowViewModel<PastMatchesViewModel> ();
		}
		#endregion


		#region logout
		public void Logout()
		{
			ShowViewModel<HomePageViewModel>();
			this.Close(this);
		}
		#endregion

		public void ShowPastMatches()
		{
			ShowViewModel<PastMatchesViewModel> ();
		}

		#region MATCH Service

		public async void MatchService()
		{
			LoginModel data = _databaseService.GetLoginDetails ();

			ResponseModel<RelationshipFindResult> result = await _matchService.Match(data.Value,"747510545","747227929");

			if (result.Status == ResponseStatus.OK) 
			{
				if (result.Content.Found) 
				{

					var matchString = Mvx.Resolve<IMvxJsonConverter>().SerializeObject(result.Content);

					ShowViewModel<RelationshipMatchDetailViewModel> (new RelationshipMatchDetailViewModel.DetailParameter { MatchResult = matchString });
				}
				else 
				{
					//TODO: Show No Match Screen
				}
			}

		}
		#endregion

	}
}

