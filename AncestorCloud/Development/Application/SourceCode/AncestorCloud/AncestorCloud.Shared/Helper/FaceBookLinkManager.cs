﻿using System;
using Cirrious.CrossCore;
using System.Threading.Tasks;

namespace AncestorCloud.Shared
{
	public class FaceBookLinkManager
	{
		private readonly IDatabaseService _databaseService;

		private readonly IDeveloperLoginService _developerLoginService;

		private readonly IFbSigninService _fbSignInService;

		private readonly IUserReadService _userReadService;


		public FaceBookLinkManager ()
		{
			_databaseService = Mvx.Resolve<IDatabaseService>();
			_userReadService = Mvx.Resolve<IUserReadService>();
			_developerLoginService = Mvx.Resolve<IDeveloperLoginService>();
			_fbSignInService = Mvx.Resolve<IFbSigninService>();
		}

		#region Exposed Methods

		public void LinkFaceBookSignUpUser()
		{
			
		}

		public async void LinkFaceBookLoginUser()
		{
			String sessionID = await DeveloperLogin ();

			LoginModel loginData = await FbSignInLink (sessionID);

			loginData = await UserReadService (loginData);

			SaveLoginDetailInDB (loginData);
		}

		#endregion

		#region Developer Login Method

		private async Task<string> DeveloperLogin()
		{
			ResponseModel<String> data = await _developerLoginService.DevelopeLogin ();

			return data.Content;
		}
		#endregion

		#region FBSignInService Method

		private async  Task<LoginModel> FbSignInLink(String sessionID)
		{
			User fbUser = _databaseService.GetUser ();

			ResponseModel<LoginModel> data = await _fbSignInService.LinkFacebookUser (fbUser,sessionID);

			return data.Content;
		}

		#endregion

		#region UserReadService Method

		private async  Task<LoginModel> UserReadService(LoginModel loginData)
		{
			ResponseModel<LoginModel> data = await _userReadService.MakeUserReadService (loginData);

			return data.Content;
		}

		#endregion

		#region UserReadService Method

		private void SaveLoginDetailInDB(LoginModel loginData)
		{
			_databaseService.InsertLoginDetails (loginData);

		}

		#endregion

	}
}
