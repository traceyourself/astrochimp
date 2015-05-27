using System;
using System.IO;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.Plugins.Messenger;
using AncestorCloud.Core;


namespace AncestorCloud.Shared.ViewModels
{
	public class ProfilePicViewModel : BaseViewModel
	{
		private readonly IProfileService _profileService;

		private readonly IDatabaseService _databaseService;

		private MvxSubscriptionToken navigationMenuToggleToken;

		IMvxMessenger _messenger = Mvx.Resolve<IMvxMessenger>();

		public ProfilePicViewModel(IProfileService profileService,IDatabaseService databaseService)
		{
			_profileService = profileService;
			_databaseService = databaseService;

			var _messenger = Mvx.Resolve<IMvxMessenger>();
			navigationMenuToggleToken = _messenger.SubscribeOnMainThread<FlyOutCloseMessage>(message => this.Close());

		}

		public void Init(DetailParameter parameter)
		{
			//this.MatchResult = parameter.MatchResult;

			if (parameter == null) return;
			IsFromSignup = parameter.FromSignUp;
		}

		#region from sign up
		public bool IsFromSignup{ get; set;}
		#endregion

		#region Parameter Class

		public class DetailParameter
		{
			public bool FromSignUp { get; set;}
		}
		#endregion

		public void ShowFamiyViewModel()
		{
			//ShowViewModel<M> ();
			if (Mvx.CanResolve<IAndroidService> ()) {
				ShowViewModel<FamilyViewModel> ();
			} else {
				if(IsFromSignup)	
				ShowViewModel	<FlyOutViewModel> (new FlyOutViewModel.DetailParameters { IsFBLogin = false });
			}

			this.Close (this);//XXXXX
		}

		#region IDisposable implementation


			
		#endregion

		#region Close Method
		public void Close()
		{
			this.Close (this);
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
				Mvx.Resolve<IAlert> ().ShowAlert (AlertConstant.PROFILE_PIC_SUCCESS_MESSAGE,AlertConstant.PROFILE_PIC_SUCCESS);
				if (Mvx.CanResolve<IAndroidService> ()){
					_messenger.Publish(new ProfilePicUploadedMessage(this));
				}else{
					_messenger.Publish(new ProfilePicUploadedMessage(this));
					ShowFamiyViewModel ();
				}
			} 
			else 
			{
				Mvx.Resolve<IAlert> ().ShowAlert (AlertConstant.PROFILE_PIC_ERROR_MESSAGE,AlertConstant.PROFILE_PIC_ERROR);
			}
		}

		#endregion

	}
}

