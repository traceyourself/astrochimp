using System;
using System.Collections.Generic;

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

