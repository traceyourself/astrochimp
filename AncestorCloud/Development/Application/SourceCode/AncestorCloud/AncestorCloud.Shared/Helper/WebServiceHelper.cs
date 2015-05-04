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

			string requestString = String.Empty;


//			int index = requestParameters.Count;


			string and = "";

			foreach(KeyValuePair<string, string> kvp in requestParameters)
			{
				requestString += String.Format (and +"{0}={1}", kvp.Key, kvp.Value);


				and = "&";
//				if( index < requestParameters.Count - 2 )
//					requestString += "&";
//
//				--index;


				if(and.Equals(""))
					and = "&";


			}

			url += "?" + requestString;

			return url;
		}
	}
}