using System;
using Cirrious.CrossCore;
using System.IO;

namespace AncestorCloud.Shared.ViewModels
{
	public class ProfilePicViewModel : BaseViewModel
	{

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

