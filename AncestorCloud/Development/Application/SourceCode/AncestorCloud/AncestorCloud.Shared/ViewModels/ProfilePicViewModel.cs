using System;
using System.IO;
using Cirrious.CrossCore;


namespace AncestorCloud.Shared.ViewModels
{
	public class ProfilePicViewModel : BaseViewModel
	{
		private readonly IProfileService _profileService;

		private readonly IDatabaseService _databaseService;

		public ProfilePicViewModel(IProfileService profileService,IDatabaseService databaseService)
		{
			_profileService = profileService;
			_databaseService = databaseService;
		}

		public void ShowFamiyViewModel()
		{
			ShowViewModel<FlyOutViewModel> ();
			this.Close (this);//XXXXX
		}

		#region Close Method
		public void Close()
		{
			this.Close(this);
		}
		#endregion

		#region call home for logout
		public void ShowHomeViewModel()
		{
			ShowViewModel<HomePageViewModel> ();
			this.Close();
		}
		#endregion

		#region logout
		public void Logout()
		{
			ShowViewModel<HomePageViewModel> ();
			this.Close (this);
		}
		#endregion


		#region Properties

		private Stream _profilePicStream;

		public Stream ProfilePicStream
		{
			get { return _profilePicStream;}

			set {
				_profilePicStream = value;
				RaisePropertyChanged(()=> ProfilePicStream);
			}
		}
		#endregion

		#region Methods

		public async void UploadImage()
		{
			LoginModel model = _databaseService.GetLoginDetails ();

			ResponseModel<ResponseDataModel> response = await _profileService.PostProfileData (model,_profilePicStream);

			if (response.Status == ResponseStatus.OK) {
				Mvx.Resolve<IAlert> ().ShowAlert ("Profile pic uploaded successfully","Success");
				ShowFamiyViewModel ();
			} 
			else 
			{
				Mvx.Resolve<IAlert> ().ShowAlert ("Profile pic upload unsuccessfull","Upload Error");
			}
		}

		#endregion
	}
}

