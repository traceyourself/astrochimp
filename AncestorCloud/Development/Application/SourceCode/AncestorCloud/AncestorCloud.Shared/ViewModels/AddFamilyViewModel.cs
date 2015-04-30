using System;
using Cirrious.MvvmCross.ViewModels;

namespace AncestorCloud.Shared.ViewModels
{
	public class AddFamilyViewModel:BaseViewModel
	{
		private IAddFamilyService _addService;

		private IDatabaseService _databaseService;

		private IAlert Alert;

		public String AddType { get; set;}

		public AddFamilyViewModel(IAddFamilyService service, IAlert alert)
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
		public async void AddPerson(AddFamilyModel model){

			ResponseModel<ResponseDataModel> response = await _addService.AddFamilyMember(model);

			if (response.Status == ResponseStatus.OK) {
				
			}

		}
		#endregion



	}
}

