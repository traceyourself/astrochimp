using System;
using System.Threading.Tasks;
using System.IO;

namespace AncestorCloud.Shared
{
	public interface IProfileService
	{
		Task<ResponseModel<ResponseDataModel>> PostProfileData(LoginModel login, Stream stream);
	}
}

