using System;
using AncestorCloud.Shared;
using System.Collections.Generic;
using Android.Provider;
using Android.Content;
using Android.App;
using Android.Database;

namespace AncestorCloud.Droid
{
	public class AndroidContactService : IContactService
	{
		public AndroidContactService ()
		{
		}



		public List<People> GetDeviceContacts ()
		{
			
			return FillContacts;
		}

		private List<People> FillContacts ()
		{
			List<People> contactList = new List<People> ();

			var uri = ContactsContract.Contacts.ContentUri;
			string[] projection = {
				ContactsContract.Contacts.InterfaceConsts.Id,
				ContactsContract.Contacts.InterfaceConsts.DisplayName,
				ContactsContract.Contacts.InterfaceConsts.PhotoId
			};

			// CursorLoader introduced in Honeycomb (3.0, API11)
			var loader = new CursorLoader(Utilities.CurrentActiveActivity, uri, projection, null, null, null);
			var cursor = (ICursor)loader.LoadInBackground();
			contactList = new List<People> ();  
			if (cursor.MoveToFirst ()) {
				do {
					contactList.Add (new People{
						Id = cursor.GetLong (cursor.GetColumnIndex (projection [0])),
						Name = cursor.GetString (cursor.GetColumnIndex (projection [1])),
						ProfilePicURL = cursor.GetString (cursor.GetColumnIndex (projection [2]))
					});
				} while (cursor.MoveToNext());
			}

			return contactList;
		}
	}
}

