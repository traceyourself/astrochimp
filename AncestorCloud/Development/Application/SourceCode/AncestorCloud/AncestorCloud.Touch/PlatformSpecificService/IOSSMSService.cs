using System;
using AncestorCloud.Shared;
using Foundation;
using UIKit;
using MessageUI;

namespace AncestorCloud.Touch
{
	public class IOSSMSService : ISMSService
	{
		public IOSSMSService ()
		{
			
		}

		#region ISMSService implementation

		public void SendSMS (People people)
		{


			//string reciepent = "sms:" + GetPhoneNumber(people);

			string reciepent = GetPhoneNumber(people);

			//var smsTo = NSUrl.FromString(str);

//			if (UIApplication.SharedApplication.CanOpenUrl(smsTo))
//			{
//				UIApplication.SharedApplication.OpenUrl(smsTo);
//			} 
//			else {
//				//TODO: Show Alert for Warning	
//			}

			if (MFMessageComposeViewController.CanSendText) {

				MFMessageComposeViewController smsView = new MFMessageComposeViewController(); 

				smsView.Body = @"SMS message here";
				smsView.Recipients = new string[]{ reciepent};

				smsView.Finished += (object s, MFMessageComposeResultEventArgs args) => args.Controller.DismissViewController (true, null);
		
				UIWindow window = UIApplication.SharedApplication.KeyWindow;

				UIViewController rootViewController = window.RootViewController;

				rootViewController.PresentViewController (smsView, true,null);
			}
		}

		#endregion

		string GetPhoneNumber(People people)
		{
			string contact = people.Contact;
			string[] tokens = contact.Split(new string[] { ")" }, StringSplitOptions.None);
			if(tokens.Length > 1)
				contact = tokens [0] + tokens [1].Trim ();
			contact = contact.Replace ("(", "").Replace (") ", "").Replace ("-", "").Replace (" ", string.Empty);
			return contact;
		}
	}
}

