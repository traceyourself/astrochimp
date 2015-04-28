using System;
using Cirrious.MvvmCross.Plugins.Messenger;

namespace AncestorCloud.Shared
{
	public class FlyOutCloseMessage : MvxMessage
	{
		public FlyOutCloseMessage(object sender)
			: base(sender)
		{
			
		}
	}
}

