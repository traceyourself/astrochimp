using System;
using AncestorCloud.Shared;
using System.Collections.Generic;
using Android.Provider;
using Android.Content;
using Android.App;
using Android.Database;
using Cirrious.CrossCore;

namespace AncestorCloud.Droid
{
	public class AndroidContactService : IContactService
	{
		public AndroidContactService ()
		{
		}

		public List<People> GetDeviceContacts ()
		{
			
			//return FillContacts();
			return ReadContacts();
		}


		private List<People> ReadContacts()
		{
			
			List<People> contactList = new List<People> ();

			ContentResolver contentResolver = Utilities.CurrentActiveActivity.ContentResolver;

			ICursor cursor = null;
			try {
				cursor = contentResolver.Query(ContactsContract.CommonDataKinds.Phone.ContentUri, null, null, null, null);
				int contactIdIdx = cursor.GetColumnIndex(ContactsContract.Contacts.InterfaceConsts.Id);
				int nameIdx = cursor.GetColumnIndex(ContactsContract.Contacts.InterfaceConsts.DisplayName);
				int phoneNumberIdx = cursor.GetColumnIndex(ContactsContract.CommonDataKinds.Phone.Number);
				int photoIdIdx = cursor.GetColumnIndex(ContactsContract.Contacts.InterfaceConsts.PhotoId);

				cursor.MoveToFirst();

				do {
					int idContact = (int)cursor.GetLong(contactIdIdx);
					String name = cursor.GetString(nameIdx);
					String photo = cursor.GetString(photoIdIdx);
					String phoneNumber = "";
					try{
						phoneNumber = cursor.GetString(phoneNumberIdx);
					}catch(Exception ee){
						Mvx.Trace(ee.StackTrace);
					}

					People people = new People();

					people.Id = idContact;
					people.Name = name;
					people.ProfilePicURL = photo;
					people.Contact = phoneNumber;

					contactList.Add(people);
				
				} while (cursor.MoveToNext());

			} catch (Exception e) {
				Mvx.Trace(e.StackTrace);
			} finally {
				if (cursor != null) {
					cursor.Close();
				}
			}

			return contactList;
		}

	}
}

