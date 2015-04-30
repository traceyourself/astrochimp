using System;
using Cirrious.MvvmCross.ViewModels;

namespace AncestorCloud.Shared.ViewModels
{
	public class AddFamilyViewModel:BaseViewModel
	{
		private IAddFamilyService _addService;

		private IDatabaseService _databaseService;

		private IAlert Alert;

		public AddFamilyViewModel(ILoginService service, IAlert alert)
		{
			_addService = service;
			Alert = alert;
		}

		#region close call
		public void Close()
		{
			this.Close (this);
		}
		#endregion

		#region addPerson
		public void AddPerson(AddFamilyModel model){

			String response = await _addService.AddFamilyMember(model);

			if (response.Status == ResponseStatus.OK) {

				_databaseService.InsertLoginDetails(response.loginModal);

				_databaseService.GetLoginDetails ();

				if (Mvx.CanResolve<IAndroidService> ()) 
				{
					ShowMyFamilyViewModel ();
					CloseCommand.Execute (null);
				}
				else
				{
					IsFbLogin = false;
					CallFlyoutCommand.Execute(null);
					CloseCommand.Execute (null);
				}
			}

		}
		#endregion



	}
}

