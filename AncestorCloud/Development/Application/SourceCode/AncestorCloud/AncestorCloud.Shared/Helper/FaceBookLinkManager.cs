using System;
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

		public async Task<ResponseStatus> LinkFaceBookLoginUser()
		{
			String sessionID = await DeveloperLogin ();

			if (sessionID.Equals (String.Empty))
				return ResponseStatus.Fail;
			
			LoginModel loginData = await FbSignInLink (sessionID);

			if (loginData == null)
				return ResponseStatus.Fail;
			//TODO: Throw error message

			loginData = await UserReadService (loginData);

			if (loginData == null)
				return ResponseStatus.Fail;
			//TODO: Throw error message

			SaveLoginDetailInDB (loginData);

			return ResponseStatus.OK;
		}

		#endregion

		#region Developer Login Method

		private async Task<string> DeveloperLogin()
		{
			ResponseModel<String> data = await _developerLoginService.DevelopeLogin ();

			if (data.Status == ResponseStatus.Fail)
				return String.Empty;
			
			return data.Content;
		}
		#endregion

		#region FBSignInService Method

		private async  Task<LoginModel> FbSignInLink(String sessionID)
		{
			User fbUser = _databaseService.GetUser ();

			ResponseModel<LoginModel> data = await _fbSignInService.LinkFacebookUser (fbUser,sessionID);

			if (data.Status ==ResponseStatus.Fail)
				return null;
			
			return data.Content;
		}

		#endregion

		#region UserReadService Method

		private async  Task<LoginModel> UserReadService(LoginModel loginData)
		{
			ResponseModel<LoginModel> data = await _userReadService.MakeUserReadService (loginData);

			if (data.Status == ResponseStatus.Fail)
				return null;
			
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

