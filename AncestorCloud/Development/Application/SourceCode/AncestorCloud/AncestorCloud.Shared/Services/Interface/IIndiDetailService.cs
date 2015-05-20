using System;
using System.Threading.Tasks;

namespace AncestorCloud.Shared
{
	public interface IIndiDetailService
	{
		Task<ResponseModel<LoginModel>> GetIndiDetails(LoginModel login);

		Task<ResponseModel<People>> GetIndiFamilyDetails(string ogfn,string sessionid);
	}
}

