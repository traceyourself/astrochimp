using System;

namespace AncestorCloud.Core
{
	using Cirrious.MvvmCross.Plugins.Messenger;

	public class ProfilePicUploadedMessage : MvxMessage
	{

		public ProfilePicUploadedMessage(object sender)
			: base(sender)
		{
			
		}
	}
}

