using System;
using System.Collections.Generic;
using AncestorCloud.Shared.ViewModels;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.Plugins.Messenger;

namespace AncestorCloud.Shared
{
	public class CelebritiesViewModel:BaseViewModel
	{
	    #region Close Method
	    public void Close()
		{
			this.Close(this);
		}
		#endregion

		private readonly IDatabaseService _databaseService;

		public CelebritiesViewModel(IDatabaseService  service)
		{
			_databaseService = service;
		    GetCelebritiesData ();

		}

		#region Sqlite Methods
		public void GetCelebritiesData()
		{
			List<Celebrity> list = _databaseService.GetCelebritiesData();
			CelebritiesList = list;
		}

		public void GetCelebritiesDataSearched()
		{
			List<Celebrity> list = _databaseService.FilterCelebs (SearchKey);
			CelebritiesList = list;
		}
		#endregion


		#region plus click event

		public void CelebrityPlusClickHandler(Celebrity celeb)
		{

			ResponseModel<Celebrity> modeltosend = new ResponseModel<Celebrity> ();
			modeltosend.Content = celeb;
			var matchString = Mvx.Resolve<IMvxJsonConverter>().SerializeObject(modeltosend);
			var _matcherMessenger = Mvx.Resolve<IMvxMessenger>();
			_matcherMessenger.Publish (new MatchGetPersonMeassage(this,matchString,true));
			Close ();
		}

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
			peopledata.Email = data.UserEmail;
			PeoplePlusClickHandler(peopledata);
		}
		#endregion


		#region Properties

		private List<Celebrity> celebritiesList;

		public List<Celebrity> CelebritiesList
		{
			get { return celebritiesList; }
			set
			{
				celebritiesList = value;
				RaisePropertyChanged(() => CelebritiesList);
			}
		}

		private string searchKey;

		public string SearchKey
		{
			get { return searchKey; }
			set
			{
				searchKey = value;
				RaisePropertyChanged(() => SearchKey);
				GetCelebritiesDataSearched ();
			}
		}
		#endregion

	}
}

