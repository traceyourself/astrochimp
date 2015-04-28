using System;
using Cirrious.MvvmCross.Plugins.Messenger;

namespace AncestorCloud.Shared
{
	public class ReloadFlyOutViewMessage : MvxMessage
	{
		public ReloadFlyOutViewMessage (object sender) : base(sender)
		{
		}
	}
}

