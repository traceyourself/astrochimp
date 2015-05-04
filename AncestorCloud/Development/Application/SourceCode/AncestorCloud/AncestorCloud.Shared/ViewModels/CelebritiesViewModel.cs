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
			#endregion


	}
}

