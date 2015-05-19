using System;
using AncestorCloud.Shared;
using Android.App;
using Java.Lang;
using Java.IO;
using System.IO;

namespace AncestorCloud.Droid
{
	public class FileService : IFileService
	{
		public FileService ()
		{
		}

		#region IFileService implementation

		public string GetCelebsDataString ()
		{
			string content;
			using (StreamReader sr = new StreamReader (Application.Context.Assets.Open ("Celebs.txt")))
			{
				content = sr.ReadToEnd ();
			}

			return content;
		}

		public string GetDatabasePath (string DbName)
		{
			return null;
		}

		#endregion
	}
}

