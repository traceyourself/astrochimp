using System;
using System.Threading.Tasks;

namespace AncestorCloud.Shared
{
	public interface IGroupCreateService
	{
		Task<ResponseModel<LoginModel>> CreateGroup(LoginModel model);
	}
}

