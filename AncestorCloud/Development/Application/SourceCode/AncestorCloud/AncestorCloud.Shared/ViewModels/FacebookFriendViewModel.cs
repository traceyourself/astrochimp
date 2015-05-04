using System;
using System.Collections.Generic;

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
			List<People> list = _databaseService.RelativeMatching ("");
			FacebookFriendList = list;
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


