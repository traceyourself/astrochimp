using System;
using AncestorCloud.Shared;
using UIKit;
using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.CrossCore;

namespace AncestorCloud.Touch
{
	public class IOSAlert:IAlert
	{
		public void ShowAlert (string message, string title)
		{
			UIAlertView alert = new UIAlertView (title, message, null, "OK", null);
			alert.Show ();
		}
		
		public void ShowAlertWithOk (string message,string title, AlertType alertType)
		{
			UIAlertView alert = new UIAlertView (title, message, new IOSAlertDelegate (), "Cancel", new string[]{ "OK" });
			alert.Delegate = new IOSAlertDelegate ();
			alert.Tag = (int)alertType;
			alert.Show ();
		}
	}

	public class IOSAlertDelegate:UIAlertViewDelegate
	{
		readonly IMvxMessenger _mvxMessenger = Mvx.Resolve<IMvxMessenger>();

		public override void Clicked (UIAlertView alertview, nint buttonIndex)
		{
			switch (buttonIndex) 
			{
			case 1:
				{
					if((int)alertview.Tag == (int)AlertType.OKCancelPermit)
						_mvxMessenger.Publish(new CheckFbFriendMessage(this,true));

					if((int)alertview.Tag == (int)AlertType.OKCancelSelect)
						_mvxMessenger.Publish(new SelectFbFriendMessage(this));

					if((int)alertview.Tag == (int)AlertType.OKCancelSelectInvite)
						_mvxMessenger.Publish(new InviteContactMessage(this));

					if((int)alertview.Tag == (int)AlertType.OKCancelSelectContact)
						_mvxMessenger.Publish(new SelectContactMessage(this));
				}
				break;
			}
		}
	}
}

