using System;

namespace AncestorCloud.Shared
{
	using Cirrious.MvvmCross.Plugins.Messenger;
	public class ChangeFlyoutFlowMessage : MvxMessage
	{
		public bool ChangeFlyoutFlow;

		public ChangeFlyoutFlowMessage(object sender, bool changeFlow)
			: base(sender)
		{
			ChangeFlyoutFlow = changeFlow;
		}
	}
}

