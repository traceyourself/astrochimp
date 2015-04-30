using System;
using AncestorCloud.Shared;
using System.Collections.Generic;
using System.IO;

namespace AncestorCloud.Touch
{
	public class FileService : IFileService
	{
		public FileService ()
		{
		}

		#region IFileService implementation
		public string GetCelebsDataString ()
		{
			string celebsDataString = File.ReadAllText ("Celebs.txt");

			return celebsDataString;
		}
		#endregion

	}
}

