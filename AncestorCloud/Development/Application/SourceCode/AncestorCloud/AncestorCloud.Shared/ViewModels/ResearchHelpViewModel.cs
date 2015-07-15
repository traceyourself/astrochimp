using System;
using System.Collections.Generic;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Plugins.Messenger;
using System.Windows.Input;

namespace AncestorCloud.Shared.ViewModels
{
	public class ResearchHelpViewModel : BaseViewModel
	{
		#region Globals
		private readonly IDatabaseService _databaseService;

		public ResearchHelpViewModel()
		{
			_databaseService = Mvx.Resolve<IDatabaseService> ();	
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
			base.ClearDatabase ();
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