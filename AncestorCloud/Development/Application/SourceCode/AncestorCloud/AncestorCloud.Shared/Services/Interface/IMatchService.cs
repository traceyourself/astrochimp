using System;
using System.Threading.Tasks;

namespace AncestorCloud.Shared
{
	public interface IMatchService
	{
		Task<ResponseModel<RelationshipFindResult>> Match (string firstOGFN, string secOGFN);
	}
}

