using System;
using AncestorCloud.Shared;
using System.Collections.Generic;
using System.IO;
using Foundation;

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

		public string GetDatabasePath (string DbName)
		{
//			var documents = NSFileManager.DefaultManager.GetUrls (NSSearchPathDirectory.CachesDirectory, NSSearchPathDomain.User) [0];
//			string documentsDirectory = documents.ToString();

			const string documentsDirectory = "../Library/Caches";

			string dbpath = System.IO.Path.Combine (documentsDirectory,DbName);

			return dbpath;
		}


		#endregion



	}
}

