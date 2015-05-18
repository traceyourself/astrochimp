using System;
using AncestorCloud.Shared;
using UIKit;
using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.CrossCore;

namespace AncestorCloud.Touch
{
	public class IOSAlert:UIAlertViewDelegate,IAlert
	{
		public void ShowAlert (string message, string title)
		{
			UIAlertView alert = new UIAlertView (title, message, this, "OK", null);
			alert.Show ();
		}
		
		public void ShowAlertWithOk (string message, string title,AlertType alertType)
		{
			UIAlertView alert = new UIAlertView (title, message, this, "OK", null);
			alert.Delegate = new IOSAlertDelegate ();
			alert.Tag = alertType;
			alert.Show ();
		}
	}

	public class IOSAlertDelegate:UIAlertViewDelegate
	{
		IMvxMessenger _mvxMessenger = Mvx.Resolve<IMvxMessenger>();

		public override void Clicked (UIAlertView alertview, nint buttonIndex)
		{
			switch (buttonIndex) 
			{
			case 1:
				{
					if(alertview.Tag == AlertType.OKCancelPermit)
						_mvxMessenger.Publish(new CheckFbFriendMessage(this,true));

					if(alertview.Tag == AlertType.OKCancelSelect)
						_mvxMessenger.Publish(new CheckFbFriendMessage(this,true));
				}
				break;
			}
		}
	}
}

