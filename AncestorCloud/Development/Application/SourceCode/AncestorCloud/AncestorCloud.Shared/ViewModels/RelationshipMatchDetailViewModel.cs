using System;

namespace AncestorCloud.Shared.ViewModels
{
	public class RelationshipMatchDetailViewModel :BaseViewModel
	{
		public void ShowPastMatches()
		{
			ShowViewModel<PastMatchesViewModel> ();
		}
	}
}

