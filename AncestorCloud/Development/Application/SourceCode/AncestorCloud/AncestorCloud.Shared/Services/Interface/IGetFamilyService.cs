using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AncestorCloud.Shared
{
	public interface IGetFamilyService
	{
		Task<ResponseModel<List<People>>> GetFamilyMembers(LoginModel model);
	}
}

