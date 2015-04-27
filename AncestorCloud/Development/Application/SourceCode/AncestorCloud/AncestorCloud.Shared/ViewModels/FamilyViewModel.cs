using System;
using Cirrious.MvvmCross.ViewModels;

namespace AncestorCloud.Shared.ViewModels
{
	public class FamilyViewModel : BaseViewModel
	{


		#region Relationship View

		public void ShowEditViewModel()
		{
			ShowViewModel<MyFamilyViewModel> ();
		}
		#endregion

		#region Close Method
		public void Close()
		{
			this.Close(this);
		}
		#endregion

		#region call_next screen _ android
		public void CallMatcher()
		{
			ShowViewModel<MatchViewModel> ();
			this.Close(this);
		}
		#endregion

		#region call home for logout
		public void ShowHomeViewModel()
		{
			ShowViewModel<HomePageViewModel> ();
			this.Close(this);
		}
		#endregion
	
	}
}

