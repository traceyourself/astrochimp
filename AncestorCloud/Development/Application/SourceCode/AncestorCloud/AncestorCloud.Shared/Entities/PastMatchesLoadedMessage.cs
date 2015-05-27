using System;

namespace AncestorCloud.Core
{
	using Cirrious.MvvmCross.Plugins.Messenger;

	public class PastMatchesLoadedMessage : MvxMessage
	{

		public PastMatchesLoadedMessage(object sender)
			: base(sender)
		{
			
		}
	}
}

