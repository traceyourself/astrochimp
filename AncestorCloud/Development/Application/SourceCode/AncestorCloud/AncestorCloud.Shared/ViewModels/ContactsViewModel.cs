using System;
using System.Collections.Generic;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.Plugins.Messenger;

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


		#region plus click handler

		public void PeoplePlusClickHandler(People people)
		{
			ResponseModel<People> modeltosend = new ResponseModel<People> ();
			modeltosend.Content = people;
			var matchString = Mvx.Resolve<IMvxJsonConverter>().SerializeObject(modeltosend);
			var _matcherMessenger = Mvx.Resolve<IMvxMessenger>();
			_matcherMessenger.Publish (new MatchGetPersonMeassage(this,matchString,false));
			Close ();
		}

		public void MePlusClicked()
		{
			LoginModel data = _databaseService.GetLoginDetails ();

			People peopledata = new People ();
			peopledata.ProfilePicURL = "";
			peopledata.IndiOgfn = data.IndiOGFN;
			PeoplePlusClickHandler(peopledata);
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


