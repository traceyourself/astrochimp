using System;
using AncestorCloud.Shared;
using AddressBook;
using Foundation;
using System.Linq;
using CoreFoundation;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Plugins.Messenger;

namespace AncestorCloud.Touch
{
	public class IOSContactService : IContactService
	{
		private readonly ILoader _loader;

		public IOSContactService ()
		{
			_loader = Mvx.Resolve<ILoader> ();
		}

		private List<People> contactList = new List<People> ();

		ABAddressBook addressBook;

		#region IContactService implementation
		public Task<List<People>> ReadPhoneContacts ()
		{
			return null;	
		}

		public List<People> GetDeviceContacts ()
		{
			_loader.showLoader ();

			if (contactList != null)
				contactList.Clear ();
			
			NSError error = null;

			addressBook =  ABAddressBook.Create(out error) ;

			if (error != null) {
				System.Diagnostics.Debug.WriteLine ("Error" + error.Description);
				return null;
			}

			CheckAddressBookAccess ();

			return contactList;
		}

		void CheckAddressBookAccess ()
		{
			switch (ABAddressBook.GetAuthorizationStatus ()) {
			case ABAuthorizationStatus.Authorized:
				AccessGrantedForAddressBook ();
				break;

			case ABAuthorizationStatus.NotDetermined:
				RequestAddressBookAccess ();
				break;

			case ABAuthorizationStatus.Denied:
			case ABAuthorizationStatus.Restricted:
				break;

			default:
				break;
			}
		}

		void RequestAddressBookAccess ()
		{
			addressBook.RequestAccess ((bool granted, NSError error) => {

				if (!granted)
					return;
				
				DispatchQueue.MainQueue.DispatchAsync (() => AccessGrantedForAddressBook ());
			});
		}

		void AccessGrantedForAddressBook ()
		{
			var people = addressBook.GetPeople ();
		
			foreach(ABPerson p in people)
			{
				contactList.Add(GetContact(p));
			}

			IMvxMessenger _mvxMessenger = Mvx.Resolve<IMvxMessenger>();
			_mvxMessenger.Publish(new ContactFetchedMessage(this,contactList));

			_loader.hideLoader ();
		}


		#endregion

		#region Parse Conatct Method

		private People GetContact(ABPerson person)
		{

			People contact = new People ();

			contact.FirstName = person.FirstName;
			contact.LastName = person.LastName;
			contact.MiddleName = person.MiddleName;
			contact.DateOfBirth = (person.Birthday != null) ? person.Birthday.ToString() : String.Empty;
			contact.Email = (person.GetEmails().Count > 0) ? person.GetEmails ().ElementAt (0).Value.ToString() : String.Empty;
			contact.Contact = (person.GetPhones().Count > 0) ? person.GetPhones ().ElementAt (0).Value.ToString() : String.Empty;

			return contact;
		}
		#endregion
	}

}

