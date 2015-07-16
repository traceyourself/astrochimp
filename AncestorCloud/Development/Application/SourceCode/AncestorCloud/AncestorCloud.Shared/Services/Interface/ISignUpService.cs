using System;
using System.Threading.Tasks;

namespace AncestorCloud.Shared
{
	public interface ISignUpService
	{
		Task<ResponseModel<LoginModel>> SignUp(string FirstName,string LastName,string email,string password, string developerId, string developerPassword);
		Task<ResponseDataModel> GetAnchor (LoginModel modal, string Name);
	}
}

