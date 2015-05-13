using System;
using AncestorCloud.Shared;

namespace AncestorCloud.Touch
{
	public class IOSReachabilityService : IReachabilityService
	{

	    public 	bool IsNetworkNotReachable()
		{
			return Reachability.InternetConnectionStatus () == NetworkStatus.NotReachable;
		}
	}
}

