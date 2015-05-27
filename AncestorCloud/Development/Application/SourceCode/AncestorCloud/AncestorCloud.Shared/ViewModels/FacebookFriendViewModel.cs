using System;
using System.Collections.Generic;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.MvvmCross.ViewModels;
using System.Windows.Input;

namespace AncestorCloud.Shared.ViewModels
{
	public class FacebookFriendViewModel:BaseViewModel
	{
	

		#region Close Method
		public void Close()
		{
			this.Close(this);
		}
		#endregion

		private readonly IDatabaseService _databaseService;

		private readonly IFBFriendLinkService _friendLinkService;

		private readonly IAlert _alert;

		private MvxSubscriptionToken checkFbFreindToken;

		private MvxSubscriptionToken selectFbFreindToken;

		IMvxMessenger _mvxMessenger = Mvx.Resolve<IMvxMessenger>();

		public FacebookFriendViewModel(IDatabaseService  service,IFBFriendLinkService fbService, IAlert alert)
		{
			_databaseService = service;
			_friendLinkService = fbService;
			_alert = alert;
			GetFacebookFriendData ();
			checkFbFreindToken = _mvxMessenger.SubscribeOnMainThread<CheckFbFriendMessage>(message => this.CheckFriend(message.IsPermited));
			selectFbFreindToken = _mvxMessenger.SubscribeOnMainThread<SelectFbFriendMessage>(message => this.PeoplePlusClickHandler(FbFriend));
		}


		#region Sqlite Methods

		public void GetFacebookFriendData()
		{
			LoginModel login = _databaseService.GetLoginDetails ();
			List<People> list = _databaseService.RelativeMatching (AppConstant.FRIENDKEY,login.UserEmail);
			FacebookFriendList = list;
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

		private List<People> facebookFriendList;

		public List<People> FacebookFriendList
		{
			get { return facebookFriendList; }
			set
			{
				facebookFriendList = value;
				RaisePropertyChanged(() => FacebookFriendList);
			}
		}

		private People _fbFriend;

		public People FbFriend
		{
			get { return _fbFriend; }
			set
			{
				_fbFriend = value;
				RaisePropertyChanged(() => FbFriend);
			}
		}
			
		#endregion

		#region Commands

		public ICommand CheckFacebookFriendCommand
		{
			get
			{
				return new MvxCommand<People>(item => this.GetUserPermission(item));
			}
		}

		#endregion

		#region Check Friend

		private async void CheckFriend(bool isPermited)
		{
			if (!isPermited)
				return;
			
			if(FbFriend.IndiOgfn ==  null)
			{
				ResponseModel<People> friendResponse = await _friendLinkService.FbFriendRead(FbFriend);

				if(friendResponse.Status == ResponseStatus.OK)
				{
					_databaseService.InsertFBFriend (FbFriend);
					_alert.ShowAlertWithOk(AlertConstant.FB_SUCCESS_MESSAGE,AlertConstant.FB_SUCCESS,AlertType.OKCancelSelect);
				}
				else
				{
					_alert.ShowAlert(AlertConstant.FB_ERROR_MESSAGE,AlertConstant.FB_ERROR);
				}
			}
		}


		private void GetUserPermission(People friend)
		{
			return;
			//TODO: remove when facebookfriend is linked

			FbFriend = friend;

			_alert.ShowAlertWithOk(AlertConstant.FB_MATCH_MESSAGE,AlertConstant.FB_MATCH,AlertType.OKCancelPermit);
		}

		#endregion

	}
}


