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

		private readonly IContactLinkService _contactLinkService;

		private readonly IAlert _alert;

		private MvxSubscriptionToken inviteContactToken;

		private MvxSubscriptionToken selectContactToken;

		IMvxMessenger _mvxMessenger = Mvx.Resolve<IMvxMessenger>();

		public ContactsViewModel(IDatabaseService  service, IContactService contact, ISMSService smsService, IContactLinkService contactService,IAlert alert)
		{
			_databaseService = service;
			_contactService = contact;
			_smsService = smsService;
			_contactLinkService = contactService;
			_alert = alert;
			GetContactsData ();

			inviteContactToken = _mvxMessenger.SubscribeOnMainThread<InviteContactMessage>(message => this.SendMessage(Contact));
			selectContactToken = _mvxMessenger.SubscribeOnMainThread<SelectContactMessage>(message => this.PeoplePlusClickHandler(Contact));
		}


		#region Sqlite Methods

		public void GetContactsData()
		{
			LoginModel login = _databaseService.GetLoginDetails ();

			List<People> list =  _contactService.GetDeviceContacts();

			foreach (People con in list) {
				con.Relation = AppConstant.CONTACTKEY;
				con.LoginUserLinkID = login.UserEmail;
				_databaseService.InsertContact (con);
			}
			
			if(ContactsList != null)
				ContactsList.Clear();
			
			ContactsList = _databaseService.RelativeMatching(AppConstant.CONTACTKEY,login.UserEmail);

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

		private People _contact;

		public People Contact
		{
			get { return _contact; }
			set
			{
				_contact = value;
				RaisePropertyChanged(() => Contact);
			}
		}

		#endregion

		#region Commands

		public ICommand SendMessageCommand
		{
			get
			{
				return new MvxCommand<People>(item => this.CheckContact(item));
			}
		}

		#endregion

		#region SendMessage

		void SendMessage(People people)
		{
			_smsService.SendSMS (people);
		}

		#endregion

		#region Check Contact

		private async void CheckContact(People people)
		{
			Contact = people;

			if (Contact.IndiOgfn == null) {
				
				if (Contact.Email != null && !Contact.Email.Trim().Equals(string.Empty)) {

					ResponseModel<People> friendResponse = await _contactLinkService.ContactRead (Contact);

					if (friendResponse.Status == ResponseStatus.OK) {
						_databaseService.InsertRelative (Contact);
						_alert.ShowAlertWithOk ("Contact added to Ancestor Cloud. Tap Ok to Select", "Success", AlertType.OKCancelSelectContact);
					} else {
						//_alert.ShowAlert ("Unable to communicate with Ancestor Cloud", "Error");
						_alert.ShowAlertWithOk ("No relation found with Ancestor Cloud. Do you want to invite him?", "Match", AlertType.OKCancelSelectInvite);
					}
				} else 
				{
					_alert.ShowAlertWithOk ("No relation found with Ancestor Cloud. Do you want to invite him?", "Match", AlertType.OKCancelSelectInvite);
				}
			}
			else {

			}
		}

		#endregion
	}
}


