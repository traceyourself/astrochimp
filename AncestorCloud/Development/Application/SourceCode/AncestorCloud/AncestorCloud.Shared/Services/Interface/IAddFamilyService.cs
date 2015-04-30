using System;
using System.Threading.Tasks;

namespace AncestorCloud.Shared
{
	public interface IAddFamilyService
	{
		Task<String> AddFamilyMember(AddFamilyModel model);
	}
}

