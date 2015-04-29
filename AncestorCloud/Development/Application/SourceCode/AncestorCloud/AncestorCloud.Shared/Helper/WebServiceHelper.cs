using System;
using System.Collections.Generic;

namespace AncestorCloud.Shared
{
	static public class WebServiceHelper
	{
		
		public static string GetWebServiceURL(string webServiceName, Dictionary<string,string> requestParameters = null)
		{
			if ( webServiceName == null)
				return null;

			string url = String.Format (AppConstant.BASEURL + "/" + webServiceName);

			if (requestParameters == null)
				return url;

			string requestString = "";

			int index = 0;

			foreach(KeyValuePair<string, string> kvp in requestParameters)
			{
				requestString = String.Format ("{0}={1}", kvp.Key, kvp.Value);

				if( index < requestParameters.Count - 2 )
					requestString += "&";

				++index;
			}

			url += "?" + requestString;

			return url;
		}
	}
}

