
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace AncestorCloud.Droid
{
	[Activity (Label = "Utilities")]			
	public class Utilities 
	{
		public static void RegisterCertificateForApiHit()
		{
			System.Net.ServicePointManager.ServerCertificateValidationCallback =
				new System.Net.Security.RemoteCertificateValidationCallback(delegate { return true; });

			/*try { 
				var w = HttpWebRequest.Create ("https://wsdev.onegreatfamily.com/v11.02/User.svc/Signin?username=mikeyamadeo@gmail.com&Password=password&DeveloperId=AncestorCloud&DeveloperPassword=492C4DD9-A129-4146-BAE9-D0D45FBC315C"); 
				using (var response = w.GetResponse ()) 
				using (var r = new StreamReader (response.GetResponseStream ())) { 
					Mvx.Trace("response : " +r.ReadToEnd ()); 
				} 
			} catch (Exception e) { 
				Console.WriteLine ("error: {0}", e); 
			} */
		}

		#region Holding instance of current Activity for Loader

		public static Activity CurrentActiveActivity{ get; set;}

		#endregion

		#region Holding person type
		public static String AddPersonType{ get; set;}
		#endregion

		#region Holding Bool to check if from fb or normal login
		public static bool LoggedInUsingFb{ get; set;}
		#endregion

	}

}

