using System;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Plugins.Messenger;

namespace AncestorCloud.Shared.ViewModels
{
	public class HomePageViewModel :BaseViewModel
	{
		#region ShowLoginViewModel

		public HomePageViewModel()
		{
//			var messenge = Mvx.Resolve<IMvxMessenger>();
//			messenge.Publish(new Message(this));
		}

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

	}
}


