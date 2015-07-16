using System;
using Cirrious.MvvmCross.Plugins.Messenger;

namespace AncestorCloud.Shared
{
	public class LogoutMessage: MvxMessage
	{
		public LogoutMessage(object sender)
			: base(sender)
		{

		}
	}

	public class IOSLogoutMessage: MvxMessage
	{
		public IOSLogoutMessage(object sender)
			: base(sender)
		{

		}
	}

}

