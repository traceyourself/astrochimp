using System;
using System.Collections.Generic;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.MvvmCross.ViewModels;
using System.Windows.Input;

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

		private readonly IContactService _contactService;

		private readonly ISMSService _smsService;

		public ContactsViewModel(IDatabaseService  service, IContactService contact, ISMSService smsService)
		{
			_databaseService = service;
			_contactService = contact;
			_smsService = smsService;
			GetContactsData ();
		}


		#region Sqlite Methods

		public void GetContactsData()
		{
			List<People> list =  _contactService.GetDeviceContacts();
			if(ContactsList != null)
				ContactsList.Clear();
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
			peopledata.FirstName = data.UserEmail;
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

		#region Commands

		public ICommand SendMessageCommand
		{
			get
			{
				return new MvxCommand<People>(item => this.SendMessage(item));
			}
		}
		#endregion

		#region SendMessage

		void SendMessage(People people)
		{
			_smsService.SendSMS (people);
		}

		#endregion
	}
}


