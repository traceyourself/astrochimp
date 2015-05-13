using System;
using System.Collections.Generic;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Plugins.Messenger;
using System.Windows.Input;

namespace AncestorCloud.Shared.ViewModels
{
	public class ResearchHelpViewModel : BaseViewModel
	{

		#region ShowmyfamilyViewModel
		public void ShowMyFamilyViewModel()
		{
			ShowViewModel <MyFamilyViewModel> ();
		}
		#endregion

		#region ShowFamilyViewModel
		public void ShowFamilyViewModel()
		{
			ShowViewModel <FamilyViewModel> ();
		}
		#endregion

		#region Close Method
		public void Close()
		{
			this.Close(this);
		}
		#endregion

		#region ShowMatcherViewModel
		public void ShowMatcherViewModel()
		{
			ShowViewModel <MatchViewModel> ();
		}
		#endregion


		#region call home for logout
		public void ShowHomeViewModel()
		{
			ShowViewModel<HomePageViewModel> ();
			this.Close(this);
		}
		#endregion

		#region logout
		public void Logout()
		{
			ShowViewModel<HomePageViewModel>();
			this.Close(this);
		}
		#endregion

		#region ProfilePic
		public void ShowProfilePicModel()
		{
			ShowViewModel<ProfilePicViewModel> (new ProfilePicViewModel.DetailParameter { FromSignUp = false });
		}
		#endregion

	}
}

