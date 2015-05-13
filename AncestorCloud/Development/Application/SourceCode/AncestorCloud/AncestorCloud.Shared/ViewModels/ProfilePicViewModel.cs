using System;
using Cirrious.CrossCore;
using System.IO;
using Cirrious.CrossCore.Platform;

namespace AncestorCloud.Shared.ViewModels
{
	public class ProfilePicViewModel : BaseViewModel
	{

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

		private string _profilePicURL;

		public string ProfilePicURL
		{
			get { return _profilePicURL;}

			set {
				_profilePicURL = value;
				RaisePropertyChanged(()=> ProfilePicURL);
			}
		}
		#endregion

		#region ProfilePic Path
		public Stream ProfilePicStream{ get; set;}
		#endregion


		#region Upload image
		public void UploadImage()
		{
			Mvx.Trace ("Stream Length : "+ProfilePicStream.Length);
		}
		#endregion




	}
}

