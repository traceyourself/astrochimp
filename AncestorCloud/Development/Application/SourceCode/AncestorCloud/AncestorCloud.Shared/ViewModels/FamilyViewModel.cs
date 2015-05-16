using System;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.CrossCore;

namespace AncestorCloud.Shared.ViewModels
{
	public class FamilyViewModel : BaseViewModel
	{
		private readonly IDatabaseService _databaseService;


		public FamilyViewModel()
		{
			_databaseService = Mvx.Resolve<IDatabaseService> ();	
		}

		#region Relationship View

		public void ShowEditViewModel()
		{
			ShowViewModel<MyFamilyViewModel> ();
		}
		#endregion

		#region Help

		public void ShowHelpViewModel()
		{
			ShowViewModel<HelpViewModel> ();
		}

		#endregion

		#region Close Method
		public void Close()
		{
			this.Close(this);
		}
		#endregion

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

		#region call Reseach help
		public void ShowResearchHelpViewModel()
		{
			ShowViewModel<ResearchHelpViewModel> ();
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

