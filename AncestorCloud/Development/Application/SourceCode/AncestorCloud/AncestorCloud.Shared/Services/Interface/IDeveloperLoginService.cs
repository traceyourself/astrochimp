using System;
using System.Threading.Tasks;

namespace AncestorCloud.Shared
{
	public interface IDeveloperLoginService
	{
		Task<ResponseModel<String>> DevelopeLogin ();
	}
}

