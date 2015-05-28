using System;

namespace AncestorCloud.Core
{
	using Cirrious.MvvmCross.Plugins.Messenger;

	public class PercentageMessage : MvxMessage
	{

		public PercentageMessage(object sender)
			: base(sender)
		{

		}
	}
}