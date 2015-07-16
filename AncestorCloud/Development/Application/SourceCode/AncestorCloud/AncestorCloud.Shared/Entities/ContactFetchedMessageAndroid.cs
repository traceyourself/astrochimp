using System;
using Cirrious.MvvmCross.Plugins.Messenger;
using System.Collections.Generic;

namespace AncestorCloud.Shared
{
	public class ContactFetchedMessageAndroid : MvxMessage
	{
		public ContactFetchedMessageAndroid(object sender)
			: base(sender)
		{
		}
	}
}

