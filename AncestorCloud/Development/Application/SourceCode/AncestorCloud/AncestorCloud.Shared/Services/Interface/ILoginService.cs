using System;
using System.Threading.Tasks;

namespace AncestorCloud.Shared
{
	public interface ILoginService
	{
		Task<ResponseModel<LoginModel>> Login(string email,string password, string developerId, string developerPassword);
	}
}

