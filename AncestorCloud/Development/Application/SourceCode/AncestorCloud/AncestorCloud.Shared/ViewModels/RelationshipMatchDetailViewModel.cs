using System;

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

	}
}

