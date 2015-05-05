﻿using System;
using System.Threading.Tasks;

namespace AncestorCloud.Shared
{
	public interface IAddFamilyService
	{
		Task<ResponseModel<People>> AddFamilyMember(People model);
		Task<ResponseModel<ResponseDataModel>> EditFamilyMember(People model);
	}
}

