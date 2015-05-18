using System;
using Cirrious.MvvmCross.Plugins.Messenger;

namespace AncestorCloud.Shared
{
	public class CheckFbFriendMessage : MvxMessage
	{
		public bool IsPermited;

		public CheckFbFriendMessage(object sender, bool permit)
			: base(sender)
		{
			IsPermited = permit;
		}
	}

	public class SelectFbFriendMessage : MvxMessage
	{

		public SelectFbFriendMessage(object sender)
			: base(sender)
		{
			
		}
	}
}

