using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AncestorCloud.Shared
{
	public interface IMatchHistoryService
	{
		Task<ResponseModel<List<RelationshipFindResult>>> HistoryReadService(LoginModel model);
	}
}

