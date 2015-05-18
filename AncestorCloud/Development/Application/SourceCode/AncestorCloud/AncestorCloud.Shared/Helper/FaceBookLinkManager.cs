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

		private readonly IGroupCreateService _groupService;

		private readonly IFamilyCreateService _famService;


		public FaceBookLinkManager ()
		{
			_databaseService = Mvx.Resolve<IDatabaseService>();
			_userReadService = Mvx.Resolve<IUserReadService>();
			_developerLoginService = Mvx.Resolve<IDeveloperLoginService>();
			_fbSignInService = Mvx.Resolve<IFbSigninService>();
			_groupService = Mvx.Resolve<IGroupCreateService>();
			_famService = Mvx.Resolve<IFamilyCreateService>();
		}

		#region Exposed Methods

		public async Task<ResponseStatus> LinkFaceBookSignUpUser()
		{
			String sessionID = await DeveloperLogin ();

			if (sessionID.Equals (String.Empty))
				return ResponseStatus.Fail;
		
			LoginModel loginData = await FbSignInLink (sessionID);

			if (loginData == null)
				return ResponseStatus.Fail;

			loginData = await FbLoginLink (sessionID);

			if (loginData == null)
				return ResponseStatus.Fail;

			loginData = await UserReadService (loginData);

			if (loginData == null)
				return ResponseStatus.Fail;

			loginData = await CreateGroup (loginData);

			if (loginData == null)
				return ResponseStatus.Fail;

			loginData = await CreateFamily (loginData);

			if (loginData == null)
				return ResponseStatus.Fail;

			SaveLoginDetailInDB (loginData);

			return ResponseStatus.OK;
		}

		public async Task<ResponseStatus> LinkFaceBookLoginUser()
		{
			String sessionID = await DeveloperLogin ();

			if (sessionID.Equals (String.Empty))
				return ResponseStatus.Fail;
			
			LoginModel loginData = await FbLoginLink (sessionID);

			if (loginData == null)
				return ResponseStatus.Fail;

			loginData = await UserReadService (loginData);

			if (loginData == null)
				return ResponseStatus.Fail;

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

		#region FBLoginService Method

		private async  Task<LoginModel> FbLoginLink(String sessionID)
		{
			User fbUser = _databaseService.GetUser ();

			ResponseModel<LoginModel> data = await _fbSignInService.LinkFacebookLoginUser (fbUser,sessionID);

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


		#region GroupCreate method

		private async  Task<LoginModel> CreateGroup(LoginModel loginData)
		{
			ResponseModel<LoginModel> loginResponse = await _groupService.CreateGroup (loginData);

			if (loginResponse.Status == ResponseStatus.Fail)
				return null;

			return loginResponse.Content;
		}


		private async  Task<LoginModel> CreateFamily(LoginModel loginData)
		{
			ResponseModel<LoginModel> loginResponse = await _famService.CreateFamily (loginData);

			if (loginResponse.Status == ResponseStatus.Fail)
				return null;

			return loginResponse.Content;
		}
		#endregion

		#region FbSigninLink method

		private async  Task<LoginModel> FbSignInLink(String sessionID)
		{
			User fbUser = _databaseService.GetUser ();

			ResponseModel<LoginModel> data = await _fbSignInService.LinkFacebookSignUpUser (fbUser,sessionID);

			if (data.Status ==ResponseStatus.Fail)
				return null;

			return data.Content;
		}

		#endregion

	}
}

