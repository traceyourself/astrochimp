using System;
using System.Collections.Generic;

namespace AncestorCloud.Shared.ViewModels
{
	public class ContactsViewModel:BaseViewModel
	{


		#region Close Method
		public void Close()
		{
			this.Close(this);
		}
		#endregion

		private readonly IDatabaseService _databaseService;

		public ContactsViewModel(IDatabaseService  service)
		{
			_databaseService = service;
			GetContactsData ();
		}


		#region Sqlite Methods

		public void GetContactsData()
		{
			List<People> list = _databaseService.RelativeMatching ("");
			ContactsList = list;
		}
		#endregion

		#region Properties

		private List<People> contactsList;

		public List<People> ContactsList
		{
			get { return contactsList; }
			set
			{
				contactsList = value;
				RaisePropertyChanged(() => ContactsList);
			}
		}
		#endregion

	}
}


