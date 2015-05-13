using System;
using System.Threading.Tasks;

namespace AncestorCloud.Shared
{
	public interface IAvatarService
	{
		Task<ResponseModel<LoginModel>> GetIndiAvatar(LoginModel login);
	}
}

