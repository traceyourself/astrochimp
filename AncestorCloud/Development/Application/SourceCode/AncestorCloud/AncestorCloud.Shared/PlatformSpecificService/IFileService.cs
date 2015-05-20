using System;
using System.Collections.Generic;

namespace AncestorCloud.Shared
{
	public interface IFileService
	{
		string GetCelebsDataString ();
		string GetDatabasePath (string DbName);
	}
}

