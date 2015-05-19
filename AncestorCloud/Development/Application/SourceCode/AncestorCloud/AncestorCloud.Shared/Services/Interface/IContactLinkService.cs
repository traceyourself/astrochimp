using System;
using System.Threading.Tasks;

namespace AncestorCloud.Shared
{
	public interface IContactLinkService
	{
		Task<ResponseModel<People>> ContactRead (People contact);
	}
}

