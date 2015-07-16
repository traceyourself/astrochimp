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


		#region Globals
		private readonly IDatabaseService _databaseService;

		private readonly IContactService _contactService;

		private readonly ISMSService _smsService;

		private readonly IContactLinkService _contactLinkService;

		private readonly IAlert _alert;

		private MvxSubscriptionToken inviteContactToken;

		private MvxSubscriptionToken selectContactToken;

		private MvxSubscriptionToken conatctFetchedToken;

		IMvxMessenger _mvxMessenger = Mvx.Resolve<IMvxMessenger>();
		private readonly ILoader _loader;

		#endregion

		#region Initialization
		public ContactsViewModel(IDatabaseService  service, IContactService contact, ISMSService smsService, IContactLinkService contactService,IAlert alert)
		{
			_databaseService = service;
			_contactService = contact;
			_smsService = smsService;
			_contactLinkService = contactService;
			_alert = alert;
			_loader = Mvx.Resolve<ILoader> ();

			if(Mvx.CanResolve<IAndroidService>())
			{
				GetPhoneContacts ();
			}else{
				GetContactsData ();
			}

			AddMessenger ();
		}

		private void AddMessenger()
		{
			inviteContactToken = _mvxMessenger.SubscribeOnMainThread<InviteContactMessage>(message => this.SendMessage(Contact));
			selectContactToken = _mvxMessenger.SubscribeOnMainThread<SelectContactMessage>(message => this.PeoplePlusClickHandler(Contact));
			conatctFetchedToken = _mvxMessenger.SubscribeOnMainThread<ContactFetchedMessage>(message => this.CreateContactList(message.ContactsList));
		}

		private void RemoveMessenger()
		{
			_mvxMessenger.Unsubscribe<InviteContactMessage> (inviteContactToken);
			_mvxMessenger.Unsubscribe<SelectContactMessage> (selectContactToken);
		}

		#endregion

		#region Sqlite Methods

		public async void GetContactsData()
		{

			List<People> list =  _contactService.GetDeviceContacts();

			CreateContactList (list);
		}

		public async void GetPhoneContacts()
		{
			try{
				_loader.showLoader();

				List<People> list =  await _contactService.ReadPhoneContacts();
				CreateContactList (list);

				_loader.hideLoader();
			}
			catch(Exception e)
			{
				Mvx.Trace (e.StackTrace);
			}
		}

		public void CreateContactList(List<People> list)
		{
			LoginModel login = _databaseService.GetLoginDetails ();

			foreach (People con in list) {
				con.Relation = AppConstant.CONTACTKEY;
				con.LoginUserLinkID = login.UserEmail;
				_databaseService.InsertContact (con);
			}

			if(ContactsList != null)
				ContactsList.Clear();

			ContactsList = _databaseService.RelativeMatching(AppConstant.CONTACTKEY,login.UserEmail);


			if(Mvx.CanResolve<IAndroidService>())
			{
				IMvxMessenger _mvxMessenger = Mvx.Resolve<IMvxMessenger>();
				_mvxMessenger.Publish(new ContactFetchedMessageAndroid(this));
			}

		}
		#endregion


		#region plus click handler

		public void PeoplePlusClickHandler(People people)
		{
			ResponseModel<People> modeltosend = new ResponseModel<People> ();
			//people.Tag = "";
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
			peopledata.Tag = AppConstant.METAGKEY;
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
			if(!Mvx.CanResolve<IAndroidService>()){
				_smsService.SendSMS (people);
			}
		}

		#endregion

		#region Check Contact

		public async void CheckContact(People people)
		{
			LoginModel data = _databaseService.GetLoginDetails ();
			people.SessionId = data.Value;

			Contact = people;

			if (Contact.IndiOgfn == null) {
				
				if (Contact.Email != null && !Contact.Email.Trim().Equals(string.Empty)) {

					ResponseModel<People> friendResponse = await _contactLinkService.ContactRead (Contact);

					if (friendResponse.Status == ResponseStatus.OK) {
						_databaseService.InsertContact (Contact);
						_alert.ShowAlertWithOk (AlertConstant.CONTACT_SUCCESS_MESSAGE, AlertConstant.CONTACT_SUCCESS, AlertType.OKCancelSelectContact);
					} else {
						//_alert.ShowAlert ("Unable to communicate with Ancestor Cloud", "Error");
						_alert.ShowAlertWithOk (AlertConstant.CONTACT_MATCH_SUCCESS, AlertConstant.CONTACT_MATCH, AlertType.OKCancelSelectInvite);
					}
				} else 
				{
					_alert.ShowAlertWithOk (AlertConstant.CONTACT_MATCH_SUCCESS, AlertConstant.CONTACT_MATCH, AlertType.OKCancelSelectInvite);
				}
			}
			else {
				this.PeoplePlusClickHandler (Contact);
			}
		}

		#endregion

		#region Close Method
		public void Close()
		{
			this.Close(this);
		}
		#endregion
	}
}


