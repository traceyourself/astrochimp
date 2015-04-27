using System;
using System.Threading.Tasks;

namespace AncestorCloud.Shared
{
	public interface ISignUpService
	{
		Task<ResponseModel<LoginModel>> SignUp(string name,string email,string password, string developerId, string developerPassword);
	}
}

