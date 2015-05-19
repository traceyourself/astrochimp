using System;
using System.Collections.Generic;
using Cirrious.CrossCore;

namespace AncestorCloud.Shared
{
	 public class FbFamilyDataManager
	{
		private readonly IAddFamilyService _addFamilyService;

		private readonly IDatabaseService _databaseService;

		public FbFamilyDataManager()
		{
			_addFamilyService = Mvx.Resolve<IAddFamilyService> ();
			_databaseService = Mvx.Resolve<IDatabaseService>();
		}

		#region Add Fb Family

		public async void AddFbFamilyMembers(List<People> list)
		{
			LoginModel loginData = _databaseService.GetLoginDetails ();

			foreach (People member in list) 
			{
				if (member.IsSelected)
				{
					member.SessionId = loginData.Value;

					member.LoggedinUserINDIOFGN = loginData.IndiOGFN;

					member.LoggedinUserFAMOFGN = loginData.FamOGFN;

					ResponseModel<People> response=  await _addFamilyService.AddFamilyMember (member);

					if (response.Status == ResponseStatus.OK) {

						People people = response.Content;

						_databaseService.UpdateRelative (people);
					}
				}
			}

			//List <People> tList = _databaseService.GetFamily ();
		}

		#endregion
	}
}

