using System;
using AncestorCloud.Shared;
using AddressBook;
using Foundation;
using System.Linq;
using CoreFoundation;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cirrious.CrossCore;

namespace AncestorCloud.Touch
{
	public class IOSContactService : IContactService
	{

		public IOSContactService ()
		{

		}

		private List<People> contactList = new List<People> ();

		ABAddressBook addressBook;

		#region IContactService implementation

		public List<People> GetDeviceContacts ()
		{

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

