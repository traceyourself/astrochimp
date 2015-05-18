using System;
using System.Threading.Tasks;

namespace AncestorCloud.Shared
{
	public interface IFBFriendLinkService
	{
		Task<ResponseModel<People>> FbFriendRead (People friend);
	}
}

