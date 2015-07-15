using System;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Plugins.Messenger;

namespace AncestorCloud.Shared.ViewModels
{
	public class HomePageViewModel :BaseViewModel
	{
		#region Globals

		private readonly IFileService _fileService;

		private readonly IStoreCelebService _storeCelebService;

		public bool IsSimpleLogin { get; set;}

		#endregion

		public HomePageViewModel()
		{
			_fileService = Mvx.Resolve<IFileService> ();
			_storeCelebService = Mvx.Resolve<IStoreCelebService> ();


//			if(!App.IsAutoLogin)
//				base.ClearDatabase ();
		}

		public void Init(ParameterClass _params)
		{
			IsSimpleLogin = _params.IsSimpleLogin;

			StoreCelebsData ();
		}

		#region ShowLoginViewModel
		public void ShowLoginViewModel()
		{
			ShowViewModel <LoginViewModel> ();
		}
		#endregion

		#region ShowSignViewModel
		public void ShowSignViewModel()
		{
			ShowViewModel <SignUpViewModel> ();
		}
		#endregion

		#region Close Method
		public void Close()
		{
			this.Close(this);
		}
		#endregion

		#region
		private void StoreCelebsData()
		{
			string celebsDataString = _fileService.GetCelebsDataString ();
			_storeCelebService.StoreCelebData (celebsDataString);
		}
		#endregion

		public class ParameterClass
		{
			public bool IsSimpleLogin { get; set;}
		}
	}


}