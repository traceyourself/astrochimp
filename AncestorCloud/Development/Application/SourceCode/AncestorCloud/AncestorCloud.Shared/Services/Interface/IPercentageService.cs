using System;
using System.Threading.Tasks;

namespace AncestorCloud.Shared
{
	public interface IPercentageService
	{
		Task<ResponseModel<LoginModel>> GetPercentComplete (LoginModel model);
	}
}

