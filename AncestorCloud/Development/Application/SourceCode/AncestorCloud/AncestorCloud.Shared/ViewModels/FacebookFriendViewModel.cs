using System;
using System.Collections.Generic;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.Plugins.Messenger;

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

		public FacebookFriendViewModel(IDatabaseService  service)
		{
			_databaseService = service;
			GetFacebookFriendData ();
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
		#endregion

	}
}


