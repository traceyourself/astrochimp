using System;

namespace AncestorCloud.Core
{
	using Cirrious.MvvmCross.Plugins.Messenger;

	public class MyFamilyReloadMessage : MvxMessage
	{

		public MyFamilyReloadMessage(object sender)
			: base(sender)
		{
			
		}
	}

	public class MyFamilyLoadViewMessage : MvxMessage
	{

		public MyFamilyLoadViewMessage(object sender)
			: base(sender)
		{

		}
	}
}

