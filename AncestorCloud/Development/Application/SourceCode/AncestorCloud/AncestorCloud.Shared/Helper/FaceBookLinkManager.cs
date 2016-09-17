using System;
using Cirrious.CrossCore;
using System.Threading.Tasks;

namespace AncestorCloud.Shared
{
	public class FaceBookLinkManager
	{

		#region Globals
		private readonly IDatabaseService _databaseService;

		private readonly IDeveloperLoginService _developerLoginService;

		private readonly IFbSigninService _fbSignInService;

		private readonly IUserReadService _userReadService;

		private readonly IGroupCreateService _groupService;

		private readonly IFamilyCreateService _famService;

		private readonly IIndiDetailService _indiDetailService;

		private readonly ISignUpService _signupService;

		#endregion

		public FaceBookLinkManager ()
		{
			_databaseService = Mvx.Resolve<IDatabaseService>();
			_userReadService = Mvx.Resolve<IUserReadService>();
			_developerLoginService = Mvx.Resolve<IDeveloperLoginService>();
			_fbSignInService = Mvx.Resolve<IFbSigninService>();
			_groupService = Mvx.Resolve<IGroupCreateService>();
			_famService = Mvx.Resolve<IFamilyCreateService>();
			_indiDetailService = Mvx.Resolve<IIndiDetailService>();
		}

		#region Exposed Methods

		public async Task<ResponseModel<LoginModel>> LinkFaceBookSignUpUser()
		{
			String sessionID = await DeveloperLogin ();
			ResponseModel<LoginModel> returnValue = new ResponseModel<LoginModel> ();
			returnValue.Status = ResponseStatus.Fail;

			if (sessionID.Equals (String.Empty)) {
				returnValue.Status= ResponseStatus.Fail;
				return returnValue;
			}
		
			// check if they already are a user
			LoginModel loginData = await FbLoginLink();

			if (loginData == null)
			{
				returnValue = await FbSignInLink();
				if (returnValue.Status == ResponseStatus.Fail)
				{
					return returnValue;
				}
				loginData = await FbLoginLink();
			}


			if (loginData == null) {
				returnValue.Status= ResponseStatus.Fail;
				return returnValue;
			}

			loginData = await UserReadService (loginData);

			if (loginData == null) {
				returnValue.Status= ResponseStatus.Fail;
				return returnValue;
			}

			SaveLoginDetailInDB (loginData);

			returnValue.Content = loginData;
			returnValue.Status = ResponseStatus.OK;
			return returnValue;
		}

		public async Task<ResponseStatus> LinkFaceBookLoginUser()
		{
			String sessionID = await DeveloperLogin ();

			if (sessionID.Equals (String.Empty))
				return ResponseStatus.Fail;
			
			LoginModel loginData = await FbLoginLink ();

			if (loginData == null)
				return ResponseStatus.Fail;

			loginData = await UserReadService (loginData);

			if (loginData == null)
				return ResponseStatus.Fail;

			loginData = await GetIndiData (loginData);

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

		private async  Task<LoginModel> FbLoginLink()
		{
			User fbUser = _databaseService.GetUser ();

			ResponseModel<LoginModel> data = await _fbSignInService.LinkFacebookLoginUser (fbUser);

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

		#region

		private async  Task<LoginModel> CreateAnchor(LoginModel loginData)
		{
			ResponseDataModel anchor = await _signupService.GetAnchor (loginData, loginData.Name);

			if (anchor == null)
				return loginData;
			else {
				loginData.IndiOGFN = anchor.value;
			}

			return loginData;
		}



		#endregion

		#region Group Read method

		private async Task<LoginModel> GetIndiData(LoginModel loginData)
		{
			ResponseModel<LoginModel> data = await _indiDetailService.GetIndiDetails (loginData);

			if (data.Status ==ResponseStatus.Fail)
				return null;

			return data.Content;
		}

		#endregion

		#region FbSigninLink method

		private async  Task<ResponseModel<LoginModel>> FbSignInLink()
		{
			User fbUser = _databaseService.GetUser ();

			ResponseModel<LoginModel> data = await _fbSignInService.LinkFacebookSignUpUser (fbUser);

			return data;
		}

		#endregion

	}
}

