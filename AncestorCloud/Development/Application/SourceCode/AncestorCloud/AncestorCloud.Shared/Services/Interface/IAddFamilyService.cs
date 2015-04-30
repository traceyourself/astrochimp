using System;
using System.Threading.Tasks;

namespace AncestorCloud.Shared
{
	public interface IAddFamilyService
	{
		Task<ResponseModel<ResponseDataModel>> AddFamilyMember(AddFamilyModel model);
	}
}

