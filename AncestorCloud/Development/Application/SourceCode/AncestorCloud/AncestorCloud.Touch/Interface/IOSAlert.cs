using System;
using AncestorCloud.Shared;
using UIKit;

namespace AncestorCloud.Touch
{
	public class IOSAlert:UIAlertViewDelegate,IAlert
	{
		public void ShowAlert (string message, string title)
		{
			UIAlertView alert = new UIAlertView (title, message, this, "OK", null);
			alert.Show ();
		}
		
	}
}

