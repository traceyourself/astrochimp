using System;
using System.Threading.Tasks;

namespace AncestorCloud.Shared
{
	public interface IUserReadService
	{
		Task<ResponseModel<LoginModel>> MakeUserReadService(LoginModel model);
	}
}

