using System;
using Cirrious.MvvmCross.ViewModels;

namespace AncestorCloud.Shared.ViewModels
{
	public class MatchViewModel:BaseViewModel
	{
		#region Relationship View

		public void ShowRelationshipMatchDetailViewModel()
		{
			ShowViewModel<RelationshipMatchDetailViewModel> ();
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

	}
}

