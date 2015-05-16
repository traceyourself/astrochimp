using System;
using System.Threading.Tasks;

namespace AncestorCloud.Shared
{
	public interface IFamilyCreateService
	{
		Task<ResponseModel<LoginModel>> CreateFamily(LoginModel model);
	}
}

