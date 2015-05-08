using System;
using System.Threading.Tasks;

namespace AncestorCloud.Shared
{
	public interface IFbSigninService
	{
		Task<ResponseModel<LoginModel>> LinkFacebookUser (User user, String sessionID);
	}
}

