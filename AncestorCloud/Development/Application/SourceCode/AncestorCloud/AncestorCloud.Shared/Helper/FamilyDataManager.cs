using System;
using Cirrious.CrossCore;
using System.Collections.Generic;

namespace AncestorCloud.Shared
{
	public class FamilyDataManager
	{
		#region Globals
		private readonly IDatabaseService _databaseService;

		#endregion
		public FamilyDataManager ()
		{
			_databaseService = Mvx.Resolve<IDatabaseService> ();
		}

		public List<People> GetParents()
		{
			LoginModel lModal = _databaseService.GetLoginDetails ();

			List<People> listP = _databaseService.RelativeMatching (AppConstant.Parent_comparison,lModal.UserEmail);

			List<People> listFather = _databaseService.RelativeMatching (AppConstant.Father_comparison, lModal.UserEmail);

			if(listFather != null)
				listP.AddRange (listFather);

			List<People> listMOther = _databaseService.RelativeMatching (AppConstant.Mother_comparison, lModal.UserEmail);

			if(listMOther != null)
				listP.AddRange (listMOther);


			return listP;
		}

		public List<People> GetGrandParents()
		{
			LoginModel lModal = _databaseService.GetLoginDetails ();

			List<People> listP = _databaseService.RelativeMatching (AppConstant.GrandParent_comparison,lModal.UserEmail);

			return listP;
		}
			
	}
}

