
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
using Javax.Net.Ssl;
using Java.Security;
using Java.Net;
using Java.IO;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Net;
using System.IO;
using Android.Content.PM;

namespace AncestorCloud.Droid
{
	[Activity (Label = "MySSLSocketFactory", ScreenOrientation = ScreenOrientation.Portrait)]			
	public class MySSLSocketFactory 
	{
		

		public void cert(){
			
			/*KeyStore keyStore = KeyStore.GetInstance(KeyStore.GetDefaultType());
			String algorithm = TrustManagerFactory.GetDefaultAlgorithm();
			TrustManagerFactory tmf = TrustManagerFactory.GetInstance(algorithm);
			tmf.Init(keyStore);

			SSLContext context = SSLContext.GetInstance("TLS");
			context.Init(null, tmf.GetTrustManagers(), null);

			URL url = new URL("https://www.example.com/");
			HttpsURLConnection urlConnection = (HttpsURLConnection) url.OpenConnection();
			urlConnection.SetSSLSocketFactory(context.GetSocketFactory());
			InputStream inp = urlConnection.GetInputStream();

			*/
		}

		public void myMethod(){
			ServicePointManager.ServerCertificateValidationCallback = Validator;
			string url = "https://kreditkarten-banking.lbb.de/";
			var request = (HttpWebRequest) WebRequest.Create(url);
			request.Method = "GET";
			var response = (HttpWebResponse) request.GetResponse ();
			int len = 0;
			using (var _r = new StreamReader (response.GetResponseStream ())) {
				char[] buf = new char [4096];
				int n;
				while ((n = _r.Read (buf, 0, buf.Length)) > 0) {
					/* ignore; we just want to make sure we can read */
					len += n;
				}
			}
			System.Console.WriteLine ("read: {0} bytes", len);
		}


		static bool Validator (object sender, X509Certificate certificate,
			X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			System.Console.WriteLine ("Validator!");
			System.Console.WriteLine ("certificate: {0}", certificate);
			System.Console.WriteLine ("chain[0]: {0}", chain.ChainElements[0].Certificate);
			string a = certificate.ToString ();
			string b = chain.ChainElements [0].Certificate.ToString ();
			if (a == b)
				System.Console.WriteLine ("equal!");
			return true;
		}

	}

}

