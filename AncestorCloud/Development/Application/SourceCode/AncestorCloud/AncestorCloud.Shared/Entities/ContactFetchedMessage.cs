using System;
using Cirrious.MvvmCross.Plugins.Messenger;
using System.Collections.Generic;

namespace AncestorCloud.Shared
{
	public class ContactFetchedMessage : MvxMessage
	{
		public List<People> ContactsList;

		public ContactFetchedMessage(object sender, List<People> _contactsList)
			: base(sender)
		{
			ContactsList = _contactsList;
		}
	}
}

