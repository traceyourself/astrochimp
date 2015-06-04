
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
using AncestorCloud.Shared;
using Android.Net;
using Cirrious.CrossCore;

namespace AncestorCloud.Droid
{
				
	public class DroidReachabilityService : IReachabilityService
	{
		#region IReachabilityService implementation		
		public bool IsNetworkNotReachable()
		{
			var connectivityManager = (ConnectivityManager)Utilities.CurrentActiveActivity.GetSystemService(Context.ConnectivityService);

			bool IsNotConnected = true;

			//check for mobile interner connection
			var activeConnection = connectivityManager.ActiveNetworkInfo;
			if ((activeConnection != null)  && activeConnection.IsConnected)
			{
				// we are connected to a network.
				IsNotConnected = false;
			}

			//if not connected then check for wifi
			if(!IsNotConnected){
				try{
					var mobileState = connectivityManager.GetNetworkInfo(ConnectivityType.Mobile).GetState();
					if (mobileState == NetworkInfo.State.Connected)
					{
						// We are connected via WiFi
						IsNotConnected = false;
					}
				}catch(Exception e){
					Mvx.Trace (e.StackTrace);
				}
			}

			return IsNotConnected;
		}
		#endregion


	}
}

