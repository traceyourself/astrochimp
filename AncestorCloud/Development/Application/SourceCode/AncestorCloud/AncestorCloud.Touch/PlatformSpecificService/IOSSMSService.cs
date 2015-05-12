using System;
using AncestorCloud.Shared;
using Foundation;
using UIKit;

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


			string str = "sms:" + GetPhoneNumber(people);

			var smsTo = NSUrl.FromString(str);

			if (UIApplication.SharedApplication.CanOpenUrl(smsTo))
			{
				UIApplication.SharedApplication.OpenUrl(smsTo);
			} 
			else {
				//TODO: Show Alert for Warning	
			}
		}

		#endregion

		string GetPhoneNumber(People people)
		{
			string contact = people.Contact;
			string[] tokens = contact.Split(new string[] { ")" }, StringSplitOptions.None);
			contact = tokens [0] + tokens [1].Trim ();
			contact = contact.Replace ("(", "").Replace (") ", "").Replace ("-", "").Replace (" ", string.Empty);
			return contact;
		}
	}
}

