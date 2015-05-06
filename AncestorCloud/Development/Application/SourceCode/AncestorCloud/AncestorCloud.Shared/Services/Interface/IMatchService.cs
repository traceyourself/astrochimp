using System;
using System.Threading.Tasks;

namespace AncestorCloud.Shared
{
	public interface IMatchService
	{
		Task<ResponseModel<RelationshipFindResult>> Match (string sessionID,string firstOGFN, string secOGFN);
	}
}

