using System;
using System.Threading.Tasks;

namespace AncestorCloud.Shared
{
	public interface IFbSigninService
	{
		Task<ResponseModel<LoginModel>> LinkFacebookLoginUser (User user, String sessionID);

		Task<ResponseModel<LoginModel>> LinkFacebookSignUpUser (User user, String sessionID);
	}
}

