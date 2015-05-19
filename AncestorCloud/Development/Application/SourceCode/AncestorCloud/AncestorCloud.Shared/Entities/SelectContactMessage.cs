using System;
using Cirrious.MvvmCross.Plugins.Messenger;

namespace AncestorCloud.Shared
{
	public class SelectContactMessage: MvxMessage
	{
		public SelectContactMessage(object sender)
			: base(sender)
		{

		}
	}

	public class InviteContactMessage: MvxMessage
	{
		public InviteContactMessage(object sender)
			: base(sender)
		{

		}
	}
}

